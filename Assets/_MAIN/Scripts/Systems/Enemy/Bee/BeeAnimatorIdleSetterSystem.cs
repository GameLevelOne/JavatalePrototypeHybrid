using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Burst;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class BeeAnimatorIdleSetterSystem : ComponentSystem 
	{
        [BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			public ComponentDataArray<Bee> Bee;
			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
			[ReadOnlyAttribute] public ComponentDataArray<AnimatorBeeIdle> AnimatorBeeIdle;
		}
		[InjectAttribute] public Data data;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;
			List<int> entitiesAnimationToggle = GameManager.entitiesAnimationToggle;
            List<GameObjectEntity> childEntitiesInGame = GameManager.childEntitiesInGame;

			for (int i=0; i<data.Length; i++) 
            {
				Entity entity = data.Entity[i];
				Bee bee = data.Bee[i];
                Parent parent = data.Parent[i];
                AnimatorBeeIdle animatorBeeIdle = data.AnimatorBeeIdle[i];

				commandBuffer.RemoveComponent<AnimatorBeeIdle>(entity);

				int entityIndex = parent.EntityIndex;
					
                GameObjectEntity entityGO = childEntitiesInGame[entityIndex];
                GameObject childGO = entityGO.gameObject;

                // IDLE
                childGO.AddComponent<BeeAnimationIdleFlyComponent>();
                entityGO.enabled = false;
                entityGO.enabled = true;
				// return;
                UpdateInjectedComponentGroups();
			}
		}
	}
}