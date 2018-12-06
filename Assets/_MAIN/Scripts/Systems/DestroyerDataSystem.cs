using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Burst;
// using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class DestroyerDataSystem : ComponentSystem 
	{
		[BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			public GameObjectArray GameObject;
			[ReadOnlyAttribute] public ComponentDataArray<DestroyedData> DestroyedData;
		}
		[InjectAttribute] private Data data;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;

			for (int i=0; i<data.Length; i++)
			{
                Entity entity = data.Entity[i];
				GameObject gameObject = data.GameObject[i];
				// DestroyedData destroyedData = data.DestroyedData[i];

                commandBuffer.RemoveComponent<DestroyedData>(entity);

				// Destroy Entity and its GO
                commandBuffer.DestroyEntity(entity);
                GameObjectEntity.Destroy(gameObject, 0.01f);
                // UpdateInjectedComponentGroups();
            }
		}	
	}
}
