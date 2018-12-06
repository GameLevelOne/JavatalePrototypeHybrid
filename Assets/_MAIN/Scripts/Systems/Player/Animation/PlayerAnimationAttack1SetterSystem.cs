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
			[ReadOnlyAttribute] public ComponentArray<PlayerAnimationAttack1Component> PlayerAnimationAttack1Component;
			public ComponentArray<PlayerAnimatorComponent> PlayerAnimatorComponent;
		}
		[InjectAttribute] private Data data;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;

			for (int i=0; i<data.Length; i++) {
				Entity entity = data.Entity[i];
				PlayerAnimatorComponent playerAnimatorComponent = data.PlayerAnimatorComponent[i];
				PlayerAnimationAttack1Component playerAnimationAttack1Component = data.PlayerAnimationAttack1Component[i];
                
				PlayerAnimationState state = PlayerAnimationState.ATTACK_1;

				commandBuffer.RemoveComponent<PlayerAnimationAttack1Component>(entity);
				GameObjectEntity.Destroy(playerAnimationAttack1Component);
                // UpdateInjectedComponentGroups();

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