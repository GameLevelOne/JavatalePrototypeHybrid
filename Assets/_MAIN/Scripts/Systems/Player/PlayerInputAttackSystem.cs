using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
// using UnityEngine;
// using Unity.Burst;
using System.Collections.Generic;

namespace Javatale.Prototype
{
    public class PlayerInputAttackSystem : JobComponentSystem
    {
        [InjectAttribute] private PlayerAttackSlashBarrier playerAttackSlashBarrier;

        // [BurstCompileAttribute]
        struct PlayerInputAttackJob : IJobProcessComponentData <PlayerInputAttack, Position, FaceDirection, Parent>
        {
            [ReadOnlyAttribute] public EntityCommandBuffer commandBuffer;
            // public EntityArchetype playerAttackArchetype;

			// public List<Entity> childEntitiesInGame;

            public bool isAttackPressed;
            // public Vector3 worldToCameraRotation;
            // public float projectileSpeed;
            public float3 float3Zero;

            PlayerAttackSpawnData playerAttackSpawnData; 

            public void Execute (
                ref PlayerInputAttack playerInputAttack,
				[ReadOnlyAttribute] ref Position pos,
				[ReadOnlyAttribute] ref FaceDirection faceDir,
                [ReadOnlyAttribute] ref Parent parent)
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
                    // playerAttackSpawnData.attackIndex = player.AttackIndex;

                    // commandBuffer.CreateEntity(playerAttackArchetype);
                    // commandBuffer.SetComponent(playerAttackSpawnData);

                    // player.AttackIndex++;
					// player.StartAnimationToggle = 21 + player.AttackIndex;

                    int parentEntityIndex = parent.EntityIndex;
                    int attackIndex = playerInputAttack.Value;

                    List<Entity> childEntitiesInGame = GameManager.childEntitiesInGame;

                    switch (attackIndex)
                    {
                        case 0:
                            commandBuffer.AddComponent(childEntitiesInGame[parentEntityIndex], new AnimationPlayerAttack1());
                            attackIndex = 1;
                            break;
                        case 1:
                            commandBuffer.AddComponent(childEntitiesInGame[parentEntityIndex], new AnimationPlayerAttack2());
                            attackIndex = 2;
                            break;
                        case 2:
                            commandBuffer.AddComponent(childEntitiesInGame[parentEntityIndex], new AnimationPlayerAttack3());
                            attackIndex = 0;
                            break;
                    }
                    
                    playerInputAttack.Value = attackIndex;
                }
            }
        }

        protected override JobHandle OnUpdate (JobHandle inputDeps)
        {
            PlayerInputAttackJob playerInputAttackJob = new PlayerInputAttackJob
            {
                isAttackPressed = GameInput.IsAttackPressed,
                commandBuffer = playerAttackSlashBarrier.CreateCommandBuffer(),
				// childEntitiesInGame = GameManager.childEntitiesInGame,
                // playerAttackArchetype = GameManager.playerAttackArchetype, 
                float3Zero = float3.zero
            };

            JobHandle playerInputAttackHandle = playerInputAttackJob.Schedule(this, inputDeps);

            return playerInputAttackHandle;
        }
    }
}