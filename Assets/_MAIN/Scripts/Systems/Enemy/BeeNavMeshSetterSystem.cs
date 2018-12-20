using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Unity.Burst;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityRandom = UnityEngine.Random; //AMBIGUOUS ISSUE

namespace Javatale.Prototype
{
	public class BeeNavMeshSetterSystem : ComponentSystem 
	{
		[BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			public ComponentDataArray<Bee> Bee;
			[ReadOnlyAttribute] public ComponentDataArray<NavMeshData> NavMeshData;
			[ReadOnlyAttribute] public ComponentDataArray<Position> Position;
			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
			// [ReadOnlyAttribute] public ComponentDataArray<EnemyAIDirection> EnemyAIDirection;
		}
		[InjectAttribute] Data data;

		float3 float3Zero = float3.zero;
		float deltaTime;

		protected override void OnUpdate ()
		{
			// EntityManager manager = World.Active.GetOrCreateManager<EntityManager>();
			EntityCommandBuffer commandBuffer = PostUpdateCommands;
			List<GameObjectEntity> childEntitiesInGame = GameManager.childEntitiesInGame;
			JavataleSettings settings = GameManager.settings;

			deltaTime = Time.deltaTime;

			for (int i=0; i<data.Length; i++)
			{
				Entity entity = data.Entity[i];
				Bee bee = data.Bee[i];
				NavMeshData navMeshData = data.NavMeshData[i];
				Position position = data.Position[i];
				Parent parent = data.Parent[i];
				
				commandBuffer.RemoveComponent<NavMeshData>(entity);
				// commandBuffer.RemoveComponent<EnemyAIDirection>(entity);
				bee.EnemyAIPowerToggle = 1;
				bee.IdleTimer = UnityRandom.Range(settings.enemyMinIdleCooldown, settings.enemyMaxIdleCooldown);
				data.Bee[i] = bee;
				// GameDebug.Log("Set Bee "+bee.MoveRange);

				int entityIndex = parent.EntityIndex;
				GameObjectEntity entityGO = childEntitiesInGame[entityIndex];
				GameObject childGO = entityGO.gameObject;

#region Nav Mesh Direction

				float moveRange = bee.MoveRange;
				float3 targetPos = GetRandomNavMeshDestination(position.Value, moveRange);

				childGO.AddComponent<NavMeshEventComponent>().Destination = targetPos;
				// NavMeshEventComponent navMeshComponent = new NavMeshEventComponent();
				// navMeshComponent.Destination = targetPos;

				/// Add MonoBehaviour Component with null refference on the entity
				// manager.AddComponent(entityGO.Entity, childGO.GetComponent<NavMeshEventComponent>().GetType()); 
				// manager.AddComponent(entityGO.Entity, typeof (NavMeshEventComponent));

				/// Add the GameObjectEntity's entity into EntityManager
				// GameObjectEntity.AddToEntityManager(manager, childGO);

				/// Enabling-Disabling jutsu
				entityGO.enabled = false;
				entityGO.enabled = true;
				// GameDebug.Log("OK Child");

				/// New Reset Data from ComponentSystem
				// data = new Data();
				// break;
				// return;
                UpdateInjectedComponentGroups();

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

		float3 GetRandomNavMeshDestination (float3 currentPos, float moveRange)
		{
			// float posX = currentPos.x;
			// float posZ = currentPos.z;
			// float randomX = UnityRandom.Range(posX - moveRange, posX + moveRange);
			// float randomZ = UnityRandom.Range(posZ - moveRange, posZ + moveRange);
			// float3 targetPos = new float3(randomX, 0f, randomZ);

			float3 randomDirection = UnityRandom.insideUnitSphere * moveRange;
			randomDirection += currentPos;
			NavMeshHit hit;
			NavMesh.SamplePosition(randomDirection, out hit, moveRange, 1);
			float3 targetPos = hit.position;
			// GameDebug.Log("TargetPos "+targetPos);

			return targetPos;
		}
	}
}
