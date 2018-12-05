using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
// using Unity.Transforms;
using Unity.Mathematics;
// using UnityEngine;
// using Unity.Burst;
// using System.Collections.Generic;

namespace Javatale.Prototype
{
    public class PlayerInputAttackSystem : JobComponentSystem
    {
        [InjectAttribute] private PlayerAttackSlashBarrier playerAttackSlashBarrier;

        // [BurstCompileAttribute]
        struct PlayerInputAttackJob : IJobProcessComponentDataWithEntity <PlayerInputAttack, Player, FaceDirection>
        {
            [ReadOnlyAttribute] public EntityCommandBuffer commandBuffer;
            // public EntityArchetype playerAttackArchetype;

			// public List<Entity> childEntitiesInGame;

            public bool isAttackPressed;
            // public Vector3 worldToCameraRotation;
            // public float projectileSpeed;
            public float3 float3Zero;

            // PlayerAttackSpawnData playerAttackSpawnData; 

            public void Execute (
                [ReadOnlyAttribute] Entity entity, //IJobProcessComponentDataWithEntity
                [ReadOnlyAttribute] int index, //IJobProcessComponentDataWithEntity
                ref PlayerInputAttack playerInputAttack,
				[ReadOnlyAttribute] ref Player player,
				[ReadOnlyAttribute] ref FaceDirection faceDir)
            {
                // SLASH ATTACK
                if (isAttackPressed) {                    
                    // float3 faceDirValue = faceDir.Value;

                    // Position initAttackPos = new Position{ Value = pos.Value + faceDirValue };
                    MoveDirection initMoveDir = new MoveDirection{ Value = float3Zero };
                    FaceDirection initFaceDir = new FaceDirection{ Value = faceDir.Value, DirIndex = faceDir.DirIndex };
                    MoveSpeed initMoveSpeed = new MoveSpeed{ Value = 0f };

                    commandBuffer.AddComponent(entity, new AnimatorPlayerSlashAttack{
                        // pos = initAttackPos,
                        moveDir = initMoveDir,
                        faceDir = initFaceDir,
                        moveSpeed = initMoveSpeed,
                        attackIndex = player.AttackIndex
                    });
                }
            }
        }

        protected override JobHandle OnUpdate (JobHandle inputDeps)
        {
            PlayerInputAttackJob playerInputAttackJob = new PlayerInputAttackJob
            {
                isAttackPressed = GameInput.IsAttackPressed,
                commandBuffer = playerAttackSlashBarrier.CreateCommandBuffer(),
                float3Zero = float3.zero
            };

            JobHandle playerInputAttackHandle = playerInputAttackJob.Schedule(this, inputDeps);

            return playerInputAttackHandle;
        }
    }
}