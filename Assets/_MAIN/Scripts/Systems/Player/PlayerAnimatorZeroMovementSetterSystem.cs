using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Burst;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerAnimatorZeroMovementSetterSystem : ComponentSystem 
	{
        [BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
			[ReadOnlyAttribute] public ComponentDataArray<AnimatorZeroMovement> AnimatorZeroMovement;
		}
		[InjectAttribute] public Data data;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;

            List<GameObjectEntity> childEntitiesInGame = GameManager.childEntitiesInGame;

			for (int i=0; i<data.Length; i++) 
            {
				Entity entity = data.Entity[i];
                Parent parent = data.Parent[i];
                AnimatorZeroMovement animatorZeroMovement = data.AnimatorZeroMovement[i];

				commandBuffer.RemoveComponent<AnimatorZeroMovement>(entity);
                
                int parentEntityIndex = parent.EntityIndex;
                GameObjectEntity entityGO = childEntitiesInGame[parentEntityIndex];
                GameObject childGO = entityGO.gameObject;

                // MOVEMENT
                childGO.AddComponent<PlayerAnimationStateComponent>().Value = PlayerAnimationState.IDLE_STAND;
                entityGO.enabled = false;
                entityGO.enabled = true;
			}
		}
	}
}