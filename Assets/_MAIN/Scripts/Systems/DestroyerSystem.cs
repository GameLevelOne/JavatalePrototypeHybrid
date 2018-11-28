using Unity.Collections;
using Unity.Entities;
// using Unity.Transforms;
using Unity.Burst;
// using UnityEngine;
// using Unity.Mathematics;

namespace Javatale.Prototype
{
	public class DestroyerSystem : ComponentSystem 
	{
		[BurstCompileAttribute]
		public struct Data 
		{
			public readonly int Length;
			[ReadOnlyAttribute] public ComponentArray<DestroyComponent> DestroyComponent;
		}
		[InjectAttribute] private Data data;

		protected override void OnUpdate () 
		{
            for (int i=0; i<data.Length; i++)
			{
				DestroyComponent destroyComponent = data.DestroyComponent[i];

				// Destroy Entity GO
                GameObjectEntity.Destroy(destroyComponent.gameObject);
                UpdateInjectedComponentGroups();
			}
        }
    }
}