using System;
using Unity.Entities;
using Unity.Mathematics;
// using UnityEngine;

namespace Javatale.Prototype 
{
	[SerializableAttribute]
	public struct MoveDirection : IComponentData 
	{
		public float3 Value;
	}

	public class MoveDirectionComponent : ComponentDataWrapper<MoveDirection> {}
}
