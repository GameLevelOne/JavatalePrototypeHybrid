using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Unity.Burst;

namespace Javatale.Prototype 
{
	public class EnemyMoveDirectionSystem : ComponentSystem
	{
		[BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public ComponentDataArray<EnemyAIDirection> EnemyAIDirection;
			[ReadOnlyAttribute] public ComponentDataArray<MoveDirection> MoveDirection;
			[ReadOnlyAttribute] public ComponentDataArray<MoveSpeed> MoveSpeed;
			public ComponentArray<Rigidbody> Rigidbody;
		}
		[InjectAttribute] private Data data;

		float deltaTime;
		
		protected override void OnUpdate () {
			deltaTime = Time.deltaTime;

			for (int i=0; i<data.Length; i++)
			{
				MoveDirection moveDir = data.MoveDirection[i];
				MoveSpeed moveSpeed = data.MoveSpeed[i];
				Rigidbody rb = data.Rigidbody[i];

				float3 dir = moveDir.Value;
				float speed = moveSpeed.Value;

				rb.velocity = dir * speed * deltaTime;
			}
		}
	}
}
