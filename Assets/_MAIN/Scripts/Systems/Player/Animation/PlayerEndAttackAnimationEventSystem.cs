using Unity.Collections;
using Unity.Entities;
using UnityEngine;
// using Unity.Mathematics;
// using Unity.Burst;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerEndAttackAnimationEventSystem : ComponentSystem 
	{
		// [BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entities;
			[ReadOnlyAttribute] public ComponentArray<ChildComponent> Child;
			public ComponentArray<PlayerAnimatorComponent> PlayerAnimatorComponent;
			public ComponentArray<EndAttackAnimationEventComponent> EndAttackAnimationEventComponent;
		}
		[InjectAttribute] private Data data;

		// float3 float3Zero = float3.zero;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;
			List<Entity> entitiesInGame = GameManager.entitiesInGame;

			for (int i=0; i<data.Length; i++)
			{
				Entity entity = data.Entities[i];
				ChildComponent child = data.Child[i];
                PlayerAnimatorComponent playerAnimatorComponent = data.PlayerAnimatorComponent[i];
                EndAttackAnimationEventComponent endAttackAnimationEventComponent = data.EndAttackAnimationEventComponent[i];

				int entityIndex = child.EntityIndex;
				int endAnimValue = endAttackAnimationEventComponent.Value;

                commandBuffer.RemoveComponent<EndAttackAnimationEventComponent>(entity);
				GameObject.Destroy(endAttackAnimationEventComponent);
                UpdateInjectedComponentGroups();

				commandBuffer.AddComponent(entitiesInGame[entityIndex], new EndAttackAnimationData{ Value = endAnimValue });

                playerAnimatorComponent.isCheckOnEndAttackAnimation = false;
            }
		}	
	}
}
