using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;
// using Unity.Burst;
using Unity.Mathematics;

namespace Javatale.Prototype
{
    public class PlayerInputAttackSystem : JobComponentSystem
    {
        [InjectAttribute] private PlayerAttackSlashBarrier playerAttackSlashBarrier;

        // [BurstCompileAttribute]
        struct PlayerInputAttackJob : IJobProcessComponentDataWithEntity <PlayerInputAttack, Position, FaceDirection, Player>
        {
            [ReadOnlyAttribute] public EntityCommandBuffer commandBuffer;
            public EntityArchetype playerAttackArchetype;

            public bool isAttackPressed;
            // public Vector3 worldToCameraRotation;
            // public float projectileSpeed;
            public float3 float3Zero;

            PlayerAttackSpawnData playerAttackSpawnData; 

            public void Execute (
				[ReadOnlyAttribute] Entity entity,
				[ReadOnlyAttribute] int index,
                [ReadOnlyAttribute] ref PlayerInputAttack playerInputAttack,
				[ReadOnlyAttribute] ref Position pos,
				[ReadOnlyAttribute] ref FaceDirection faceDir,
                [ReadOnlyAttribute] ref Player player)
            {
                // SLASH ATTACK
                if (isAttackPressed) {
                    playerAttackSpawnData.pos = new Position 
                    {
                        Value = pos.Value + faceDir.Value
                    };
                    playerAttackSpawnData.moveDir = new MoveDirection 
                    {
                        Value = float3Zero
                    };
                    playerAttackSpawnData.faceDir = new FaceDirection 
                    { 
                        Value = faceDir.Value, dirIndex = faceDir.dirIndex 
                    };
                    playerAttackSpawnData.moveSpeed = new MoveSpeed 
                    { 
                        Value = 0f
                    };
                    playerAttackSpawnData.attackIndex = player.AttackIndex;

                    // commandBuffer.CreateEntity(playerAttackArchetype);
                    // commandBuffer.SetComponent(playerAttackSpawnData);

                    // player.AttackIndex++;
					// player.StartAnimationToggle = 21 + player.AttackIndex;

                    int attackIndex = playerInputAttack.Value;

                    switch (attackIndex)
                    {
                        case 0:
                            commandBuffer.AddComponent(entity, new AnimationPlayerAttack1());
                            break;
                        case 1:
                            commandBuffer.AddComponent(entity, new AnimationPlayerAttack2());
                            break;
                        case 2:
                            commandBuffer.AddComponent(entity, new AnimationPlayerAttack3());
                            playerInputAttack.Value = -1;
                            break;
                    }
                    
                    playerInputAttack.Value++;
                }
            }
        }

        protected override JobHandle OnUpdate (JobHandle inputDeps)
        {
            PlayerInputAttackJob playerInputAttackJob = new PlayerInputAttackJob
            {
                isAttackPressed = GameInput.IsAttackPressed,
                commandBuffer = playerAttackSlashBarrier.CreateCommandBuffer(),
                playerAttackArchetype = GameManager.playerAttackArchetype, 
                float3Zero = float3.zero
            };

            JobHandle playerInputAttackHandle = playerInputAttackJob.Schedule(this, inputDeps);

            return playerInputAttackHandle;
        }
    }
}