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
			public ComponentDataArray<MoveDirection> MoveDirection;
			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
			[ReadOnlyAttribute] public ComponentDataArray<AnimatorPlayerSlashAttack> AnimatorPlayerSlashAttack;
			[ReadOnlyAttribute] public ComponentDataArray<PlayerSpawnAttackData> PlayerSpawnAttackData;
			[ReadOnlyAttribute] public ComponentDataArray<PlayerInputDirection> PlayerInputDirection;
			[ReadOnlyAttribute] public ComponentDataArray<PlayerInputAttack> PlayerInputAttack;
		}
		[InjectAttribute] public Data data;

        float3 float3Zero = float3.zero;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;

            List<GameObjectEntity> childEntitiesInGame = GameManager.childEntitiesInGame;

			for (int i=0; i<data.Length; i++) 
            {
				Entity entity = data.Entity[i];
                Player player = data.Player[i];
                MoveDirection moveDir = data.MoveDirection[i];
                Parent parent = data.Parent[i];
                // AnimatorPlayerSlashAttack animatorPlayerSlashAttack = data.AnimatorPlayerSlashAttack[i];
				PlayerSpawnAttackData playerSpawnAttackData = data.PlayerSpawnAttackData[i];

				commandBuffer.RemoveComponent<AnimatorPlayerSlashAttack>(entity);
				commandBuffer.RemoveComponent<PlayerInputDirection>(entity);
				commandBuffer.RemoveComponent<PlayerInputAttack>(entity);

                moveDir.Value = float3Zero;
                data.MoveDirection[i] = moveDir;
                
                player.AnimationToggleValue = 1;
                data.Player[i] = player; 
                
                int parentEntityIndex = parent.EntityIndex;
                GameObjectEntity entityGO = childEntitiesInGame[parentEntityIndex];
                GameObject childGO = entityGO.gameObject;
                
                int attackIndex = playerSpawnAttackData.attackIndex;

                switch (attackIndex)
                {
                    case 1:
                        childGO.AddComponent<PlayerAnimationAttack2Component>();

                        break;
                    case 2:
                        childGO.AddComponent<PlayerAnimationAttack3Component>();

                        break;
                    default: // Case 0
                        childGO.AddComponent<PlayerAnimationAttack1Component>();
                        
                        break;
                }

                entityGO.enabled = false;
                entityGO.enabled = true;
			}
		}
	}
}