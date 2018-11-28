// using System;
using Unity.Entities;
// using Unity.Mathematics;
using UnityEngine;
using System.Collections.Generic;


namespace Javatale.Prototype 
{
	//DIRECTION
	public struct PlayerInputDirection : IComponentData
	{
		public Vector3 Value;
	}
	public class PlayerInputDirectionComponent : ComponentDataWrapper<PlayerInputDirection> {}
    
	//ATTACK
	public struct PlayerInputAttack : IComponentData
	{
		public int Value;
	}
	public class PlayerInputAttackComponent : ComponentDataWrapper<PlayerInputAttack> {}
}
