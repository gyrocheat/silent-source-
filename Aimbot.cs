using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;

namespace AotForms
{
    internal static class Aimbot
    {
        private static bool isAiming = false; // Cờ theo dõi trạng thái ghim
        private static DateTime aimStartTime; // Thời điểm bắt đầu ghim
        private static DateTime aimPressTime; // Thời điểm nhấn phím aim

        internal static void Work()
        {
            while (true)
            {
                if (!Config.AimBot)
                {
                    Thread.Sleep(1);
                    continue;
                }
                DelayedAimbot(); // Gọi hàm DelayedAimbot tại đây
                Thread.Sleep(1); // Thêm delay nhỏ để không tốn CPU
            }
        }


        private static void DelayedAimbot()
        {
            if ((WinAPI.GetAsyncKeyState(Config.Aim9) & 0x8000) == 0)
            {
                isAiming = false; // Reset cờ ghim
                return;
            }

            if (!isAiming)
            {
                isAiming = true;
                aimPressTime = DateTime.Now;
            }

            if ((DateTime.Now - aimPressTime).TotalMilliseconds >= Config.DelayAim)
            {
                AimbotLogic();
            }
        }


        private async static void AimbotLogic()
        {
            Entity target = null;
            float distance = float.MaxValue;

            if (Core.Width == -1 || Core.Height == -1 || !Core.HaveMatrix) return;

            Vector2 screenCenter = new Vector2(Core.Width / 2f, Core.Height / 2f);

            foreach (var entity in Core.Entities.Values)
            {
                if (!entity.IsKnown || entity.IsDead || (Config.IgnoreKnocked && entity.IsKnocked)) continue;



                var head2D = W2S.WorldToScreen(Core.CameraMatrix, entity.Head, Core.Width, Core.Height);

                if (head2D.X < 1 || head2D.Y < 1) continue;

                float playerDistance = Vector3.Distance(Core.LocalMainCamera, entity.Head);
                if (playerDistance > Config.AimBotMaxDistance) continue;

                float x = head2D.X - screenCenter.X;
                float y = head2D.Y - screenCenter.Y;
                var crosshairDist = (float)Math.Sqrt(x * x + y * y);

                if (crosshairDist > 24)
                {
                    continue;
                }


                distance = crosshairDist;
                target = entity;
            }

            if (target != null)
            {
                var playerLook = MathUtils.GetRotationToLocation(target.Head, 0.1f, Core.LocalMainCamera);
                InternalMemory.Write(Core.LocalPlayer + Offsets.AimRotation, playerLook);
                await Task.Delay(TimeSpan.FromMilliseconds(0.5));

            }
        }

    }
}