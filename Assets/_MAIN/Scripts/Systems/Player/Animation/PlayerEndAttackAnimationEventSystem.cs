using Unity.Collections;
using Unity.Entities;
// using UnityEngine;
// using Unity.Mathematics;
using Unity.Burst;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerEndAttackAnimationEventSystem : ComponentSystem 
	{
		[BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			[ReadOnlyAttribute] public ComponentArray<ChildComponent> ChildComponent;
			public ComponentArray<PlayerAnimatorComponent> PlayerAnimatorComponent;
			[ReadOnlyAttribute] public ComponentArray<EndAttackAnimationEventComponent> EndAttackAnimationEventComponent;
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
                EndAttackAnimationEventComponent endAttackAnimationEventComponent = data.EndAttackAnimationEventComponent[i];

                commandBuffer.RemoveComponent<EndAttackAnimationEventComponent>(entity);
				GameObjectEntity.Destroy(endAttackAnimationEventComponent);
                // UpdateInjectedComponentGroups();

				int entityIndex = childComponent.EntityIndex;
				int endAttackAnimationValue = endAttackAnimationEventComponent.Value;

				commandBuffer.AddComponent(parentEntitiesInGame[entityIndex], new EndAttackAnimationData{ Value = endAttackAnimationValue });

                playerAnimatorComponent.isCheckOnEndAttackAnimation = false;
            }
		}	
	}
}
