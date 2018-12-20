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
			public ComponentArray<EnemyAIComponent> EnemyAIComponent;
			[ReadOnlyAttribute] public ComponentArray<NavMeshEventComponent> NavMeshEventComponent;
		}
		[InjectAttribute] private Data data;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;
			List<Entity> parentEntitiesInGame = GameManager.parentEntitiesInGame;

            for (int i=0; i<data.Length; i++)
			{
				Entity entity = data.Entity[i];
				EnemyAIComponent enemyAIComponent = data.EnemyAIComponent[i];
				ChildComponent childComponent = data.ChildComponent[i];
				NavMeshEventComponent navMeshEventComponent = data.NavMeshEventComponent[i];

				// navMeshEventComponent = childComponent.GetComponent<NavMeshEventComponent>(); //DISINI
                float3 destination = navMeshEventComponent.Destination;

                commandBuffer.RemoveComponent<NavMeshEventComponent>(entity);
				GameObjectEntity.Destroy(navMeshEventComponent);

				enemyAIComponent.navMeshAgent.SetDestination(destination);
                enemyAIComponent.navMeshAgent.enabled = true;
				float3 fixedDestination =  enemyAIComponent.navMeshAgent.destination;

				int entityIndex = childComponent.EntityIndex;
				// GameDebug.Log("set destination "+destination);
				// GameDebug.Log("OK Child");

                commandBuffer.AddComponent(parentEntitiesInGame[entityIndex], new EnemyAIDirection 
				{
					Destination = new float3(fixedDestination.x, 0f, fixedDestination.z)
				});
			}
        }
    }
}