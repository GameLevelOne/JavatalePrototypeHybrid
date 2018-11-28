using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;
// using Unity.Burst;

namespace Javatale.Prototype
{
    public class PlayerInputAttackSystem : JobComponentSystem
    {
        [InjectAttribute] private PlayerAttackSlashBarrier playerAttackSlashBarrier;

        // [BurstCompileAttribute]
        struct PlayerInputAttackJob : IJobProcessComponentData <PlayerInputAttack, Position, FaceDirection, Player>
        {
            public bool isAttackPressed;

            public Vector3 worldToCameraRotation;
            public float projectileSpeed;
            public EntityArchetype playerAttackArchetype;
            [ReadOnlyAttribute] public EntityCommandBuffer commandBuffer;

            PlayerAttackSpawnData playerAttackSpawnData; 

            public void Execute (
                ref PlayerInputAttack playerInputAttack,
				[ReadOnlyAttribute] ref Position pos,
				[ReadOnlyAttribute] ref FaceDirection faceDir,
                ref Player player)
            {
                if (isAttackPressed) {
                    playerAttackSpawnData.pos = new Position 
                    {
                        Value = pos.Value + faceDir.Value
                    };
                    playerAttackSpawnData.rot = new Rotation
                    {
                        Value = Quaternion.Euler(worldToCameraRotation)
                    };
                    // playerAttackSpawnData.moveDir = new MoveDirection 
                    // {
                    //     Value = faceDir.Value 
                    // };
                    playerAttackSpawnData.faceDir = new FaceDirection 
                    { 
                        Value = faceDir.Value, dirIndex = faceDir.dirIndex 
                    };
                    playerAttackSpawnData.attackIndex = player.AttackIndex;

                    commandBuffer.CreateEntity(playerAttackArchetype);
                    commandBuffer.SetComponent(playerAttackSpawnData);

                    // player.AttackIndex++;
					player.StartAnimationToggle = 21 + player.AttackIndex;
                }
            }
        }

        protected override JobHandle OnUpdate (JobHandle inputDeps)
        {
            PlayerInputAttackJob playerInputAttackJob = new PlayerInputAttackJob
            {
                isAttackPressed = GameInput.IsAttackPressed,
                worldToCameraRotation = GameManager.settings.worldToCameraRotation,
                commandBuffer = playerAttackSlashBarrier.CreateCommandBuffer(),
                playerAttackArchetype = GameManager.playerAttackArchetype 
            };

            JobHandle playerInputAttackHandle = playerInputAttackJob.Schedule(this, inputDeps);

            return playerInputAttackHandle;
        }
    }
}