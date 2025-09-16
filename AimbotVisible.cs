using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;

namespace AotForms
{
    internal static class AimbotSystem
    {
        private static Entity _currentTarget;
        private static float _closestDistanceSquared;
        private static Vector2 _aimPosition;
        private static readonly float Smoothing = 7f; // Điều chỉnh độ mượt (cao = chậm hơn)

        internal static void Work()
        {
            while (true)
            {
                if (!Config.AimbotVisible || Core.Width <= 0 || Core.Height <= 0 || !Core.HaveMatrix)
                {
                    Thread.Sleep(5);
                    continue;
                }

                FindBestTarget();

                if (_currentTarget != null)
                {
                    SmoothAimAtTarget(_currentTarget);
                }

                Thread.Sleep(1);
            }
        }

        private static void FindBestTarget()
        {
            Entity bestTarget = null;
            float closestDistanceSquared = float.MaxValue;
            var screenCenter = new Vector2(Core.Width / 2f, Core.Height / 2f);

            lock (Core.Entities)
            {
                foreach (var entity in Core.Entities.Values)
                {
                    if (!IsValidTarget(entity)) continue;

                    var head2D = W2S.WorldToScreen(Core.CameraMatrix, entity.Head, Core.Width, Core.Height);
                    if (head2D.X <= 1 || head2D.Y <= 1) continue;

                    float distanceToPlayerSquared = Vector3.DistanceSquared(Core.LocalMainCamera, entity.Head);
                    if (distanceToPlayerSquared > Config.AimBotMaxDistance * Config.AimBotMaxDistance) continue;

                    var delta = head2D - screenCenter;
                    float distanceToCrosshairSquared = delta.LengthSquared();

                    if (distanceToCrosshairSquared < closestDistanceSquared &&
                        distanceToCrosshairSquared <= Config.AimbotFOV * Config.AimbotFOV)
                    {
                        closestDistanceSquared = distanceToCrosshairSquared;
                        bestTarget = entity;
                        _aimPosition = head2D;
                    }
                }
            }

            _currentTarget = bestTarget;
            _closestDistanceSquared = closestDistanceSquared;
        }

        private static bool IsValidTarget(Entity entity)
        {
            if (entity == null || entity.Address == 0) return false;
            if (entity.IsDead || (Config.IgnoreKnocked && entity.IsKnocked)) return false;

            return true;
        }

        private static void SmoothAimAtTarget(Entity target)
        {
            if (target == null || target.Address == 0) return;

            var screenCenter = new Vector2(Core.Width / 2f, Core.Height / 2f);
            var delta = _aimPosition - screenCenter;

            // Smooth movement
            var smoothedDelta = delta / Smoothing;

            // Thêm một ít ngẫu nhiên để tránh mẫu chuyển động chính xác
            smoothedDelta.X += GetRandomFloat(-0.5f, 0.5f);
            smoothedDelta.Y += GetRandomFloat(-0.5f, 0.5f);

            var newAim = screenCenter + smoothedDelta;

            if (!InternalMemory.Read<uint>(target.Address + 0x3D8, out uint headCollider) || headCollider == 0)
                return;

            // Mô phỏng giống người chơi đang nhích chuột về phía mục tiêu
            InternalMemory.Write(target.Address + 0x50, headCollider);
        }

        private static float GetRandomFloat(float min, float max)
        {
            var rand = new Random(Guid.NewGuid().GetHashCode());
            return (float)(rand.NextDouble() * (max - min) + min);
        }
    }
}