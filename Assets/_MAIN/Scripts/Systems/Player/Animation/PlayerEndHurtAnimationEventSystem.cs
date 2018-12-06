using Unity.Collections;
using Unity.Entities;
// using UnityEngine;
using Unity.Burst;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerEndHurtAnimationEventSystem : ComponentSystem 
	{
		[BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			[ReadOnlyAttribute] public ComponentArray<ChildComponent> ChildComponent;
			public ComponentArray<PlayerAnimatorComponent> PlayerAnimatorComponent;
			public ComponentArray<PlayerColliderComponent> PlayerColliderComponent;
			[ReadOnlyAttribute] public ComponentArray<EndHurtAnimationEventComponent> EndHurtAnimationEventComponent;
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
                PlayerColliderComponent playerColliderComponent = data.PlayerColliderComponent[i];
                EndHurtAnimationEventComponent endHurtAnimationEventComponent = data.EndHurtAnimationEventComponent[i];

				commandBuffer.RemoveComponent<EndHurtAnimationEventComponent>(entity);
				GameObjectEntity.Destroy(endHurtAnimationEventComponent);
                // UpdateInjectedComponentGroups();

				int entityIndex = childComponent.EntityIndex;
                int endHurtAnimationValue = endHurtAnimationEventComponent.Value;

				commandBuffer.AddComponent(parentEntitiesInGame[entityIndex], new EndHurtAnimationData { Value = endHurtAnimationValue });

                playerAnimatorComponent.isCheckOnEndHurtAnimation = false;
                playerColliderComponent.isCheckOnDamaged = false;
            }
		}	
	}
}
