using Unity.Collections;
using Unity.Entities;
using UnityEngine;
// using Unity.Mathematics;
using Unity.Burst;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerEndHurtAnimationDataSystem : ComponentSystem 
	{
		[BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			public ComponentDataArray<Player> Player;
			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
			[ReadOnlyAttribute] public ComponentDataArray<EndHurtAnimationData> EndHurtAnimationData;
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
				EndHurtAnimationData endHurtAnimationData = data.EndHurtAnimationData[i];

                commandBuffer.RemoveComponent<EndHurtAnimationData>(entity);

                int endHurtAnimationValue = endHurtAnimationData.Value;
                
                switch (endHurtAnimationValue)
                {
                    default : // CASE 0		
                        commandBuffer.AddComponent(entity, new PlayerInputDirection{});
                        commandBuffer.AddComponent(entity, new PlayerInputAttack{});

                        player.AnimationToggleValue = 0;
                        data.Player[i] = player;
                        
                        break;
                }

                int parentEntityIndex = parent.EntityIndex;
                GameObjectEntity entityGO = childEntitiesInGame[parentEntityIndex];
                GameObject childGO = entityGO.gameObject;
                        
                childGO.AddComponent<PlayerAnimationIdleStandComponent>();
                entityGO.enabled = false;
                entityGO.enabled = true;
            }
		}	
	}
}
