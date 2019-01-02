using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Javatale.Prototype
{
    public class BeeAIDirectionSystem : JobComponentSystem
    {
        [InjectAttribute] private EnemyNavMeshSetBarrier enemyNavMeshSetBarrier;

        struct BeeAIDirectionJob : IJobProcessComponentDataWithEntity <Bee, EnemyAIDirection, FaceDirection, Position>
        {
            [ReadOnlyAttribute] public EntityCommandBuffer commandBuffer;

			// public Vector3 vector3Zero;
			public float3 float3Zero;
			public float3 float3Left;
			public float3 float3Right;
			public float3 float3Front;
			public float3 float3Back;
            public float minimumFinishGap;
            public float deltaTime;

            public void Execute (
                [ReadOnlyAttribute] Entity entity,
                [ReadOnlyAttribute] int index,
                ref Bee bee,
                ref EnemyAIDirection enemyAIDir,
				ref FaceDirection faceDir,
                [ReadOnlyAttribute] ref Position pos)
            {
                int enemyAIPowerToggle = bee.EnemyAIPowerToggle;

                if (enemyAIPowerToggle == 1)
                {
                    Vector3 currentPos = pos.Value;
                    Vector3 targerPos = enemyAIDir.Destination;

                    Vector3 distance = targerPos - currentPos;
                    float finishGap = distance.magnitude;

                    if (finishGap < minimumFinishGap) 
                    {
                        bee.EnemyAIPowerToggle = 0;
                        commandBuffer.AddComponent(entity, new AnimatorBeeIdle {});
                    }
                    else
                    {
                        float3 currentDir = enemyAIDir.Value;
                        float3 normalizedDir = distance.normalized;
                        
                        float currentDirX = currentDir.x;
                        float currentDirZ = currentDir.z;
                        float normalizedDirX = normalizedDir.x;
                        normalizedDirX = normalizedDirX > 0f ? 1f : normalizedDirX < 0f ? -1f : 0f;
                        float normalizedDirZ = normalizedDir.z;
                        normalizedDirZ = normalizedDirZ > 0f ? 1f : normalizedDirZ < 0f ? -1f : 0f;

                        if (currentDirX != normalizedDirX || currentDirZ != normalizedDirZ)
                        {
                            // if (normalizedDirX != 0f && normalizedDirZ != 0f) 
                            // {//DIAGONAL FACING
                            //     if (currentDirX == 0f) 
                            //     {//PREVIOUS MOVEMENT IS VERTICAL
                            //         if (normalizedDirZ == -1f) 
                            //         {
                            //             faceDir.DirIndex = 0;//FACE DOWN
                            //             faceDir.Value = float3Back;
                            //         }
                            //         else 
                            //         {
                            //             faceDir.DirIndex = 2;//FACE UP
                            //             faceDir.Value = float3Front;
                            //         }
                            //     } else {//PREVIOUS MOVEMENT IS HORIZONTAL
                            //         if (normalizedDirX == -1f) 
                            //         {
                            //             faceDir.DirIndex = 1;//FACE LEFT
                            //             faceDir.Value = float3Left;
                            //         }
                            //         else 
                            //         {
                            //             faceDir.DirIndex = 3;//FACE RIGHT
                            //             faceDir.Value = float3Right;
                            //         }
                            //     }

                            //     commandBuffer.AddComponent(entity, new AnimatorBeeMove { 
                            //         dirIndex = faceDir.DirIndex,
                            //         dirValue = faceDir.Value	 
                            //     });
                            // } 
                            // else 
                            if (normalizedDirX != 0f) 
                            {
                                if (normalizedDirX == -1f) 
                                {//FACE LEFT
                                    faceDir.DirIndex = 1;
                                    faceDir.Value = float3Left;
                                } 
                                else
                                {//FACE RIGHT
                                    faceDir.DirIndex = 3;
                                    faceDir.Value = float3Right;
                                }

                                commandBuffer.AddComponent(entity, new AnimatorBeeMove { 
                                    dirIndex = faceDir.DirIndex,
                                    dirValue = faceDir.Value	 
                                });
                            }
                            else if ( normalizedDirZ != 0f)
                            {
                                if (normalizedDirZ == -1f) 
                                {//FACE DOWN
                                    faceDir.DirIndex = 0;
                                    faceDir.Value = float3Back;
                                } 
                                else
                                {//FACE UP
                                    faceDir.DirIndex = 2;
                                    faceDir.Value = float3Front;
                                }

                                commandBuffer.AddComponent(entity, new AnimatorBeeMove { 
                                    dirIndex = faceDir.DirIndex,
                                    dirValue = faceDir.Value	 
                                });
                            }
                            // else 
                            // { // IDLE
                            //     commandBuffer.AddComponent(entity, new AnimatorBeeIdle {});
                            // }

					        float3 direction = new float3 (normalizedDirX, 0f, normalizedDirZ);

                            enemyAIDir.Value = direction;
                        }
                    }
                }
                else
                {
                    float beeIdleTimer = bee.IdleTimer;

                    if (beeIdleTimer < 0f)
                    {
                        commandBuffer.AddComponent(entity, new NavMeshData{});
                        commandBuffer.RemoveComponent<EnemyAIDirection>(entity);
                        // GameDebug.Log("Entity "+index+" is finish");
                    }
                    else 
                    {
                        bee.IdleTimer -= deltaTime;
                    }
                }
            }
        } 

        protected override JobHandle OnUpdate (JobHandle inputDeps)
        {
            BeeAIDirectionJob beeAIDirectionJob = new BeeAIDirectionJob 
            {
                commandBuffer = enemyNavMeshSetBarrier.CreateCommandBuffer(),
                // vector3Zero = Vector3.zero,
				float3Zero = float3.zero,
				float3Right = new float3 (1f, 0f, 0f),
				float3Left = new float3 (-1f, 0f, 0f),
				float3Front = new float3 (0f, 0f, 1f),
				float3Back = new float3 (0f, 0f, -1f),
                minimumFinishGap = GameManager.settings.enemyMinimumFinishGap,
                deltaTime = Time.deltaTime,
            };

            JobHandle beeAIDirectionHandle = beeAIDirectionJob.Schedule(this, inputDeps);

            return beeAIDirectionHandle;
        }
    }
}