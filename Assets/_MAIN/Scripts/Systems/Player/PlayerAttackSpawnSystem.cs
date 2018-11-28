using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Burst;
using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;

namespace Javatale.Prototype
{
	public class PlayerAttackSpawnSystem : ComponentSystem 
	{
		[BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray PlayerAttackEntities;
			[ReadOnlyAttribute] public ComponentDataArray<PlayerAttackSpawnData> PlayerAttackSpawnData;
		}
		[InjectAttribute] private Data data;

		protected override void OnUpdate ()
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;
			JavataleSettings settings = GameManager.settings;
			// Mesh playerAttackMesh = settings.playerAttackMesh;
			// Material[] playerAttackMaterials = settings.playerAttackMaterials;
			List<Entity> listEntities = GameManager.entitiesInGame;
           	List<float3> listPos = GameManager.entitiesPos;
           	List<EntryAnimation> listAnim = GameManager.entitiesAnimation;
			   
			List<int> emptyEntitiesIndexes = GameManager.emptyEntitiesIndexes;
			List<int> emptyPosIndexes = GameManager.emptyPosIndexes;
           	List<int> emptyAnimIndexes = GameManager.emptyAnimIndexes;
			
			for (int i=0; i<data.Length; i++) {
				PlayerAttackSpawnData playerAttackSpawnData = data.PlayerAttackSpawnData[i];
				Entity playerAttackEntity = data.PlayerAttackEntities[i];

				commandBuffer.RemoveComponent<PlayerAttackSpawnData>(playerAttackEntity);

				Position attackInitPos = playerAttackSpawnData.pos;
				Rotation attackInitRot = playerAttackSpawnData.rot;
				MoveDirection attackInitMoveDir = playerAttackSpawnData.moveDir;
				FaceDirection attackInitFaceDir = playerAttackSpawnData.faceDir;
				Parent attackParent = playerAttackSpawnData.parent;
				Projectile attackProjectile= playerAttackSpawnData.projectile;
				int attackIndex = playerAttackSpawnData.attackIndex;

				commandBuffer.AddComponent(playerAttackEntity, attackInitPos);
				commandBuffer.AddComponent(playerAttackEntity, attackInitRot);
				commandBuffer.AddComponent(playerAttackEntity, attackInitMoveDir);
				commandBuffer.AddComponent(playerAttackEntity, attackInitFaceDir);
				commandBuffer.AddComponent(playerAttackEntity, attackParent);
				commandBuffer.AddComponent(playerAttackEntity, attackProjectile);

				#region ENTITY LIST
				int currentEntityListIndex = 0;

				if (emptyEntitiesIndexes.Count > 0)
				{
					int emptyEntityIndex = emptyEntitiesIndexes[0];
					emptyEntitiesIndexes.RemoveAt(0);

					listEntities[emptyEntityIndex] = playerAttackEntity;

					currentEntityListIndex = emptyEntityIndex;
				}
				else 
				{
					listEntities.Add(playerAttackEntity);
					
					currentEntityListIndex = listPos.Count-1;
				}
				#endregion

				#region POS LIST
				float3 attackPosValue = attackInitPos.Value;
				int currentPosListIndex = 0;

				if (emptyPosIndexes.Count > 0)
				{
					//Get empty index from their list, then remove it
					int emptyPosIndex = emptyPosIndexes[0];
					emptyPosIndexes.RemoveAt(0);

					//Set projectile list by its empty index
					listPos[emptyPosIndex] = attackPosValue;

					//Set current index by its empty index
					currentPosListIndex = emptyPosIndex;
				}
				else 
				{
					listPos.Add(attackPosValue);
					
					currentPosListIndex = listPos.Count-1;
				}
				#endregion

				#region ANIM LIST
				int attackFaceDirIndex = attackInitFaceDir.dirIndex;
				float3 attackFaceValue = attackInitFaceDir.Value;
				int currentAnimListIndex = 0;

				if (emptyAnimIndexes.Count > 0) 
				{
					//Get empty index from their list, then remove it
					int emptyProjectileAnimIndex = emptyAnimIndexes[0];
					emptyAnimIndexes.RemoveAt(0);

					//Set projectile list by its empty index
					listAnim[emptyProjectileAnimIndex] = new EntryAnimation(attackFaceDirIndex, attackFaceValue, 1); //START 1

					//Set current index by its empty index
					currentAnimListIndex = emptyProjectileAnimIndex;
				}
				else 
				{
					listAnim.Add(new EntryAnimation(attackFaceDirIndex, attackFaceValue, 1)); //START 1
					
					currentAnimListIndex = listAnim.Count-1;
				}
				#endregion

				commandBuffer.SetComponent<Parent>(playerAttackEntity, new Parent{
					EntityIndex = currentEntityListIndex,
					PosIndex = currentPosListIndex,	
					AnimIndex = currentAnimListIndex
				});

				//=====ATTACK'S CHILD GAMEOBJECT=====
				GameObject attackGO = null;

				switch (attackIndex)
				{
					case 1:
						attackGO = GameObject.Instantiate(settings.playerAttack2Childs[attackFaceDirIndex], attackPosValue, quaternion.identity);
						break;
					case 2:
						attackGO = GameObject.Instantiate(settings.playerAttack3Childs[attackFaceDirIndex], attackPosValue, quaternion.identity);
						break;
					default: //case 0
						attackGO = GameObject.Instantiate(settings.playerAttack1Childs[attackFaceDirIndex], attackPosValue, quaternion.identity);
						break;
				}
				
				attackGO.GetComponent<ChildComponent>().EntityIndex = currentEntityListIndex;
				attackGO.GetComponent<ChildComponent>().PosIndex = currentPosListIndex;
				attackGO.GetComponent<ChildComponent>().AnimIndex = currentAnimListIndex;

				attackGO.SetActive(true);
			}
		}
	}
}