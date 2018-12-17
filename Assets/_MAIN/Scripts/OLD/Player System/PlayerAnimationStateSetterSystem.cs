// using Unity.Collections;
// using Unity.Entities;
// // using UnityEngine;
// using Unity.Burst;
// using System.Collections.Generic;

// namespace Javatale.Prototype 
// {
// 	public class PlayerAnimationStateSetterSystem : ComponentSystem 
// 	{
//         [BurstCompileAttribute]
// 		public struct Data
// 		{
// 			public readonly int Length;
// 			[ReadOnlyAttribute] public EntityArray Entity;
// 			[ReadOnlyAttribute] public ComponentArray<ChildComponent> ChildComponent;
// 			[ReadOnlyAttribute] public ComponentArray<PlayerAnimationStateComponent> PlayerAnimationStateComponent;
// 			public ComponentArray<PlayerAnimatorComponent> PlayerAnimatorComponent;
// 		}
// 		[InjectAttribute] private Data data;

// 		protected override void OnUpdate () 
// 		{
// 			EntityCommandBuffer commandBuffer = PostUpdateCommands;

// 			List<Entity> parentEntitiesInGame = GameManager.parentEntitiesInGame;
// 			List<bool> addedStateComponentsInGame = GameManager.addedStateComponentsInGame;

// 			for (int i=0; i<data.Length; i++) {
// 				Entity entity = data.Entity[i];
// 				ChildComponent childComponent = data.ChildComponent[i];
// 				PlayerAnimatorComponent playerAnimatorComponent = data.PlayerAnimatorComponent[i];
// 				PlayerAnimationStateComponent playerAnimationStateComponent = data.PlayerAnimationStateComponent[i];

//                 PlayerAnimationState state = playerAnimationStateComponent.Value;

// 				commandBuffer.RemoveComponent<PlayerAnimationStateComponent>(entity);
// 				GameObjectEntity.Destroy(playerAnimationStateComponent);
//                 // UpdateInjectedComponentGroups();

// 				playerAnimatorComponent.currentState = state;
// 				playerAnimatorComponent.animator.Play(state.ToString());

// 				// Set the Availability of State Component
// 				int entityIndex = childComponent.EntityIndex;
// 				addedStateComponentsInGame[entityIndex] = false;
// 				GameDebug.Log("Check (False) : "+state+" "+GameManager.addedStateComponentsInGame[entityIndex]+"\n====================");

// #region List (OLD)
// 				//SET LIST ANIMATION
// 				// int animIndex = parent.AnimIndex;
// 				// EntryAnimation entryAnim = listAnim[animIndex];
// 				// entryAnim.StartAnimationToggle = 21;
// 				// listAnim[parent.AnimIndex] = entryAnim;
				
// 				//SET LIST PLAYER ANIMATION STATE
// 				// PlayerAnimationState state = PlayerAnimationState.ATTACK_1;
// 				// int playerAnimStateIndex = player.AnimStateIndex;
// 				// listPlayerAnimState[playerAnimStateIndex] = state;
// #endregion
// 			}
// 		}
// 	}
// }