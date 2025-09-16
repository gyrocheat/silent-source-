using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Threading;
using System.Windows.Forms;

namespace AotForms
{
    internal class AimLockd
    {
        private static Thread aimLockThread;
        private static CancellationTokenSource cancellationTokenSource;

        internal static void Start()
        {
            cancellationTokenSource = new CancellationTokenSource();
            aimLockThread = new Thread(() => Work(cancellationTokenSource.Token));
            aimLockThread.IsBackground = true;
            aimLockThread.Start();
        }

        internal static void Stop()
        {
            cancellationTokenSource?.Cancel();
        }

        private static void Work(CancellationToken cancellationToken)
        {
            while (true)
            {
                if (cancellationToken.IsCancellationRequested) break;
                if (!Config.AimLock) continue;

                if ((WinAPI.GetAsyncKeyState(Config.AimbotKey) & 0x8000) == 0)
                {
                    Thread.Sleep(1);
                    continue;
                }

                Entity target = null;
                float minFovDistanceSquared = float.MaxValue;
                float minDistanceSquared = float.MaxValue;

                if (Core.Width == -1 || Core.Height == -1 || !Core.HaveMatrix) continue;

                var screenCenter = new Vector2(Core.Width / 2f, Core.Height / 2f);
                var fov = Config.AimFOV ? Config.AimbotFOV : 50;
                var fovSquared = fov * fov;

                foreach (var entity in Core.Entities.Values)
                {
                    if (entity.IsDead || !entity.IsKnown || (Config.IgnoreKnocked && entity.IsKnocked)) continue; // Ignore knocked entities if enabled
                    if (!Config.Aimfovc) continue;
                    var head2D = W2S.WorldToScreen(Core.CameraMatrix, entity.Head, Core.Width, Core.Height);
                    if (head2D.X < 1 || head2D.Y < 1) continue;

                    var playerDistanceSquared = Vector3.DistanceSquared(Core.LocalMainCamera, entity.Head);
                    if (playerDistanceSquared > Config.AimBotMaxDistance * Config.AimBotMaxDistance) continue;

                    var x = head2D.X - screenCenter.X;
                    var y = head2D.Y - screenCenter.Y;
                    var crosshairDistSquared = x * x + y * y;

                    if (crosshairDistSquared > fovSquared) continue;

                    if (crosshairDistSquared < minFovDistanceSquared && playerDistanceSquared < minDistanceSquared)
                    {
                        minFovDistanceSquared = crosshairDistSquared;
                        minDistanceSquared = playerDistanceSquared;
                        target = entity;
                    }
                }

                if (target != null)
                {
                    var playerLook = MathUtils.GetRotationToLocation(target.Head, 0.1f, Core.LocalMainCamera);
                    InternalMemory.Write(Core.LocalPlayer + Offsets.AimRotation, playerLook);
                }
            }
        }
    }
}
