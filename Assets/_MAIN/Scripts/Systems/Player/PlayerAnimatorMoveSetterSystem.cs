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

            List<GameObjectEntity> childEntitiesInGame = GameManager.childEntitiesInGame;

			for (int i=0; i<data.Length; i++) 
            {
				Entity entity = data.Entity[i];
				Player player = data.Player[i];
                Parent parent = data.Parent[i];
                AnimatorPlayerMove animatorPlayerMove = data.AnimatorPlayerMove[i];

				commandBuffer.RemoveComponent<AnimatorPlayerMove>(entity);

				int playerAnimToggleValue = player.AnimationToggleValue;
                
				if (playerAnimToggleValue == 0)
				{
                    // player.AttackIndex = 0;
                    // data.Player[i] = player;

                    int dirIndex = animatorPlayerMove.dirIndex;
                    float3 dirValue = animatorPlayerMove.dirValue;
                    
                    int parentEntityIndex = parent.EntityIndex;
                    GameObjectEntity entityGO = childEntitiesInGame[parentEntityIndex];
                    GameObject childGO = entityGO.gameObject;

                    // MOVEMENT
                    // childGO.AddComponent<PlayerAnimationStateComponent>().Value = PlayerAnimationState.MOVE_RUN;
                    childGO.AddComponent<PlayerAnimationMoveRunComponent>();
                    entityGO.enabled = false;
                    entityGO.enabled = true;

                    // DIRECTION
                    // childGO.AddComponent<AnimatorDirectionComponent>();
                    AnimatorDirectionComponent animDirComponent = childGO.AddComponent<AnimatorDirectionComponent>();
                    // AnimatorDirectionComponent animDirComponent = childGO.GetComponent<AnimatorDirectionComponent>();
                    animDirComponent.dirIndex = dirIndex;
                    animDirComponent.dirValue = dirValue;
                    entityGO.enabled = false;
                    entityGO.enabled = true;
                }
			}
		}
	}
}