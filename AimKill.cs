using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using AotForms;

namespace Client
{
    internal static class Telekill2
    {
        private static Task tele;
        private static CancellationTokenSource cts = new();
        private static bool isRunning = false;
        private static Vector3 lastTeleportedPosition;

        internal static void Start()
        {
            if (isRunning) return;

            cts = new CancellationTokenSource();
            isRunning = true;

            tele = Task.Run(async () =>
            {
                Random rand = new Random();
                while (!cts.Token.IsCancellationRequested)
                {
                    if (!Config.telekill2)
                    {
                        await Task.Delay(3, cts.Token);
                        continue;
                    }

                    var validEnemies = Core.Entities.Values
                        .Where(entity => !entity.IsDead
                                         && (!Config.IgnoreKnocked || !entity.IsKnocked)
                                         && Vector3.Distance(Core.LocalMainCamera, entity.Head) <= 200)
                        .ToList();

                    if (validEnemies.Count > 0)
                    {
                        var randomEnemy = validEnemies[rand.Next(validEnemies.Count)];

                        // Đọc transform và vị trí local player
                        var localRootBone = InternalMemory.Read<uint>(Core.LocalPlayer + (uint)Bones.Root, out var localRootBonePtr);
                        var localTransform = InternalMemory.Read<uint>(localRootBonePtr + 0x8, out var localTransformValue);
                        var localTransformObj = InternalMemory.Read<uint>(localTransformValue + 0x8, out var localTransformObjPtr);
                        var localMatrix = InternalMemory.Read<uint>(localTransformObjPtr + 0x20, out var localMatrixValue);

                        // Lấy vị trí enemy
                        var enemyRootBone = InternalMemory.Read<uint>(randomEnemy.Address + (uint)Bones.Root, out var enemyRootBonePtr);
                        var enemyRootPosition = Transform.GetNodePosition(enemyRootBonePtr, out var enemyRootTransform);

                        // Ghi vị trí mới vào LocalPlayer
                        InternalMemory.Write<Vector3>(localMatrixValue + 0x80, enemyRootTransform);

                        // Ghi nhớ vị trí cuối cùng để không bị giật khi tắt
                        lastTeleportedPosition = enemyRootTransform;
                    }

                    await Task.Delay(1, cts.Token);
                }
            }, cts.Token);
        }

        internal static void Stop()
        {
            if (!isRunning) return;

            cts.Cancel();
            isRunning = false;

            // Gán lại vị trí cuối cùng khi tắt Telekill để tránh bị giật về chỗ cũ
            UpdateLocalPlayerPosition(lastTeleportedPosition);
        }

        private static void UpdateLocalPlayerPosition(Vector3 position)
        {
            var localRootBone = InternalMemory.Read<uint>(Core.LocalPlayer + (uint)Bones.Root, out var localRootBonePtr);
            var localTransform = InternalMemory.Read<uint>(localRootBonePtr + 0x8, out var localTransformValue);
            var localTransformObj = InternalMemory.Read<uint>(localTransformValue + 0x8, out var localTransformObjPtr);
            var localMatrix = InternalMemory.Read<uint>(localTransformObjPtr + 0x20, out var localMatrixValue);

            InternalMemory.Write<Vector3>(localMatrixValue + 0x80, position);
        }
    }
}