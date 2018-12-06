using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Burst;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerAnimatorHurtSetterSystem : ComponentSystem 
	{
        [BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			public ComponentDataArray<Player> Player;
			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
			[ReadOnlyAttribute] public ComponentDataArray<AnimatorPlayerHurt> AnimatorPlayerHurt;
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
                AnimatorPlayerHurt animatorPlayerHurt = data.AnimatorPlayerHurt[i];

				commandBuffer.RemoveComponent<AnimatorPlayerHurt>(entity);

				int playerAnimToggleValue = player.AnimationToggleValue;

                if (playerAnimToggleValue == 0) 
                {
                    commandBuffer.RemoveComponent<PlayerInputAttack>(entity);
    				commandBuffer.RemoveComponent<PlayerInputDirection>(entity);
                }
                
                player.AttackIndex = 0;
                player.AnimationToggleValue = -1;
                data.Player[i] = player;
                
                int parentEntityIndex = parent.EntityIndex;
                GameObjectEntity entityGO = childEntitiesInGame[parentEntityIndex];
                GameObject childGO = entityGO.gameObject;
                
                int hurtIndex = animatorPlayerHurt.Value;

                switch (hurtIndex)
                {
                    // case 1:
                    //     childGO.AddComponent<PlayerAnimationAttack2Component>();

                    //     break;
                    // case 2:
                    //     childGO.AddComponent<PlayerAnimationAttack3Component>();

                    //     break;
                   default: //Case 0
                        childGO.AddComponent<PlayerAnimationHitHurtComponent>();
                        GameDebug.Log("PlayerAnimationNormalHitHurtComponent");
                        
                        break;
                }
                
                entityGO.enabled = false;
                entityGO.enabled = true;
			}
		}
	}
}