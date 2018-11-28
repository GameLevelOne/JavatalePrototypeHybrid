using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Burst;
using Unity.Mathematics;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class BeeAnimatorSystem : ComponentSystem 
	{
		[BurstCompileAttribute]
		public struct ChildData
		{
			public readonly int Length;
			[ReadOnlyAttribute] public ComponentArray<ChildComponent> Child;
			[ReadOnlyAttribute] public ComponentArray<BeeAnimatorComponent> Animator;
		}
		[InjectAttribute] private ChildData childData;

		protected override void OnUpdate () 
		{
            List<EntryAnimation> listAnim = GameManager.entitiesAnimation;
            List<BeeAnimationState> listBeeAnimState = GameManager.entitiesBeeAnimState;
			
			string faceX = Constants.AnimatorParameter.Float.FACE_X;
			string faceY = Constants.AnimatorParameter.Float.FACE_Y;

			for (int i=0; i<childData.Length; i++)
			{
				ChildComponent child = childData.Child[i];
				BeeAnimatorComponent anim = childData.Animator[i];

				int childAnimIndex = child.AnimIndex;
				EntryAnimation entryAnim = listAnim[childAnimIndex];
				float3 faceDirValue = entryAnim.FaceDirValue;
				
				int childBeeAnimStateIndex = child.AnimStateIndex;
				// EntryBeeAnimState entryBeeAnimState = listBeeAnimState[childBeeAnimStateIndex];
				BeeAnimationState state = listBeeAnimState[childBeeAnimStateIndex];

				// int dirIndex = entryAnim.DirIndex;
				// int currentDirIndex = anim.currentDirIndex;
				// BeeAnimationState currentState = anim.currentState;

#region PLAY & STOP ANIMATION
				int beeStartAnimToggle = entryAnim.StartAnimationToggle;

				// if (state != currentState)
				if (beeStartAnimToggle != 0)
				{
                    anim.animator.Play(state.ToString());
					anim.currentState = state;

					entryAnim.StartAnimationToggle = 0;
					listAnim[childAnimIndex] = entryAnim;
				}
				else
				{
					//
				}
#endregion

#region DIRECTION
				int dirIndex = entryAnim.DirIndex;
				int currentDirIndex = anim.currentDirIndex;
				
				if (dirIndex != currentDirIndex) 
				{
					anim.animator.SetFloat(faceX, faceDirValue.x);
                    anim.animator.SetFloat(faceY, faceDirValue.z);
                    
					anim.currentDirIndex = dirIndex;
					anim.currentFaceDirValue = faceDirValue;
				}
#endregion

#region OLD		
				// if (dirIndex != currentDirIndex) 
				// {
                //     float2 facingValues = float2.zero;

				// 	switch (dirIndex) 
				// 	{
				// 		case 1: //DOWN
                //             facingValues = new float2 (0f, -1f);
                //             break;
				// 		case 2: //LEFT
                //             facingValues = new float2 (-1f, 0f);
                //             break;
				// 		case 3: //UP
                //             facingValues = new float2 (0f, 1f);
                //             break;
				// 		case 4: //RIGHT
                //             facingValues = new float2 (1f, 0f);
                //             break;
				// 	}

				// 	anim.animator.SetFloat(Constants.AnimatorParameter.Float.FACE_X, facingValues.x);
                //     anim.animator.SetFloat(Constants.AnimatorParameter.Float.FACE_Y, facingValues.y);
                    
				// 	anim.currentDirIndex = dirIndex;
				// }
#endregion
			}
		}
	}
}