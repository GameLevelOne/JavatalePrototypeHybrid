// using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
// using Unity.Transforms;
using Unity.Mathematics;

namespace Javatale.Prototype
{
	public class PlayerAnimatorComponent : MonoBehaviour 
	{
		public GameObjectEntity entityGO;
		public Animator animator;
		public AnimationEvent animationEvent;

		[HeaderAttribute("Current")]
		public int currentDirIndex;
		public PlayerAnimationState currentState;
		public float3 currentFaceDirValue;
		
		[SpaceAttribute(10f)]
		public bool isFinishAnyAnimation = true;
		public bool isFinishAttackAnimation = true;
		public bool isCheckOnStartAnimation = false;
		public bool isCheckOnSpawnSomethingOnAnimation = false;
		public bool isCheckOnEndAttackAnimation = false;
		public bool isCheckOnEndAllAnimation = false;

		void OnEnable ()
		{
			animationEvent.OnStartAnimation += OnStartAnimation;
			animationEvent.OnSpawnSomethingOnAnimation += OnSpawnSomethingOnAnimation;
			animationEvent.OnEndAttackAnimation += OnEndAttackAnimation;
			animationEvent.OnEndAnimation += OnEndAnimation;
		}

		void OnDisable ()
		{
			animationEvent.OnStartAnimation -= OnStartAnimation;
			animationEvent.OnSpawnSomethingOnAnimation -= OnSpawnSomethingOnAnimation;
			animationEvent.OnEndAttackAnimation -= OnEndAttackAnimation;
			animationEvent.OnEndAnimation -= OnEndAnimation;
		}

		void OnStartAnimation ()
		{
			if (!isCheckOnStartAnimation)
			{
				isCheckOnStartAnimation = true;

				gameObject.AddComponent<StartAnimationEventComponent>().Value = 0;
				entityGO.enabled = false;
				entityGO.enabled = true;
			}
		}

		void OnSpawnSomethingOnAnimation ()
		{
			if (!isCheckOnSpawnSomethingOnAnimation)
			{
				isCheckOnSpawnSomethingOnAnimation = true;

				gameObject.AddComponent<SpawnSomethingOnAnimationEventComponent>().Value = 0;
				entityGO.enabled = false;
				entityGO.enabled = true;
			}
		}

		void OnEndAttackAnimation ()
		{
			if (!isCheckOnEndAttackAnimation)
			{
				isCheckOnEndAttackAnimation = true;

				gameObject.AddComponent<EndAttackAnimationEventComponent>().Value = 0;
				entityGO.enabled = false;
				entityGO.enabled = true;
			}
		}

		void OnEndAnimation ()
		{
			if (!isCheckOnEndAllAnimation) 
			{
				isCheckOnEndAllAnimation = true;

				gameObject.AddComponent<EndAllAnimationEventComponent>().Value = 0;
				entityGO.enabled = false;
				entityGO.enabled = true;
			}
		}
	}
}
