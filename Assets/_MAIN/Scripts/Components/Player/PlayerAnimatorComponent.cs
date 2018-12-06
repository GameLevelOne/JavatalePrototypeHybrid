using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

namespace Javatale.Prototype
{
	public class PlayerAnimatorComponent : MonoBehaviour 
	{
		[HeaderAttribute("References")]
		public GameObjectEntity entityGO;
		public Animator animator;
		public AnimationEvent animationEvent;

		[HeaderAttribute("Current")]
		public int currentDirIndex;
		public PlayerAnimationState currentState;
		public float3 currentFaceDirValue;
		
		[SpaceAttribute(10f)]
		// public bool isFinishAnyAnimation = true;
		// public bool isFinishAttackAnimation = true;
		public bool isCheckOnStartAnimation = false;
		public bool isCheckOnSpawnSomethingOnAnimation = false;
		public bool isCheckOnSpawnAttackAnimation = false;
		public bool isCheckOnEndAttackAnimation = false;
		public bool isCheckOnEndAllAnimation = false;
		public bool isCheckOnEndHurtAnimation = false;

		void OnEnable ()
		{
			animationEvent.OnStartAnimation += OnStartAnimation;
			animationEvent.OnSpawnSomethingOnAnimation += OnSpawnSomethingOnAnimation;
			animationEvent.OnSpawnAttackAnimation += OnSpawnAttackAnimation;
			animationEvent.OnEndAttackAnimation += OnEndAttackAnimation;
			animationEvent.OnEndAllAnimation += OnEndAllAnimation;
			animationEvent.OnEndHurtAnimation += OnEndHurtAnimation;
		}

		void OnDisable ()
		{
			animationEvent.OnStartAnimation -= OnStartAnimation;
			animationEvent.OnSpawnSomethingOnAnimation -= OnSpawnSomethingOnAnimation;
			animationEvent.OnSpawnAttackAnimation -= OnSpawnAttackAnimation;
			animationEvent.OnEndAttackAnimation -= OnEndAttackAnimation;
			animationEvent.OnEndAllAnimation -= OnEndAllAnimation;
			animationEvent.OnEndHurtAnimation -= OnEndHurtAnimation;
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

		void OnSpawnAttackAnimation ()
		{
			if (!isCheckOnSpawnAttackAnimation)
			{
				isCheckOnSpawnAttackAnimation = true;

				gameObject.AddComponent<SpawnAttackAnimationEventComponent>().Value = 0;
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

		void OnEndAllAnimation ()
		{
			if (!isCheckOnEndAllAnimation) 
			{
				isCheckOnEndAllAnimation = true;

				gameObject.AddComponent<EndAllAnimationEventComponent>().Value = 0;
				entityGO.enabled = false;
				entityGO.enabled = true;
			}
		}

		void OnEndHurtAnimation ()
		{
			if (!isCheckOnEndHurtAnimation)
			{
				isCheckOnEndHurtAnimation = true;

				gameObject.AddComponent<EndHurtAnimationEventComponent>().Value = 0;
				entityGO.enabled = false;
				entityGO.enabled = true;
			}
		}
	}
}
