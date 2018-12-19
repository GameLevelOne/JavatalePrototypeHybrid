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

			public Vector3 vector3Zero;
			public float3 float3Zero;
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

                    float finishGap = (currentPos - targerPos).magnitude;

                    // GameDebug.Log("finishGap "+finishGap);
                    if (finishGap < minimumFinishGap) {
                        bee.EnemyAIPowerToggle = 0;
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
                vector3Zero = Vector3.zero,
				float3Zero = float3.zero,
                minimumFinishGap = GameManager.settings.enemyMinimumFinishGap,
                deltaTime = Time.deltaTime,
            };

            JobHandle beeAIDirectionHandle = beeAIDirectionJob.Schedule(this, inputDeps);

            return beeAIDirectionHandle;
        }
    }
}