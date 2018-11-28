// using Unity.Collections;
// using Unity.Entities;
// // using UnityEngine;
// using Unity.Burst;
// using Unity.Mathematics;
// // using System.Collections.Generic;

// namespace Javatale.Prototype 
// {
// 	public class PlayerFaceDirectionSystem : ComponentSystem 
// 	{
// 		[BurstCompileAttribute]
// 		public struct Data
// 		{
// 			public readonly int Length;
// 			[ReadOnlyAttribute] public ComponentDataArray<FaceDirection> FaceDirection;
// 			public ComponentArray<PlayerAnimatorComponent> PlayerAnimatorComponent;
// 		}
// 		[InjectAttribute] private Data data;
		
// 		protected override void OnUpdate () 
// 		{
// 			string faceX = Constants.AnimatorParameter.Float.FACE_X;
// 			string faceY = Constants.AnimatorParameter.Float.FACE_Y;

// 			for (int i=0; i<data.Length; i++)
// 			{
// 				FaceDirection faceDirection = data.FaceDirection[i];
// 				PlayerAnimatorComponent playerAnimatorComponent = data.PlayerAnimatorComponent[i];

// 				float3 faceDirValue = faceDirection.Value;
// 				int dirIndex = faceDirection.dirIndex;

// 				int currentDirIndex = playerAnimatorComponent.currentDirIndex;
				
// 				if (dirIndex != currentDirIndex) 
// 				{
// 					playerAnimatorComponent.animator.SetFloat(faceX, faceDirValue.x);
//                     playerAnimatorComponent.animator.SetFloat(faceY, faceDirValue.z);
                    
// 					playerAnimatorComponent.currentDirIndex = dirIndex;
// 					playerAnimatorComponent.currentFaceDirValue = faceDirValue;
// 				}
// 			}
// 		}
// 	}
// }