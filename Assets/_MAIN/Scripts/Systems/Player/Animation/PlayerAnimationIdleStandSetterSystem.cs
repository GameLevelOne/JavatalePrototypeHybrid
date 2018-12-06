using Unity.Collections;
using Unity.Entities;
// using UnityEngine;
using Unity.Burst;
// using Unity.Mathematics;

namespace Javatale.Prototype 
{
	public class PlayerAnimationIdleStandSetterSystem : ComponentSystem 
	{
        [BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			[ReadOnlyAttribute] public ComponentArray<PlayerAnimationIdleStandComponent> PlayerAnimationIdleStandComponent;
			public ComponentArray<PlayerAnimatorComponent> PlayerAnimatorComponent;
		}
		[InjectAttribute] private Data data;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;

			for (int i=0; i<data.Length; i++) {
				Entity entity = data.Entity[i];
				PlayerAnimatorComponent playerAnimatorComponent = data.PlayerAnimatorComponent[i];
				PlayerAnimationIdleStandComponent playerAnimationIdleStandComponent = data.PlayerAnimationIdleStandComponent[i];
                
				PlayerAnimationState state = PlayerAnimationState.IDLE_STAND;

				commandBuffer.RemoveComponent<PlayerAnimationIdleStandComponent>(entity);
				GameObjectEntity.Destroy(playerAnimationIdleStandComponent);
                // UpdateInjectedComponentGroups();

				playerAnimatorComponent.currentState = state;
				playerAnimatorComponent.animator.Play(state.ToString());

				// //SET TO PLAYER	
				// player.AttackIndex = 0;	
				// player.State = state;
				// data.Player[i] = player;

#region LIST (OLD)
				//SET LIST ANIMATION
				// int animIndex = parent.AnimIndex;
				// EntryAnimation entryAnim = listAnim[animIndex];
				// entryAnim.StartAnimationToggle = 1;
				// listAnim[animIndex] = entryAnim;
				
				//SET LIST PLAYER ANIMATION STATE
				// PlayerAnimationState state = PlayerAnimationState.IDLE_STAND;
				// int playerAnimStateIndex = player.AnimStateIndex;
				// listPlayerAnimState[playerAnimStateIndex] = state;
#endregion
			}
		}
	}
}