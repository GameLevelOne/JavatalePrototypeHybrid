using System;
using Unity.Entities;
using UnityEngine;
// using Unity.Transforms;
// using Unity.Mathematics;

namespace Javatale.Prototype 
{
    [SerializableAttribute]
	public struct Player : IComponentData
	{
		public int AnimStateIndex;

		[HeaderAttribute("Current")]
		public PlayerAnimationState State;

		/// <summary>
		/// <para>Values: <br /></para>
		/// <para>0 OFF<br /></para>
		/// <para>1 Idle Stand<br /></para>
		/// <para>2 Idle Run<br /></para>
		/// <para>21 Attack 1<br /></para>
		/// <para>22 Attack 2<br /></para>
		/// <para>41 Normal Hit<br /></para>
		/// </summary>
		public int StartAnimationToggle;
		// public int EndAnimationToggle;
		public int AnimationToggleValue;
		
		/// <summary>
		/// <para>Values: <br /></para>
		/// <para>0 Attack 1<br /></para>
		/// <para>1 Attack 2<br /></para>
		/// <para>2 Attack 3<br /></para>
		/// </summary>
		public int AttackIndex;
	}
	public class PlayerComponent : ComponentDataWrapper<Player> {}
}