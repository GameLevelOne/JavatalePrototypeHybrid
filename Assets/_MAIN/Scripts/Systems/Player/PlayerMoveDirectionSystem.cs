using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Unity.Burst;

namespace Javatale.Prototype 
{
	public class PlayerMoveDirectionSystem : ComponentSystem
	{
		[BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public ComponentDataArray<PlayerInputDirection> PlayerInputDirection; //KEY
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

#region JOB (Cannot using Rigidbody)
	// public class MoveDirectionSystem : JobComponentSystem 
	// {
	// 	[BurstCompileAttribute]
	// 	struct MoveJob : IJobProcessComponentData <Position, MoveSpeed, MoveDirection, Parent> 
	// 	{
	// 		public float deltaTime;

	// 		public void Execute (
	// 			ref Position pos, 
	// 			[ReadOnlyAttribute] ref MoveSpeed speed, 
	// 			[ReadOnlyAttribute] ref MoveDirection dir,
	// 			[ReadOnlyAttribute] ref Parent parent) 
	// 		{
	// 			float3 value = pos.Value;

	// 			value += deltaTime * speed.Value * dir.Value;

	// 			pos.Value = value;
	// 		}
	// 	}

	// 	protected override JobHandle OnUpdate (JobHandle inputDeps) 
	// 	{
	// 		MoveJob moveJob = new MoveJob 
	// 		{
	// 			deltaTime = Time.deltaTime
	// 		};

	// 		JobHandle moveHandle = moveJob.Schedule(this, inputDeps);
			
	// 		return moveHandle;
	// 	}
	// }
#endregion
}
