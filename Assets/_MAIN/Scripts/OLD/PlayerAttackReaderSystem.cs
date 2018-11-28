// using Unity.Collections;
// using Unity.Entities;
// using UnityEngine;
// using System.Collections.Generic;
// using Unity.Transforms;

// namespace Javatale.Prototype 
// {
// 	public class PlayerAttackReaderSystem : ComponentSystem 
// 	{
// 		public struct Data
// 		{
// 			public readonly int Length;
// 			[ReadOnlyAttribute] public ComponentDataArray<FaceDirection> FaceDirection;
// 			[ReadOnlyAttribute] public ComponentDataArray<PlayerInputAttack> PlayerInputAttack;
// 		}
// 		[InjectAttribute] private Data data;

// 		protected override void OnUpdate () 
// 		{
// 			EntityCommandBuffer commandBuffer = PostUpdateCommands;
//             Vector3 worldToCameraRotation = GameManager.settings.worldToCameraRotation;
//             EntityArchetype playerAttack1Archetype = GameManager.playerAttack1Archetype;
//             PlayerAttack1SpawnData playerAttack1SpawnData; 

// 			for (int i=0; i<data.Length; i++)
// 			{
// 				PlayerInputAttack playerInputAttack = data.PlayerInputAttack[i];
//                 FaceDirection playerDir = data.FaceDirection[i];

//                 List<Vector3> attackPositions = playerInputAttack.InitPositions;

// 				for (int j=0; j<attackPositions.Count; j++)  {
//                     playerAttack1SpawnData.pos = new Position
//                     {
//                         Value = attackPositions[j]
//                     };
//                     playerAttack1SpawnData.rot = new Rotation
//                     {
//                         Value = Quaternion.Euler(worldToCameraRotation)
//                     };
//                     playerAttack1SpawnData.dir = new MoveDirection { Value = playerDir.Value };

//                     commandBuffer.CreateEntity(playerAttack1Archetype);
//                     commandBuffer.SetComponent(playerAttack1SpawnData);
//                 }
// 			}
// 		}	
// 	}
// }
