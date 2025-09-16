using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AotForms.main;

namespace AotForms
{
    internal static class AimFOV
    {
        public static AimMode SelectedAimMode { get; set; } = AimMode.Nearest;

        internal static void Work()
        {
            while (true)
            {
                if (!Config.AimFOV)
                {
                    Thread.Sleep(1);
                    continue;
                }

                if ((WinAPI.GetAsyncKeyState(Config.AimFovKey) & 0x8000) == 0)
                {
                    Thread.Sleep(1);
                    continue;
                }

                Entity target = null;
                float closestMetric = float.MaxValue;

                if (Core.Width == -1 || Core.Height == -1 || !Core.HaveMatrix) continue;

                var screenCenter = new Vector2(Core.Width / 2f, Core.Height / 2f);

                foreach (var entity in Core.Entities.Values)
                {
                    if (!entity.IsKnown || entity.IsDead || (Config.IgnoreKnocked && entity.IsKnocked)) continue;

                    var head2D = W2S.WorldToScreen(Core.CameraMatrix, entity.Head, Core.Width, Core.Height);
                    var playerDistance = Vector3.Distance(Core.LocalMainCamera, entity.Head);
                    var crosshairDist = Vector2.Distance(new Vector2(head2D.X, head2D.Y), screenCenter);

                    float effectiveFOV = Math.Min(Config.AimbotFOV, Math.Min(Core.Width, Core.Height) / 2f);
                    if (crosshairDist > effectiveFOV || playerDistance > Config.AimBotMaxDistance) continue;

                    float metric = (SelectedAimMode == AimMode.Nearest) ? playerDistance : crosshairDist;

                    if (metric < closestMetric)
                    {
                        closestMetric = metric;
                        target = entity;
                    }
                }

                if (target != null)
                {
                    var playerLook = MathUtils.GetRotationToLocation(target.Head, 0.1f, Core.LocalMainCamera);
                    InternalMemory.Write(Core.LocalPlayer + Offsets.AimRotation, playerLook);
                    Thread.Sleep(4);
                }
            }
        }
    }

}
