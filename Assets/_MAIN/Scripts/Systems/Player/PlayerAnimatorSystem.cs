using Unity.Collections;
using Unity.Entities;
// using UnityEngine;
using Unity.Burst;
using Unity.Mathematics;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerAnimatorSystem : ComponentSystem 
	{
		[BurstCompileAttribute]
		public struct ChildData
		{
			public readonly int Length;
			[ReadOnlyAttribute] public ComponentArray<ChildComponent> Child;
			[ReadOnlyAttribute] public ComponentArray<PlayerAnimatorComponent> Animator;
		}
		[InjectAttribute] private ChildData childData;
		
		protected override void OnUpdate () 
		{
			List<EntryAnimation> listAnim = GameManager.entitiesAnimation;
            // List<EntryPlayerAnim> listAnim = GameManager.entitiesPlayerAnim;
			List<PlayerAnimationState> listPlayerAnimState = GameManager.entitiesPlayerAnimState;

			string faceX = Constants.AnimatorParameter.Float.FACE_X;
			string faceY = Constants.AnimatorParameter.Float.FACE_Y;

			for (int j=0; j<childData.Length; j++)
			{
				ChildComponent child = childData.Child[j];
				PlayerAnimatorComponent anim = childData.Animator[j];

				int childAnimIndex = child.AnimIndex;
				EntryAnimation entryAnim = listAnim[childAnimIndex];
				float3 faceDirValue = entryAnim.FaceDirValue;

				int childAnimStateIndex = child.AnimStateIndex;
				// EntryPlayerAnimState entryPlayerAnimState = listPlayerAnimState[childAnimStateIndex];
				PlayerAnimationState state = listPlayerAnimState[childAnimStateIndex];

#region PLAY & STOP ANIMATION
				int playerStartAnimToggle = entryAnim.StartAnimationToggle;

				if (playerStartAnimToggle != 0)
				{
                    anim.animator.Play(state.ToString());
					anim.currentState = state;
					
					entryAnim.StartAnimationToggle = 0;
					listAnim[childAnimIndex] = entryAnim;
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
			}
		}
	}
}