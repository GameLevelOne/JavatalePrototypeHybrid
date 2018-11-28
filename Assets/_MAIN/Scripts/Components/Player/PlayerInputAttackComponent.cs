using System;
using Unity.Entities;
// using Unity.Mathematics;
// using UnityEngine;


namespace Javatale.Prototype 
{
    [SerializableAttribute]
	public struct PlayerInputAttack : IComponentData
	{
		public int Value;
	}
	public class PlayerInputAttackComponent : ComponentDataWrapper<PlayerInputAttack> {}
}
