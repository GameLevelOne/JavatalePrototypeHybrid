using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Burst;
using Unity.Mathematics;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerAnimatorSlashAttackSetterSystem : ComponentSystem 
	{
        [BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			public ComponentDataArray<Player> Player;
			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
			[ReadOnlyAttribute] public ComponentDataArray<AnimatorPlayerSlashAttack> AnimatorPlayerSlashAttack;
			[ReadOnlyAttribute] public ComponentDataArray<PlayerInputDirection> PlayerInputDirection;
			[ReadOnlyAttribute] public ComponentDataArray<PlayerInputAttack> PlayerInputAttack;
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
                AnimatorPlayerSlashAttack animatorPlayerSlashAttack = data.AnimatorPlayerSlashAttack[i];

				commandBuffer.RemoveComponent<AnimatorPlayerSlashAttack>(entity);
				commandBuffer.RemoveComponent<PlayerInputDirection>(entity);
				commandBuffer.RemoveComponent<PlayerInputAttack>(entity);
                
                // float3 posValue = animatorPlayerSlashAttack.pos.Value;
				// float3 moveDirValue = animatorPlayerSlashAttack.moveDir.Value;
				// float3 faceDirValue = animatorPlayerSlashAttack.faceDir.Value;
				// float moveSpeed = animatorPlayerSlashAttack.moveSpeed.Value;
                
                int parentEntityIndex = parent.EntityIndex;
                int attackIndex = player.AttackIndex;
                GameObjectEntity entityGO = childEntitiesInGame[parentEntityIndex];
                GameObject childGO = entityGO.gameObject;

                switch (attackIndex)
                {
                    case 0:
                        attackIndex++;
                        player.AttackIndex = attackIndex;
                        data.Player[i] = player;

                        childGO.AddComponent<PlayerAnimationStateComponent>().Value = PlayerAnimationState.ATTACK_1;
                        entityGO.enabled = false;
                        entityGO.enabled = true;
                        break;
                    case 1:
                        // commandBuffer.AddComponent(childEntitiesInGame[parentEntityIndex], new AnimationPlayerAttack2());
                        
                        // entityGO.AddComponent<PlayerAnimationAttack2Component>();
                        break;
                    case 2:
                        // commandBuffer.AddComponent(childEntitiesInGame[parentEntityIndex], new AnimationPlayerAttack3());
                        // entityGO.AddComponent<PlayerAnimationAttack3Component>();
                        attackIndex = -1;
                        break;
                }
			}
		}
	}
}