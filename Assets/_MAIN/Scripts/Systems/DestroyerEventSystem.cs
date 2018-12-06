using Unity.Collections;
using Unity.Entities;
using Unity.Burst;
// using UnityEngine;
// using Unity.Mathematics;
using System.Collections.Generic;

namespace Javatale.Prototype
{
	public class DestroyerEventSystem : ComponentSystem 
	{
		[BurstCompileAttribute]
		public struct Data 
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			[ReadOnlyAttribute] public ComponentArray<ChildComponent> ChildComponent;
			[ReadOnlyAttribute] public ComponentArray<DestroyedEventComponent> DestroyedEventComponent;
		}
		[InjectAttribute] private Data data;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;

			// List<GameObject[]> playerSlashAttackChilds = GameManager.settings.playerSlashAttackChilds;
			List<Entity> parentEntitiesInGame = GameManager.parentEntitiesInGame;
			// List<GameObjectEntity> childEntitiesInGame = GameManager.childEntitiesInGame;

            for (int i=0; i<data.Length; i++)
			{
				Entity entity = data.Entity[i];
				ChildComponent childComponent = data.ChildComponent[i];
				// DestroyedEventComponent destroyedEventComponent = data.DestroyedEventComponent[i];

                commandBuffer.RemoveComponent<DestroyedEventComponent>(entity);

				int entityIndex = childComponent.EntityIndex;
				commandBuffer.AddComponent(parentEntitiesInGame[entityIndex], new DestroyedData {});
			}
        }
    }
}