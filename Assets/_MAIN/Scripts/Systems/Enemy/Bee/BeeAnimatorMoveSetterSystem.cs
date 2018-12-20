using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Burst;
using Unity.Mathematics;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class BeeAnimatorMoveSetterSystem : ComponentSystem 
	{
        [BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			// public ComponentDataArray<Bee> Bee;
			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
			[ReadOnlyAttribute] public ComponentDataArray<AnimatorBeeMove> AnimatorBeeMove;
		}
		[InjectAttribute] public Data data;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;
            List<int> entitiesIdleLoopAnimationChecker = GameManager.entitiesIdleLoopAnimationChecker;
            List<GameObjectEntity> childEntitiesInGame = GameManager.childEntitiesInGame;

			for (int i=0; i<data.Length; i++) 
            {
				Entity entity = data.Entity[i];
                Parent parent = data.Parent[i];
                AnimatorBeeMove animatorBeeMove = data.AnimatorBeeMove[i];

				commandBuffer.RemoveComponent<AnimatorBeeMove>(entity);

				int entityIndex = parent.EntityIndex;

				if (entitiesIdleLoopAnimationChecker[entityIndex] == 0)
				{
					entitiesIdleLoopAnimationChecker[entityIndex] = 1;
					
					GameObjectEntity entityGO = childEntitiesInGame[entityIndex];
					GameObject childGO = entityGO.gameObject;
					
					int dirIndex = animatorBeeMove.dirIndex;
					float3 dirValue = animatorBeeMove.dirValue;

					// DIRECTION
					AnimatorDirectionComponent animDirComponent = childGO.AddComponent<AnimatorDirectionComponent>();
					animDirComponent.dirIndex = dirIndex;
					animDirComponent.dirValue = dirValue;

					// MOVEMENT
					childGO.AddComponent<BeeAnimationMovePatrolComponent>();
					
					entityGO.enabled = false;
					entityGO.enabled = true;
					UpdateInjectedComponentGroups();
					// return;
				}
			}
		}
	}
}