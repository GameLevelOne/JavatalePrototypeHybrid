using Unity.Collections;
using Unity.Entities;
// using Unity.Mathematics;
using UnityEngine;
using Unity.Burst;

namespace Javatale.Prototype
{
	public class EnemyAIDirectionSystem : ComponentSystem 
	{
		[BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray entity;
			public ComponentDataArray<Bee> Bee;
			public ComponentDataArray<EnemyAIDirection> EnemyAIDirection;
			public ComponentDataArray<MoveDirection> MoveDirection;
			public ComponentDataArray<FaceDirection> FaceDirection;
		}
		[InjectAttribute] Data data;

		Vector3 vector3Zero = Vector3.zero;
		float deltaTime;

		protected override void OnUpdate ()
		{
			deltaTime = Time.deltaTime;

			for (int i=0; i<data.Length; i++)
			{
				Entity entity = data.entity[i];
				Bee bee = data.Bee[i];
				EnemyAIDirection enemyAIDirection = data.EnemyAIDirection[i];
				MoveDirection moveDirection = data.MoveDirection[i];
				FaceDirection faceDirection = data.FaceDirection[i];

				float currentIdleTimer = bee.IdleTimer;
				float currentPatrolTimer = bee.PatrolTimer;
				Vector3 moveDir = moveDirection.Value;

				
				if (currentIdleTimer < 0f) 
				{
					// bee.IdleTimer = bee.MaxIdleCooldown;
					
					if (currentPatrolTimer < 0f)
					{
						bee.IdleTimer = bee.MaxIdleCooldown;

						moveDir = Vector3.zero;
					}
					else if (moveDir == vector3Zero)
					{
						bee.PatrolTimer = bee.MaxPatrolCooldown;
						
						moveDir = new Vector3(Random.value, 0f, Random.value);
					}
					else
					{
						bee.PatrolTimer -= deltaTime;
					}
				}
				else
				{
					bee.IdleTimer -= deltaTime;					
				}

				moveDirection.Value = moveDir;
				data.MoveDirection[i] = moveDirection;
				data.Bee[i] = bee;
			}
		}
	}
}
