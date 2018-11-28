using Unity.Collections;
using Unity.Entities;
// using UnityEngine;
using Unity.Burst;
// using Unity.Mathematics;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class BeeAnimationMoveFlySetterSystem : ComponentSystem 
	{
        [BurstCompileAttribute]
		public struct ParentData
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray AnimationIdleEntities;
			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
			public ComponentDataArray<Bee> Bee;
			[ReadOnlyAttribute] public ComponentDataArray<FaceDirection> FaceDirection;
			public ComponentDataArray<AnimationBeeMoveFly> AnimationBeeMoveFly;
		}
		[InjectAttribute] private ParentData parentData;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;
			List<EntryAnimation> listAnim = GameManager.entitiesAnimation;
			List<BeeAnimationState> listBeeAnimState = GameManager.entitiesBeeAnimState;

			for (int i=0; i<parentData.Length; i++) {
				Entity animEntity = parentData.AnimationIdleEntities[i];
				Parent parent = parentData.Parent[i];
				Bee bee = parentData.Bee[i];
				FaceDirection faceDir = parentData.FaceDirection[i];

				commandBuffer.RemoveComponent<AnimationBeeMoveFly>(animEntity);
                
				//SET LIST ANIMATION
				int animIndex = parent.AnimIndex;
				EntryAnimation entryAnim = listAnim[animIndex];
				entryAnim.StartAnimationToggle = 2;

				listAnim[animIndex] = entryAnim;
				
				//SET LIST BEE ANIMATION STATE
				BeeAnimationState state = BeeAnimationState.MOVE_FLY;

				int beeAnimStateIndex = bee.AnimStateIndex;
				// EntryBeeAnimState entryBeeAnimState = listBeeAnimState[beeAnimStateIndex];
				// entryBeeAnimState.State = state;

				listBeeAnimState[beeAnimStateIndex] = state;

				//SET TO BEE (PARENT)	
				// bee.AttackIndex = 0;	
				bee.State = state;
				parentData.Bee[i] = bee;
			}
		}
	}
}