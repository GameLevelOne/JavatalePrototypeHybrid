using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Burst;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerAnimatorIdleSetterSystem : ComponentSystem 
	{
        [BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			[ReadOnlyAttribute] public ComponentDataArray<Player> Player;
			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
			[ReadOnlyAttribute] public ComponentDataArray<AnimatorPlayerIdle> AnimatorPlayerIdle;
		}
		[InjectAttribute] public Data data;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;

            List<GameObjectEntity> childEntitiesInGame = GameManager.childEntitiesInGame;

			for (int i=0; i<data.Length; i++) 
            {
				Entity entity = data.Entity[i];
				Player player = data.Player[i];
                Parent parent = data.Parent[i];
                AnimatorPlayerIdle animatorPlayerIdle = data.AnimatorPlayerIdle[i];

				commandBuffer.RemoveComponent<AnimatorPlayerIdle>(entity);

				int playerAnimToggleValue = player.AnimationToggleValue;
                
				if (playerAnimToggleValue == 0)
				{
					int parentEntityIndex = parent.EntityIndex;
					GameObjectEntity entityGO = childEntitiesInGame[parentEntityIndex];
					GameObject childGO = entityGO.gameObject;

					// IDLE
					// childGO.AddComponent<PlayerAnimationStateComponent>().Value = PlayerAnimationState.IDLE_STAND;
					childGO.AddComponent<PlayerAnimationIdleStandComponent>();
					entityGO.enabled = false;
					entityGO.enabled = true;
				}
			}
		}
	}
}