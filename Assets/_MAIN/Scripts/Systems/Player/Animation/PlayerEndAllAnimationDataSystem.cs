using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Burst;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerEndAllAnimationDataSystem : ComponentSystem 
	{
		[BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			public ComponentDataArray<Player> Player;
			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
			[ReadOnlyAttribute] public ComponentDataArray<EndAllAnimationData> EndAllAnimationData;
		}
		[InjectAttribute] private Data data;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;

            List<GameObjectEntity> childEntitiesInGame = GameManager.childEntitiesInGame;

			for (int i=0; i<data.Length; i++)
			{
				Entity entity = data.Entity[i];
				Player player = data.Player[i];
                Parent parent = data.Parent[i];
				EndAllAnimationData endAllAnimationData = data.EndAllAnimationData[i];

                commandBuffer.RemoveComponent<EndAllAnimationData>(entity);

				int playerAnimToggleValue = player.AnimationToggleValue;
                
				if (playerAnimToggleValue == 0)
				{
					int parentEntityIndex = parent.EntityIndex;
					GameObjectEntity entityGO = childEntitiesInGame[parentEntityIndex];
					GameObject childGO = entityGO.gameObject;
					
					int endAllAnimationValue = endAllAnimationData.Value;

					switch (endAllAnimationValue)
					{
						default : // CASE 0		
							player.AttackIndex = 0;
							data.Player[i] = player;

							// childGO.AddComponent<PlayerAnimationStateComponent>().Value = PlayerAnimationState.IDLE_STAND;
							childGO.AddComponent<PlayerAnimationIdleStandComponent>();
							entityGO.enabled = false;
							entityGO.enabled = true;
							
							break;
					}
				}
            }
		}	
	}
}
