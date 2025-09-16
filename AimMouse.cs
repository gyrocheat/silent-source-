using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace AotForms
{
    internal static class Aimmouse
    {
        private static Entity _currentTarget;
        private static Vector2 _screenCenter => new Vector2(Core.Width / 2f, Core.Height / 2f);

        internal static void Work()
        {
            while (true)
            {
                if (Core.Width <= 0 || Core.Height <= 0 || !Core.HaveMatrix || !Config.AimbotVisible1)
                {
                    Thread.Sleep(5);
                    continue;
                }

                _currentTarget = null;

                FindBestTarget();

                if (_currentTarget != null)
                    AimMouseAtTarget(_currentTarget);

                Thread.Sleep(1);
            }
        }

        private static void FindBestTarget()
        {
            Entity bestTarget = null;
            float closest = float.MaxValue;

            lock (Core.Entities)
            {
                foreach (var entity in Core.Entities.Values)
                {
                    if (!IsValidTarget(entity)) continue;

                    Vector2 head2D = W2S.WorldToScreen(Core.CameraMatrix, entity.Head, Core.Width, Core.Height);
                    if (head2D.X < 1 || head2D.Y < 1) continue;

                    Vector2 delta = head2D - _screenCenter;
                    float distCrosshair = delta.LengthSquared();

                    if (distCrosshair > Config.aimfov * Config.aimfov)
                        continue;

                    if (distCrosshair < closest)
                    {
                        closest = distCrosshair;
                        bestTarget = entity;
                    }
                }
            }

            _currentTarget = bestTarget;
        }

        private static bool IsValidTarget(Entity entity)
        {
            return entity != null &&
                   entity.Address != IntPtr.Zero &&
                   !entity.IsDead &&
                   (!Config.IgnoreKnocked || !entity.IsKnocked);
        }

        private static void AimMouseAtTarget(Entity target)
        {
            Vector2 head2D = W2S.WorldToScreen(Core.CameraMatrix, target.Head, Core.Width, Core.Height);
            Vector2 delta = head2D - _screenCenter;

            if (delta.LengthSquared() > Config.aimfov * Config.aimfov)
                return;

            float sensi = Config.AimMouseSensi;
            float smooth = Config.AimMouseSmooth;

            int moveX = (int)((delta.X * sensi) / smooth);
            int moveY = (int)((delta.Y * sensi) / smooth);

            MoveMouseRelative(moveX, moveY);
        }

        private static void MoveMouseRelative(int dx, int dy)
        {
            INPUT[] inputs = new INPUT[1];
            inputs[0].type = 0; // INPUT_MOUSE
            inputs[0].U.mi = new MOUSEINPUT
            {
                dx = dx,
                dy = dy,
                mouseData = 0,
                dwFlags = MOUSEEVENTF_MOVE,
                time = 0,
                dwExtraInfo = IntPtr.Zero
            };
            SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        internal static void StartHotkeyThread()
        {
            new Thread(MonitorHotkeys) { IsBackground = true }.Start();
        }

        private static void MonitorHotkeys()
        {
            while (true)
            {
                if ((GetAsyncKeyState(Keys.F9) & 0x8000) != 0)
                {
                    Config.AimbotVisible1 = !Config.AimbotVisible1;
                    Console.WriteLine($"[Hotkey] AimbotVisible: {Config.AimbotVisible1}");
                    Thread.Sleep(300);
                }
                Thread.Sleep(50);
            }
        }

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        [DllImport("user32.dll")]
        private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        private const int MOUSEEVENTF_MOVE = 0x0001;

        [StructLayout(LayoutKind.Sequential)]
        private struct INPUT
        {
            public uint type;
            public InputUnion U;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct InputUnion
        {
            [FieldOffset(0)] public MOUSEINPUT mi;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
    }
}