using System;
using Unity.Entities;
using UnityEngine;
// using Unity.Transforms;
// using Unity.Mathematics;

namespace Javatale.Prototype
{
	[SerializableAttribute]
	public struct Bee : IComponentData
	{
		// public int AnimStateIndex;
		public float MoveRange;
		public float MaxIdleCooldown;
		public float MaxPatrolCooldown;

		[HeaderAttribute("Current")]
		// public BeeAnimationState State;

		/// <summary>
		/// <para>Values: <br /></para>
		/// <para>-1 Hurt<br /></para>
		/// <para>0 OFF<br /></para>
		/// <para>1 Attack<br /></para>
		/// </summary>
		public int AnimationToggleValue;
		
		public float IdleTimer;
		public float PatrolTimer;
	}
	public class BeeComponent : ComponentDataWrapper<Bee> {}
}
