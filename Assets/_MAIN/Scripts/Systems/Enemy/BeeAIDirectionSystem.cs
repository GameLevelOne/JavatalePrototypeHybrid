using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Unity.Burst;
using System.Collections.Generic;
using UnityRandom = UnityEngine.Random; //AMBIGUOUS ISSUE

namespace Javatale.Prototype
{
	public class BeeAIDirectionSystem : ComponentSystem 
	{
		[BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray entity;
			[ReadOnlyAttribute] public ComponentDataArray<Bee> Bee;
			public ComponentDataArray<EnemyAIDirection> EnemyAIDirection;
			[ReadOnlyAttribute] public ComponentDataArray<Position> Position;
			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
		}
		[InjectAttribute] Data data;

		float3 float3Zero = float3.zero;
		float deltaTime;

		protected override void OnUpdate ()
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;
			List<GameObjectEntity> childEntitiesInGame = GameManager.childEntitiesInGame;

			deltaTime = Time.deltaTime;

			for (int i=0; i<data.Length; i++)
			{
				Entity entity = data.entity[i];
				Bee bee = data.Bee[i];
				EnemyAIDirection enemyAIDirection = data.EnemyAIDirection[i];
				Position position = data.Position[i];
				Parent parent = data.Parent[i];
				
				commandBuffer.RemoveComponent<EnemyAIDirection>(entity);

				float3 targetPos = float3Zero;
				int entityIndex = parent.EntityIndex;
				GameObjectEntity entityGO = childEntitiesInGame[entityIndex];
				GameObject childGO = entityGO.gameObject;

#region Nav Mesh Direction

				float moveRange = bee.MoveRange;
				float posX = position.Value.x;
				float posZ = position.Value.z;
				float randomX = UnityRandom.Range(posX - moveRange, posX + moveRange);
				float randomZ = UnityRandom.Range(posZ - moveRange, posZ + moveRange);
				targetPos = new float3(randomX, 0f, randomZ);
				
				// enemyNavMeshData.TargetPosition = targetPos;
				childGO.AddComponent<NavMeshEventComponent>().Destination = targetPos;
				entityGO.enabled = false;
				entityGO.enabled = true;
#endregion

#region Conventional Direction

				// if (moveDir != vector3Zero)
				// {
				// 	float patrolTimer = bee.PatrolTimer;

				// 	if (patrolTimer <= 0f)
				// 	{
				// 		moveDir = vector3Zero;

				// 		bee.PatrolTimer = bee.MaxPatrolCooldown;
				// 		GameDebug.Log("Set Idle");
				// 	}
				// 	else 
				// 	{
				// 		bee.PatrolTimer -= deltaTime;
				// 	}
				// }
				// else
				// {
				// 	float idleTimer = bee.IdleTimer;

				// 	if (idleTimer <= 0f)
				// 	{
				// 		float randomX = Random.Range(-1,2);
				// 		float randomZ = Random.Range(-1,2);
				// 		moveDir = new Vector3(randomX, 0f, randomZ);

				// 		bee.IdleTimer = bee.MaxIdleCooldown;
				// 		GameDebug.Log("Set Patrol");
				// 	}
				// 	else
				// 	{
				// 		bee.IdleTimer -= deltaTime;
				// 	}
				// }

#endregion
			}
		}
	}
}
