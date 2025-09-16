using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using AotForms;

namespace Client
{
    internal static class Telekill
    {
        private static Task tele;
        private static CancellationTokenSource cts = new();
        private static bool isRunning = false;

        // Enemy hiện tại sẽ tele tới
        private static Entity? targetEnemy = null;

        internal static void Start()
        {
            if (isRunning) return;

            cts = new CancellationTokenSource();
            isRunning = true;

            tele = Task.Run(async () =>
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    // Nếu Telekill chưa bật => bỏ qua
                    if (!Config.telekill)
                    {
                        targetEnemy = null;
                        await Task.Delay(3, cts.Token);
                        continue;
                    }

                    // Nếu không có target hoặc target đã chết/bị knock => tìm lại
                    if (targetEnemy == null || targetEnemy.IsDead || (Config.IgnoreKnocked && targetEnemy.IsKnocked))
                    {
                        targetEnemy = Core.Entities.Values
                            .Where(entity => !entity.IsDead
                                             && (!Config.IgnoreKnocked || !entity.IsKnocked)
                                             && Vector3.Distance(Core.LocalMainCamera, entity.Head) <= 500)
                            .OrderBy(e => Vector3.Distance(Core.LocalMainCamera, e.Head)) // ưu tiên gần nhất
                            .FirstOrDefault();
                    }

                    // Nếu có enemy hợp lệ => tele tới đó
                    if (targetEnemy != null)
                    {
                        var localRootBone = InternalMemory.Read<uint>(Core.LocalPlayer + (uint)Bones.Root, out var localRootBonePtr);
                        var localTransform = InternalMemory.Read<uint>(localRootBonePtr + 0x8, out var localTransformValue);
                        var localTransformObj = InternalMemory.Read<uint>(localTransformValue + 0x8, out var localTransformObjPtr);
                        var localMatrix = InternalMemory.Read<uint>(localTransformObjPtr + 0x20, out var localMatrixValue);

                        var enemyRootBone = InternalMemory.Read<uint>(targetEnemy.Address + (uint)Bones.Root, out var enemyRootBonePtr);
                        var enemyRootPosition = Transform.GetNodePosition(enemyRootBonePtr, out var enemyRootTransform);

                        // Ghi vị trí enemy vào local player
                        InternalMemory.Write<Vector3>(localMatrixValue + 0x80, enemyRootTransform);
                    }

                    await Task.Delay(1, cts.Token);
                }
            }, cts.Token);
        }

        internal static void Stop()
        {
            if (!isRunning) return;

            cts.Cancel();
            cts = new CancellationTokenSource();
            isRunning = false;
            targetEnemy = null;
        }
    }
}
