using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Unity.Transforms;
using Unity.Burst;

namespace Javatale.Prototype 
{
	public class PlayerPositionSystem : ComponentSystem
	{
		[BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			public ComponentDataArray<Position> Position; //KEY
			[ReadOnlyAttribute] public ComponentArray<Transform> Transform;
		}
		[InjectAttribute] private Data data;
		
		protected override void OnUpdate () {
			for (int i=0; i<data.Length; i++)
			{
				Position position = data.Position[i];
				Transform transform = data.Transform[i];

                position.Value = transform.position;
                data.Position[i] = position;
			}
		}
	}
}