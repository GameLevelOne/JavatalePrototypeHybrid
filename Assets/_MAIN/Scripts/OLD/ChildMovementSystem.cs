// using Unity.Collections;
// using Unity.Entities;
// using UnityEngine;
// // using Unity.Jobs;
// using Unity.Mathematics;
// // using Unity.Rendering;
// using Unity.Transforms;
// // using UnityEngine;
// using Unity.Burst;
// using System.Collections.Generic;

// namespace Javatale.Prototype 
// {
// 	public class ChildMovementSystem : ComponentSystem 
// 	{
//         [BurstCompileAttribute]
// 		public struct ParentData 
// 		{
// 			public readonly int Length;
// 			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
//             [ReadOnlyAttribute] public ComponentDataArray<Position> ParentPosition;
// 		}
// 		[InjectAttribute] ParentData parentData;
		
//         [BurstCompileAttribute]
// 		public struct ChildData 
// 		{
// 			public readonly int Length;
// 			[ReadOnlyAttribute] public ComponentArray<ChildComponent> Child;
//             public ComponentArray<Transform> ChildTransform;
// 		}
// 		[InjectAttribute] ChildData childData;

// 		protected override void OnUpdate () 
// 		{
//            List<float3> listPos = GameManager.entitiesPos;

// 			for (int i=0; i<parentData.Length; i++) 
// 			{
// 				Parent parent = parentData.Parent[i];
// 				Position position = parentData.ParentPosition[i];
                
//                 //SET PARENT POSITION
//                 float3 parentPosValue = position.Value;
//                 listPos[parent.PosIndex] = parentPosValue;
// 			}

// 			for (int j=0; j<childData.Length; j++) 
// 			{
// 				ChildComponent child = childData.Child[j];
//                 Transform ChildTransform = childData.ChildTransform[j];
                
//                 //GET PARENT POSITION
//                 float3 parentPosValue = listPos[child.PosIndex];
//                 ChildTransform.position = parentPosValue;
// 			}
// 		}
// 	}
// }
