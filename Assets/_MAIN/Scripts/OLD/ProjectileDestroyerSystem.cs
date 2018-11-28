// using Unity.Collections;
// using Unity.Entities;
// // using Unity.Transforms;
// // using Unity.Rendering;
// using Unity.Burst;
// // using UnityEngine;
// using System.Collections.Generic;
// // using Unity.Mathematics;

// namespace Javatale.Prototype
// {
// 	public class ProjectileDestroyerSystem : ComponentSystem 
// 	{
// 		[BurstCompileAttribute]
// 		public struct Data 
// 		{
// 			public readonly int Length;
// 			[ReadOnlyAttribute] public EntityArray ParentEntities;
// 			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
//             [ReadOnlyAttribute] public ComponentDataArray<Projectile> Projectile;
// 		}
// 		[InjectAttribute] private Data data;

// 		protected override void OnUpdate () 
// 		{
// 			EntityCommandBuffer commandBuffer = PostUpdateCommands;
// 			List<EntryAnimation> listAnim = GameManager.entitiesAnimation;

//             for (int i=0; i<data.Length; i++)
// 			{
// 				Entity entity = data.ParentEntities[i];
// 				Parent parent = data.Parent[i];

// 				int parentAnimIndex = parent.AnimIndex;
// 				EntryAnimation entryAnim = listAnim[parentAnimIndex];
// 				int endAnimToggle = entryAnim.EndAnimationToggle;

// 				if (endAnimToggle == 1)
// 				{
// 					commandBuffer.DestroyEntity(entity);
// 				}
// 			}
//         }
//     }
// }