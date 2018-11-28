// using System;
// using Unity.Entities;
using UnityEngine;
// using Unity.Transforms;
using Unity.Mathematics;

namespace Javatale.Prototype
{
	public class BeeAnimatorComponent : MonoBehaviour {
		public Animator animator;
		// public AnimationEvent animationEvent;

		[HeaderAttribute("Current")]
		public int currentDirIndex;
		public BeeAnimationState currentState;
		public float3 currentFaceDirValue;
	}
}
