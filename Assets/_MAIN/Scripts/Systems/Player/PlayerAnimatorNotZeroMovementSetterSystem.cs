using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Burst;
using Unity.Mathematics;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerAnimatorNotZeroMovementSetterSystem : ComponentSystem 
	{
        [BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
			[ReadOnlyAttribute] public ComponentDataArray<AnimatorNotZeroMovement> AnimatorNotZeroMovement;
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
                AnimatorNotZeroMovement animatorNotZeroMovement = data.AnimatorNotZeroMovement[i];

				commandBuffer.RemoveComponent<AnimatorNotZeroMovement>(entity);
                
                int dirIndex = animatorNotZeroMovement.dirIndex;
				float3 dirValue = animatorNotZeroMovement.dirValue;
                
                int parentEntityIndex = parent.EntityIndex;
                GameObjectEntity entityGO = childEntitiesInGame[parentEntityIndex];
                GameObject childGO = entityGO.gameObject;

                // MOVEMENT
                childGO.AddComponent<PlayerAnimationStateComponent>().Value = PlayerAnimationState.MOVE_RUN;
                entityGO.enabled = false;
                entityGO.enabled = true;

                // DIRECTION
                childGO.AddComponent<AnimatorDirectionComponent>();
                AnimatorDirectionComponent animDirComponent = childGO.GetComponent<AnimatorDirectionComponent>();
                animDirComponent.dirIndex = dirIndex;
                animDirComponent.dirValue = dirValue;
                entityGO.enabled = false;
                entityGO.enabled = true;
			}
		}
	}
}