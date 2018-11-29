using Unity.Collections;
using Unity.Entities;
// using UnityEngine;
using Unity.Burst;
// using Unity.Mathematics;

namespace Javatale.Prototype 
{
	public class PlayerAnimationAttack3SetterSystem : ComponentSystem 
	{
        [BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			public ComponentDataArray<Player> Player;
			[ReadOnlyAttribute] public ComponentDataArray<AnimationPlayerAttack3> AnimationPlayerAttack3;
			public ComponentArray<PlayerAnimatorComponent> PlayerAnimatorComponent;
		}
		[InjectAttribute] private Data data;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;

			for (int i=0; i<data.Length; i++) {
				Entity Entity = data.Entity[i];
				Player player = data.Player[i];
				PlayerAnimatorComponent playerAnimatorComponent = data.PlayerAnimatorComponent[i];

				commandBuffer.RemoveComponent<AnimationPlayerAttack3>(Entity);
				commandBuffer.RemoveComponent<PlayerInputDirection>(Entity);
				commandBuffer.RemoveComponent<PlayerInputAttack>(Entity);
                
				PlayerAnimationState state = PlayerAnimationState.ATTACK_3;

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
				// entryAnim.StartAnimationToggle = 23;
				// listAnim[parent.AnimIndex] = entryAnim;
				
				//SET LIST PLAYER ANIMATION STATE
				// PlayerAnimationState state = PlayerAnimationState.ATTACK_3;
				// int playerAnimStateIndex = player.AnimStateIndex;
				// listPlayerAnimState[playerAnimStateIndex] = state;
#endregion
			}
		}
	}
}