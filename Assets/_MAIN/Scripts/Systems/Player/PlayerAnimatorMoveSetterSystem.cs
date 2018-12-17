using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Burst;
using Unity.Mathematics;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerAnimatorMoveSetterSystem : ComponentSystem 
	{
        [BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			public ComponentDataArray<Player> Player;
			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
			[ReadOnlyAttribute] public ComponentDataArray<AnimatorPlayerMove> AnimatorPlayerMove;
		}
		[InjectAttribute] public Data data;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;
            List<int> entitiesIdleLoopAnimationChecker = GameManager.entitiesIdleLoopAnimationChecker;
            List<GameObjectEntity> childEntitiesInGame = GameManager.childEntitiesInGame;
			// List<bool> addedStateComponentsInGame = GameManager.addedStateComponentsInGame;

			for (int i=0; i<data.Length; i++) 
            {
				Entity entity = data.Entity[i];
				// Player player = data.Player[i];
                Parent parent = data.Parent[i];
                AnimatorPlayerMove animatorPlayerMove = data.AnimatorPlayerMove[i];

				commandBuffer.RemoveComponent<AnimatorPlayerMove>(entity);

				int entityIndex = parent.EntityIndex;
				int entityIdleLoopAnimValue = entitiesIdleLoopAnimationChecker[entityIndex];

				// GameDebug.Log("==========START=========="+"\nCheck if "+entityIdleLoopAnimValue);
				if (entityIdleLoopAnimValue == 0)
				{
					// GameDebug.Log("PAMoveSS "+entityIdleLoopAnimValue);
					int dirIndex = animatorPlayerMove.dirIndex;
					float3 dirValue = animatorPlayerMove.dirValue;
					
					GameObjectEntity entityGO = childEntitiesInGame[entityIndex];
					GameObject childGO = entityGO.gameObject;

					// MOVEMENT
					// childGO.AddComponent<PlayerAnimationStateComponent>().Value = PlayerAnimationState.MOVE_RUN;
					childGO.AddComponent<PlayerAnimationMoveRunComponent>();

					// DIRECTION
					AnimatorDirectionComponent animDirComponent = childGO.AddComponent<AnimatorDirectionComponent>();
					animDirComponent.dirIndex = dirIndex;
					animDirComponent.dirValue = dirValue;
					
					entityGO.enabled = false;
					entityGO.enabled = true;

					entitiesIdleLoopAnimationChecker[entityIndex] = 1;
					// GameDebug.Log("PAMoveSS "+entitiesIdleLoopAnimation[entityIndex]+"\n==========");
				}
			}
		}
	}
}