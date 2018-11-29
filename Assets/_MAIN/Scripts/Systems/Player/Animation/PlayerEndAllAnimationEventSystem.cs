using Unity.Collections;
using Unity.Entities;
// using UnityEngine;
using Unity.Burst;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerEndAllAnimationEventSystem : ComponentSystem 
	{
		[BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			[ReadOnlyAttribute] public ComponentArray<ChildComponent> ChildComponent;
			public ComponentArray<PlayerAnimatorComponent> PlayerAnimatorComponent;
			[ReadOnlyAttribute] public ComponentArray<EndAllAnimationEventComponent> EndAllAnimationEventComponent;
		}
		[InjectAttribute] private Data data;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;
			List<Entity> parentEntitiesInGame = GameManager.parentEntitiesInGame;

			for (int i=0; i<data.Length; i++)
			{
				Entity entity = data.Entity[i];
                ChildComponent childComponent = data.ChildComponent[i];
                PlayerAnimatorComponent playerAnimatorComponent = data.PlayerAnimatorComponent[i];
                EndAllAnimationEventComponent endAllAnimationEventComponent = data.EndAllAnimationEventComponent[i];

				commandBuffer.RemoveComponent<EndAllAnimationEventComponent>(entity);
				GameObjectEntity.Destroy(endAllAnimationEventComponent);
                UpdateInjectedComponentGroups();

				int entityIndex = childComponent.EntityIndex;
                int endAllAnimationValue = endAllAnimationEventComponent.Value;

				commandBuffer.AddComponent(parentEntitiesInGame[entityIndex], new EndAllAnimationData{ Value = endAllAnimationValue });

                playerAnimatorComponent.isCheckOnEndAllAnimation = false;
            }
		}	
	}
}
