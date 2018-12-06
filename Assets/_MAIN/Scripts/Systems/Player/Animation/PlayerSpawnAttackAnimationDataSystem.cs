using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Burst;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerSpawnAttackAnimationDataSystem : ComponentSystem 
	{
		[BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			[ReadOnlyAttribute] public ComponentDataArray<Position> Position;
			[ReadOnlyAttribute] public ComponentDataArray<SpawnAttackAnimationData> SpawnAttackAnimationData;
			[ReadOnlyAttribute] public ComponentDataArray<PlayerSpawnAttackData> PlayerSpawnAttackData;
		}
		[InjectAttribute] private Data data;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;

			JavataleSettings settings = GameManager.settings;
			float3[] playerAttackRanges = settings.playerAttackRanges;
			List<GameObject[]> playerSlashAttackChilds = settings.playerSlashAttackChilds;

			List<Entity> parentEntitiesInGame = GameManager.parentEntitiesInGame;
			List<GameObjectEntity> childEntitiesInGame = GameManager.childEntitiesInGame;

			for (int i=0; i<data.Length; i++)
			{
				Entity entity = data.Entity[i];
				Position position = data.Position[i];
				SpawnAttackAnimationData spawnAttackAnimationData = data.SpawnAttackAnimationData[i];
				PlayerSpawnAttackData playerSpawnAttackData = data.PlayerSpawnAttackData[i];

                commandBuffer.RemoveComponent<SpawnAttackAnimationData>(entity);

                int spawnAttackAnimationValue = spawnAttackAnimationData.Value;
                
                switch (spawnAttackAnimationValue)
                {
                    default : // CASE 0		
						// Position attackInitPos = playerSpawnAttackData.pos;
						MoveDirection attackInitMoveDir = playerSpawnAttackData.moveDir;
						FaceDirection attackInitFaceDir = playerSpawnAttackData.faceDir;
						MoveSpeed attackInitMoveSpeed = playerSpawnAttackData.moveSpeed;
						int attackIndex = playerSpawnAttackData.attackIndex;

#region SPAWN ATTACK GAMEOBJECT

						int attackFaceDirIndex = attackInitFaceDir.DirIndex;
						float3 attackPosValue = position.Value + playerAttackRanges[attackFaceDirIndex];
						
						GameObject attackGO = null;
						
						attackGO = GameObjectEntity.Instantiate(playerSlashAttackChilds[attackIndex][attackFaceDirIndex], attackPosValue, quaternion.identity);

						// attackGO.AddComponent<PositionComponent>().Value = attackInitPos;
						attackGO.AddComponent<MoveDirectionComponent>().Value = attackInitMoveDir;
						attackGO.AddComponent<FaceDirectionComponent>().Value = attackInitFaceDir;
						attackGO.AddComponent<MoveSpeedComponent>().Value = attackInitMoveSpeed;

						attackGO.SetActive(true);

#endregion

#region INSERT ATTACK GAMEOBJECT INTO PARENT & CHILD LIST

						// PARENT
						Entity attackEntity = attackGO.GetComponent<GameObjectEntity>().Entity;

						parentEntitiesInGame.Add(attackEntity);
						int currentParentEntityIndex = parentEntitiesInGame.Count-1;

						commandBuffer.SetComponent(attackEntity, new Parent { EntityIndex = currentParentEntityIndex });

						// CHILD
						ChildComponent childComponent = attackGO.GetComponentInChildren<ChildComponent>();
						GameObjectEntity attackGOEntity = childComponent.GetComponent<GameObjectEntity>();

						childEntitiesInGame.Add(attackGOEntity);
						int currentChildEntityIndex = childEntitiesInGame.Count-1;

						childComponent.EntityIndex = currentChildEntityIndex;

#endregion
                        
                        break;
                }
            }
		}	
	}
}
