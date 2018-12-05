using Unity.Collections;
using Unity.Entities;
// using UnityEngine;
// using Unity.Mathematics;
using Unity.Burst;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerSpawnAttackAnimationEventSystem : ComponentSystem 
	{
		[BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			[ReadOnlyAttribute] public ComponentArray<ChildComponent> ChildComponent;
			public ComponentArray<PlayerAnimatorComponent> PlayerAnimatorComponent;
			[ReadOnlyAttribute] public ComponentArray<SpawnAttackAnimationEventComponent> SpawnAttackAnimationEventComponent;
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
                SpawnAttackAnimationEventComponent spawnAttackAnimationEventComponent = data.SpawnAttackAnimationEventComponent[i];

                commandBuffer.RemoveComponent<SpawnAttackAnimationEventComponent>(entity);
				GameObjectEntity.Destroy(spawnAttackAnimationEventComponent);
                UpdateInjectedComponentGroups();

				int entityIndex = childComponent.EntityIndex;
				int spawnAttackAnimationValue = spawnAttackAnimationEventComponent.Value;

				commandBuffer.AddComponent(parentEntitiesInGame[entityIndex], new SpawnAttackAnimationData { Value = spawnAttackAnimationValue });

                playerAnimatorComponent.isCheckOnSpawnAttackAnimation = false;
            }
		}	
	}
}
