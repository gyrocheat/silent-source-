using AotForms;
using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

public static class AimFOV2
{
 

    private static Thread _aimThread;
    private static bool _isWorking;
    private static Vector2 _previousPosition;
    private static readonly object _threadLock = new object();

    [DllImport("user32.dll")]
    private static extern short GetAsyncKeyState(int vKey);

    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int x, int y);

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out POINT lpPoint);

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT { public int X; public int Y; }



    public static bool IsWorking() => _isWorking && _aimThread?.IsAlive == true;

    public static void Work()
    {
        if (_isWorking) return;

        lock (_threadLock)
        {
            _isWorking = true;
            _aimThread = new Thread(() =>
            {
                POINT cursorPos;
                Vector2 currentPos;
                Vector2 targetPos = Vector2.Zero;
                float lerpValue = 0f;

                while (_isWorking)
                {
                    try
                    {
                        Thread.Sleep(1);

                        if (!ShouldAim())
                        {
                            lerpValue = 0f;
                            continue;
                        }

                        Entity target = FindOptimalTarget();
                        if (target == null)
                        {
                            lerpValue = 0f;
                            continue;
                        }

                        Vector2 targetScreenPos = WorldToScreen(target.Head);
                        if (targetScreenPos.X < 0 || targetScreenPos.Y < 0) continue;

                        GetCursorPos(out cursorPos);
                        currentPos = new Vector2(cursorPos.X, cursorPos.Y);

                        ApplySmoothing(ref targetPos, ref lerpValue, currentPos, targetScreenPos);
                        MoveMouse(targetPos);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[AimFOV] Thread error: {ex.Message}");
                        Thread.Sleep(50);
                    }
                }
            });

            _aimThread.IsBackground = true;
            _aimThread.Start();
        }
    }

    public static void Stop()
    {
        lock (_threadLock)
        {
            _isWorking = false;
            _aimThread?.Join(100);
        }
    }

    private static bool ShouldAim()
    {
        return Config.Enabled &&
               Core.HaveMatrix &&
               Core.Width > 0 &&
               Core.Height > 0 &&
               (GetAsyncKeyState((int)Config.ActivationKey) & 0x8000) != 0;
    }

    private static Entity FindOptimalTarget()
    {
        Vector2 screenCenter = new Vector2(Core.Width / 2f, Core.Height / 2f);
        Entity bestTarget = null;
        float minScore = float.MaxValue;

        foreach (var entity in Core.Entities.Values)
        {
            if (entity == null || entity.IsDead ||
               (Config.IgnoreKnockedPlayers && entity.IsKnocked))
                continue;

            Vector2 screenPos = WorldToScreen(entity.Head);
            float distanceToCenter = Vector2.Distance(screenCenter, screenPos);
            float distance3D = Vector3.Distance(Core.LocalMainCamera, entity.Head);

            if (distanceToCenter > Config.AimbotFOV || distance3D > Config.MaxDistance)
                continue;

            if (Config.VisibilityCheck1 && !IsVisible(entity.Head))
                continue;

            float score = CalculateTargetScore(distanceToCenter, distance3D);
            if (score < minScore)
            {
                minScore = score;
                bestTarget = entity;
            }
        }

        return bestTarget;
    }

    private static Vector2 WorldToScreen(Vector3 worldPos)
    {
        return W2S.WorldToScreen(Core.CameraMatrix, worldPos, Core.Width, Core.Height);
    }

    private static void ApplySmoothing(ref Vector2 targetPos, ref float lerp,
                                      Vector2 currentPos, Vector2 newTargetPos)
    {
        if (lerp == 0f)
        {
            targetPos = newTargetPos;
        }
        else if (Vector2.Distance(targetPos, newTargetPos) > 5f)
        {
            targetPos = newTargetPos;
            lerp = 0f;
        }

        float distance = Vector2.Distance(currentPos, newTargetPos);
        float dynamicSmooth = Config.Smoothness * (1 + Config.SoftnessFactor * (distance / 200f));

        lerp = Math.Clamp(lerp + (1f / dynamicSmooth), 0f, 1f);
        targetPos = Vector2.Lerp(currentPos, targetPos, SmoothStep(lerp));
    }

    private static void MoveMouse(Vector2 position)
    {
        SetCursorPos((int)position.X, (int)position.Y);
        _previousPosition = position;
    }

    private static float CalculateTargetScore(float distance2D, float distance3D)
    {
        return distance2D * 0.6f + distance3D * 0.4f;
    }

    private static float SmoothStep(float x)
    {
        return x * x * (3f - 2f * x);
    }

    private static bool IsVisible(Vector3 targetPos)
    {
        // Triển khai kiểm tra tầm nhìn tại đây (nếu cần)
        return true;
    }
}