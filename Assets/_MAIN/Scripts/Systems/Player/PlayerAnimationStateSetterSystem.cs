using Unity.Collections;
using Unity.Entities;
// using UnityEngine;
using Unity.Burst;

namespace Javatale.Prototype 
{
	public class PlayerAnimationStateSetterSystem : ComponentSystem 
	{
        [BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			[ReadOnlyAttribute] public ComponentArray<PlayerAnimationStateComponent> PlayerAnimationStateComponent;
			public ComponentArray<PlayerAnimatorComponent> PlayerAnimatorComponent;
		}
		[InjectAttribute] private Data data;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;

			for (int i=0; i<data.Length; i++) {
				Entity entity = data.Entity[i];
				PlayerAnimatorComponent playerAnimatorComponent = data.PlayerAnimatorComponent[i];
				PlayerAnimationStateComponent playerAnimationStateComponent = data.PlayerAnimationStateComponent[i];

                PlayerAnimationState state = playerAnimationStateComponent.Value;

				commandBuffer.RemoveComponent<PlayerAnimationStateComponent>(entity);
				GameObjectEntity.Destroy(playerAnimationStateComponent);

				playerAnimatorComponent.currentState = state;
				playerAnimatorComponent.animator.Play(state.ToString());

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