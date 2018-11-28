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
// 	public class PlayerAttackChildDestroyerSystem : ComponentSystem 
// 	{
// 		[BurstCompileAttribute]
// 		public struct ParentData
// 		{
// 			public readonly int Length;
// 			[ReadOnlyAttribute] public EntityArray ParentEntities;
// 			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
// 			[ReadOnlyAttribute] public ComponentDataArray<Projectile> Projectile;
// 		}
// 		[InjectAttribute] private ParentData parentData;

// 		[BurstCompileAttribute]
// 		public struct ChildData 
// 		{
// 			public readonly int Length;
// 			public ComponentArray<PlayerAttackComponent> PlayerAttackComponent;
// 			[ReadOnlyAttribute] public ComponentArray<ChildComponent> ChildComponent;
// 		}
// 		[InjectAttribute] private ChildData childData;

// 		// float deltaTime;

// 		protected override void OnUpdate () 
// 		{
// 			EntityCommandBuffer commandBuffer = PostUpdateCommands;
           	
// 			List<EntryAnimation> listAnim = GameManager.entitiesAnimation;
//             // List<EntryProjectileAnim> listProjectileAnim = GameManager.entitiesProjectileAnim;
// 			// List<int> emptyPosIndexes = GameManager.emptyPosIndexes;
// 			// List<int> emptyProjectileAnimIndexes = GameManager.emptyProjectileAnimIndexes;
			
// 			for (int i=0; i<parentData.Length; i++)
// 			{
// 				Entity entity = parentData.ParentEntities[i];
// 				Parent parent = parentData.Parent[i];
// 				Projectile projectile = parentData.Projectile[i];

// 				int parentAnimIndex = parent.AnimIndex;
// 				EntryAnimation entryAnim = listAnim[parentAnimIndex];
// 				int endAnimToggle = entryAnim.EndAnimationToggle;

// 				if (endAnimToggle == 1)
// 				{
// 					commandBuffer.DestroyEntity(entity);
// 				}

// 			}

// #region OLD
// 			// for (int j=0; j<childData.Length; j++)
// 			// {
// 			// 	PlayerAttackComponent playerAttackComponent = childData.PlayerAttackComponent[j];
// 			// 	ChildComponent childComponent = childData.ChildComponent[j];
				
// 			// 	int childAnimIndex = childComponent.AnimIndex;
// 			// 	bool isInitDestroyed = playerAttackComponent.isInitDestroyed;
// 			// 	bool isDestroyed = playerAttackComponent.isDestroyed;

// 			// 	if (isInitDestroyed && !isDestroyed)
// 			// 	{
// 			// 		//Add pos index to List of empty pos index
// 			// 		emptyPosIndexes.Add(childComponent.PosIndex);

// 			// 		//Add anim index to List of empty anim index
// 			// 		emptyProjectileAnimIndexes.Add(childComponent.AnimIndex);

// 			// 		GameObjectEntity.Destroy(playerAttackComponent.gameObject);
// 			// 		UpdateInjectedComponentGroups();

// 			// 		EntryProjectileAnim entryProjectileAnim = listProjectileAnim[childAnimIndex];
// 			// 		entryProjectileAnim.EndAnimationToggle = 1;

// 			// 		listProjectileAnim[childAnimIndex] = entryProjectileAnim;

// 			// 		// foreach (int a in emptyPosIndexes) GameDebug.Log(a);
// 			// 		isDestroyed = true;
// 			// 	}
// 			// }
// #endregion
// 		}
// 	}
// }
