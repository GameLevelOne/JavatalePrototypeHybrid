using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
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
        struct PlayerInputAttackJob : IJobProcessComponentDataWithEntity <PlayerInputAttack, Position, FaceDirection, MoveDirection>
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
				[ReadOnlyAttribute] ref Position pos,
				[ReadOnlyAttribute] ref FaceDirection faceDir,
                ref MoveDirection moveDir)
            {
                // SLASH ATTACK
                if (isAttackPressed) {
                    moveDir.Value = float3Zero;
                    
                    float3 faceDirValue = faceDir.Value;

                    Position initAttackPos = new Position{ Value = pos.Value + faceDirValue };
                    MoveDirection initMoveDir = new MoveDirection{ Value = float3Zero };
                    FaceDirection initFaceDir = new FaceDirection{ Value = faceDirValue, DirIndex = faceDir.DirIndex };
                    MoveSpeed initMoveSpeed = new MoveSpeed{ Value = 0f };

                    commandBuffer.AddComponent(entity, new AnimatorPlayerSlashAttack{
                        pos = initAttackPos,
                        moveDir = initMoveDir,
                        faceDir = initFaceDir,
                        moveSpeed = initMoveSpeed
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