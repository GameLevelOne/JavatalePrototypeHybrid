using Unity.Collections;
using Unity.Entities;
// using UnityEngine;
using Unity.Burst;
// using Unity.Mathematics;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerAnimationHitHurtSetterSystem : ComponentSystem 
	{
        [BurstCompileAttribute]
		public struct ParentData
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray AnimationIdleEntities;
			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
			public ComponentDataArray<Player> Player;
			[ReadOnlyAttribute] public ComponentDataArray<FaceDirection> FaceDirection;
			[ReadOnlyAttribute] public ComponentDataArray<AnimationPlayerHitHurt> AnimationPlayerNormalHit;
		}
		[InjectAttribute] private ParentData parentData;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;
			List<EntryAnimation> listAnim = GameManager.entitiesAnimation;
            // List<EntryPlayerAnim> listAnim = GameManager.entitiesPlayerAnim;
			List<PlayerAnimationState> listPlayerAnimState = GameManager.entitiesPlayerAnimState;

			for (int i=0; i<parentData.Length; i++) {
				Entity animEntity = parentData.AnimationIdleEntities[i];
				Parent parent = parentData.Parent[i];
				Player player = parentData.Player[i];
				FaceDirection faceDir = parentData.FaceDirection[i];

				commandBuffer.RemoveComponent<AnimationPlayerHitHurt>(animEntity);
                
				//SET LIST ANIMATION
				int animIndex = parent.AnimIndex;
				EntryAnimation entryAnim = listAnim[animIndex];
				entryAnim.StartAnimationToggle = 41;

				listAnim[animIndex] = entryAnim;
				
				//SET LIST PLAYER ANIMATION STATE
				PlayerAnimationState state = PlayerAnimationState.HIT_HURT;

				int playerAnimStateIndex = player.AnimStateIndex;
				// EntryPlayerAnimState entryPlayerAnimState = listPlayerAnimState[playerAnimStateIndex];
				// entryPlayerAnimState.State = state;

				listPlayerAnimState[playerAnimStateIndex] = state;

				//SET TO PLAYER (PARENT)	
                player.AnimationToggleValue = 0; // INTERUPT ATTACK
				player.AttackIndex = 0;	
				player.State = state;
				parentData.Player[i] = player;
			}
		}
	}
}