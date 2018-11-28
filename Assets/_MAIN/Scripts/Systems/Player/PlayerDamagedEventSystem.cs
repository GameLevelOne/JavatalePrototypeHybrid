using Unity.Collections;
using Unity.Entities;
using UnityEngine;
// using Unity.Mathematics;
using Unity.Burst;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerDamagedEventSystem : ComponentSystem 
	{
		
        [BurstCompileAttribute]
		public struct Data 
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entities;
			[ReadOnlyAttribute] public ComponentArray<ChildComponent> Child;
            [ReadOnlyAttribute] public ComponentArray<PlayerColliderComponent> PlayerColliderComponent;
            public ComponentArray<DamagedEventComponent> DamagedEventComponent;
		}
		[InjectAttribute] Data data;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;
			List<Entity> entitiesInGame = GameManager.entitiesInGame;

			for (int i=0; i<data.Length; i++) 
			{
				Entity entity = data.Entities[i];
				ChildComponent child = data.Child[i];
				PlayerColliderComponent playerColliderComponent = data.PlayerColliderComponent[i];
				DamagedEventComponent damagedEventComponent = data.DamagedEventComponent[i];
                
				int childEntityIndex = child.EntityIndex;
				EntryDamage entryDamage = damagedEventComponent.entryDamage;
				float damageValue = entryDamage.Value;
				int damageType = entryDamage.Type;

				commandBuffer.RemoveComponent<DamagedEventComponent>(entity);
				GameObject.Destroy(damagedEventComponent);
                UpdateInjectedComponentGroups();

				commandBuffer.AddComponent(entitiesInGame[childEntityIndex], new DamagedData { Value = damageValue, Type = damageType });

				playerColliderComponent.isCheckOnDamaged = false;
			}
		}
	}
}
