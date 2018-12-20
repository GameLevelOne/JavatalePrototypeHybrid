using Unity.Collections;
using Unity.Entities;
// using UnityEngine;
using Unity.Burst;
// using Unity.Mathematics;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class BeeAnimationMovePatrolSetterSystem : ComponentSystem 
	{
        [BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			[ReadOnlyAttribute] public ComponentArray<ChildComponent> ChildComponent;
			[ReadOnlyAttribute] public ComponentArray<BeeAnimationMovePatrolComponent> BeeAnimationMovePatrolComponent;
			public ComponentArray<BeeAnimatorComponent> BeeAnimatorComponent;
		}
		[InjectAttribute] private Data data;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;
            List<int> entitiesIdleLoopAnimationChecker = GameManager.entitiesIdleLoopAnimationChecker;
			List<int> entitiesAnimationToggle = GameManager.entitiesAnimationToggle;

			for (int i=0; i<data.Length; i++) {
				Entity entity = data.Entity[i];
				ChildComponent childComponent = data.ChildComponent[i];
				BeeAnimatorComponent beeAnimatorComponent = data.BeeAnimatorComponent[i];
				BeeAnimationMovePatrolComponent beeAnimationMovePatrolComponent = data.BeeAnimationMovePatrolComponent[i];

				commandBuffer.RemoveComponent<BeeAnimationMovePatrolComponent>(entity);
				GameObjectEntity.Destroy(beeAnimationMovePatrolComponent);

				int entityIndex = childComponent.EntityIndex;
				int animationToggle = entitiesAnimationToggle[entityIndex];

				if (animationToggle == 0)
				{
					BeeAnimationState state = BeeAnimationState.MOVE_PATROL;
					beeAnimatorComponent.currentState = state;
					beeAnimatorComponent.animator.Play(state.ToString());
				}

				entitiesIdleLoopAnimationChecker[entityIndex] = 0;
			}
		}
	}
}