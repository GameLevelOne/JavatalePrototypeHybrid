using System;
using Unity.Entities;
using Unity.Mathematics;
// using UnityEngine;


namespace Javatale.Prototype 
{
    [SerializableAttribute]
	public struct PlayerInputDirection : IComponentData
	{
		public float3 Value;
	}
	public class PlayerInputDirectionComponent : ComponentDataWrapper<PlayerInputDirection> {}
}
