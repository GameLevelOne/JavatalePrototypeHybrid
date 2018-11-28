using Unity.Collections;
using Unity.Entities;
// using UnityEngine;
using Unity.Burst;
// using Unity.Mathematics;

namespace Javatale.Prototype 
{
	public class PlayerAnimationAttack1SetterSystem : ComponentSystem 
	{
        [BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			public ComponentDataArray<Player> Player;
			[ReadOnlyAttribute] public ComponentDataArray<AnimationPlayerAttack1> AnimationPlayerAttack1;
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

				commandBuffer.RemoveComponent<AnimationPlayerAttack1>(entity);
				commandBuffer.RemoveComponent<PlayerInputDirection>(entity);
				commandBuffer.RemoveComponent<PlayerInputAttack>(entity);
                
				PlayerAnimationState state = PlayerAnimationState.ATTACK_1;

				playerAnimatorComponent.currentState = state;
				playerAnimatorComponent.animator.Play(state.ToString());

				//SET TO PLAYER (PARENT)
				player.AnimationToggleValue = 1;
				player.State = state;
				data.Player[i] = player;

#region List (OLD)
				//SET LIST ANIMATION
				// int animIndex = parent.AnimIndex;
				// EntryAnimation entryAnim = listAnim[animIndex];
				// entryAnim.StartAnimationToggle = 21;
				// listAnim[parent.AnimIndex] = entryAnim;
				
				//SET LIST PLAYER ANIMATION STATE
				// PlayerAnimationState state = PlayerAnimationState.ATTACK_1;
				// int playerAnimStateIndex = player.AnimStateIndex;
				// listPlayerAnimState[playerAnimStateIndex] = state;
#endregion
			}
		}
	}
}