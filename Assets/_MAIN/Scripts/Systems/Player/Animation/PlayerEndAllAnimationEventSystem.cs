using Unity.Collections;
using Unity.Entities;
using UnityEngine;
// using Unity.Mathematics;
// using Unity.Burst;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerEndAllAnimationEventSystem : ComponentSystem 
	{
		// [BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entities;
			[ReadOnlyAttribute] public ComponentArray<ChildComponent> Child;
			public ComponentArray<PlayerAnimatorComponent> PlayerAnimatorComponent;
			public ComponentArray<EndAllAnimationEventComponent> EndAllAnimationEventComponent;
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
                EndAllAnimationEventComponent endAllAnimationEventComponent = data.EndAllAnimationEventComponent[i];

				int entityIndex = child.EntityIndex;
				int endAnimValue = endAllAnimationEventComponent.Value;

                commandBuffer.RemoveComponent<EndAllAnimationEventComponent>(entity);
				GameObject.Destroy(endAllAnimationEventComponent);
                UpdateInjectedComponentGroups();

				commandBuffer.AddComponent(entitiesInGame[entityIndex], new EndAllAnimationData{ Value = endAnimValue });

                playerAnimatorComponent.isCheckOnEndAllAnimation = false;
            }
		}	
	}
}
