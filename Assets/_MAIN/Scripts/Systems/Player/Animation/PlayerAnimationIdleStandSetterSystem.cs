using Unity.Collections;
using Unity.Entities;
// using UnityEngine;
using Unity.Burst;
// using Unity.Mathematics;
// using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerAnimationIdleStandSetterSystem : ComponentSystem 
	{
        [BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			public ComponentDataArray<Player> Player;
			[ReadOnlyAttribute] public ComponentDataArray<AnimationPlayerIdleStand> AnimationPlayerIdle;
			public ComponentArray<PlayerAnimatorComponent> PlayerAnimatorComponent;
		}
		[InjectAttribute] private Data data;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;

			for (int i=0; i<data.Length; i++) {
				Entity entity = data.Entity[i];
				Player player = data.Player[i];
				PlayerAnimatorComponent playerAnimatorComponent = data.PlayerAnimatorComponent[i];

				commandBuffer.RemoveComponent<AnimationPlayerIdleStand>(entity);
                
				PlayerAnimationState state = PlayerAnimationState.IDLE_STAND;

				playerAnimatorComponent.currentState = state;
				playerAnimatorComponent.animator.Play(state.ToString());

				//SET TO PLAYER	
				player.AttackIndex = 0;	
				player.State = state;
				data.Player[i] = player;

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