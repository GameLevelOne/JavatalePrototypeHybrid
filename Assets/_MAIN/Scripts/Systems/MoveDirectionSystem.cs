using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Unity.Burst;

namespace Javatale.Prototype 
{
	public class MoveDirectionSystem : JobComponentSystem 
	{
		[BurstCompileAttribute]
		struct MoveJob : IJobProcessComponentData <Position, MoveSpeed, MoveDirection, Parent> 
		{
			public float deltaTime;

			public void Execute (
				ref Position pos, 
				[ReadOnlyAttribute] ref MoveSpeed speed, 
				[ReadOnlyAttribute] ref MoveDirection dir,
				[ReadOnlyAttribute] ref Parent parent) 
			{
				float3 value = pos.Value;

				value += deltaTime * speed.Value * dir.Value;

				pos.Value = value;
			}
		}

		protected override JobHandle OnUpdate (JobHandle inputDeps) 
		{
			MoveJob moveJob = new MoveJob 
			{
				deltaTime = Time.deltaTime
			};

			JobHandle moveHandle = moveJob.Schedule(this, inputDeps);
			
			return moveHandle;
		}
	}
}
