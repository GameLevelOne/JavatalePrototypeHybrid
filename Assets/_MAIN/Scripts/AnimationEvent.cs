using UnityEngine;

namespace Javatale.Prototype
{
	public class AnimationEvent : MonoBehaviour 
	{
		public delegate void AnimationControl();
		public event AnimationControl OnStartAnimation;
		public event AnimationControl OnSpawnAttackAnimation;
		public event AnimationControl OnSpawnSomethingOnAnimation;
		public event AnimationControl OnEndAttackAnimation;
		public event AnimationControl OnEndAllAnimation;
		public event AnimationControl OnEndHurtAnimation;

		// public Animator animator;

		// bool isAnimating = false;

		void StartAnimation () 
		{
			if (OnStartAnimation != null) 
			{
				OnStartAnimation();
			}
		} 

		void SpawnSomethingOnAnimation ()
		{
			if (OnSpawnSomethingOnAnimation != null)
			{
				OnSpawnSomethingOnAnimation();
			}
		}

		void SpawnAttackAnimation ()
		{
			if (OnSpawnAttackAnimation != null)
			{
				OnSpawnAttackAnimation();
			}
		}

		void EndAttackAnimation ()
		{
			if (OnEndAttackAnimation != null)
			{
				OnEndAttackAnimation();
			}
		}

		void EndAllAnimation ()
		{
			if (OnEndAllAnimation != null)
			{
				OnEndAllAnimation();
			}
		}

		void EndHurtAnimation ()
		{
			if (OnEndHurtAnimation != null)
			{
				OnEndHurtAnimation();
			}
		}
	}
}
