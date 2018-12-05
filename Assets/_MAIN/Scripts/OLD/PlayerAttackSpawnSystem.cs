// using Unity.Collections;
// using Unity.Entities;
// using Unity.Transforms;
// using UnityEngine;
// using Unity.Burst;
// using Unity.Mathematics;

// namespace Javatale.Prototype
// {
// 	public class PlayerAttackSpawnSystem : ComponentSystem 
// 	{
// 		[BurstCompileAttribute]
// 		public struct Data
// 		{
// 			public readonly int Length;
// 			[ReadOnlyAttribute] public EntityArray Entity;
// 			[ReadOnlyAttribute] public ComponentDataArray<PlayerAttackSpawnData> PlayerAttackSpawnData;
// 		}
// 		[InjectAttribute] private Data data;

// 		protected override void OnUpdate ()
// 		{
// 			EntityCommandBuffer commandBuffer = PostUpdateCommands;
//             JavataleSettings settings = GameManager.settings;
			
// 			for (int i=0; i<data.Length; i++) {
// 				PlayerAttackSpawnData playerAttackSpawnData = data.PlayerAttackSpawnData[i];
// 				Entity entity = data.Entity[i];
                
// 				commandBuffer.RemoveComponent<PlayerAttackSpawnData>(entity);

// 				Position attackInitPos = playerAttackSpawnData.pos;
//                 MoveDirection attackInitMoveDir = playerAttackSpawnData.moveDir;
// 				FaceDirection attackInitFaceDir = playerAttackSpawnData.faceDir;
//                 MoveSpeed attackInitMoveSpeed = playerAttackSpawnData.moveSpeed;
// 				int attackIndex = playerAttackSpawnData.attackIndex;

//                 commandBuffer.DestroyEntity(entity);

//                 float3 attackPosValue = attackInitPos.Value;
//                 int attackFaceDirIndex = attackInitFaceDir.DirIndex;
//                 float attackMoveSpeed = attackInitMoveSpeed.Value;

// 				//========== SPAWN ATTACK GAMEOBJECT ==========
// 				GameObject attackGO = null;

// 				switch (attackIndex)
// 				{
// 					case 1:
// 						attackGO = GameObjectEntity.Instantiate(settings.playerAttack2Childs[attackFaceDirIndex], attackPosValue, quaternion.identity);
// 						break;
// 					case 2:
// 						attackGO = GameObjectEntity.Instantiate(settings.playerAttack3Childs[attackFaceDirIndex], attackPosValue, quaternion.identity);
// 						break;
// 					default: //case 0
// 						attackGO = GameObjectEntity.Instantiate(settings.playerAttack1Childs[attackFaceDirIndex], attackPosValue, quaternion.identity);
// 						break;
// 				}

//                 attackGO.AddComponent<PositionComponent>().Value = attackInitPos;
//                 attackGO.AddComponent<MoveDirectionComponent>().Value = attackInitMoveDir;
//                 attackGO.AddComponent<FaceDirectionComponent>().Value = attackInitFaceDir;
//                 attackGO.AddComponent<MoveSpeedComponent>().Value = attackInitMoveSpeed;


// 				attackGO.SetActive(true);
// 			}
// 		}
// 	}
// }