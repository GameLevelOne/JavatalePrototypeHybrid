using Unity.Collections;
using Unity.Entities;
using Unity.Burst;
// using UnityEngine;
using Unity.Mathematics;
using System.Collections.Generic;

namespace Javatale.Prototype
{
	public class EnemyNavMeshEventSystem : ComponentSystem 
	{
		[BurstCompileAttribute]
		public struct Data 
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			[ReadOnlyAttribute] public ComponentArray<ChildComponent> ChildComponent;
			[ReadOnlyAttribute] public ComponentArray<NavMeshEventComponent> NavMeshEventComponent;
			public ComponentArray<EnemyAIComponent> EnemyAIComponent;
		}
		[InjectAttribute] private Data data;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;

            for (int i=0; i<data.Length; i++)
			{
				Entity entity = data.Entity[i];
				EnemyAIComponent enemyAIComponent = data.EnemyAIComponent[i];
				NavMeshEventComponent navMeshEventComponent = data.NavMeshEventComponent[i];

                float3 destination = navMeshEventComponent.Destination;

                commandBuffer.RemoveComponent<NavMeshEventComponent>(entity);
				GameObjectEntity.Destroy(navMeshEventComponent);

				enemyAIComponent.navMeshAgent.SetDestination(destination);
                enemyAIComponent.navMeshAgent.enabled = true;
			}
        }
    }
}