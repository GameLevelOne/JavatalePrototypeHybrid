using UnityEngine;

namespace Javatale.Prototype
{
	public class AnimationEvent : MonoBehaviour 
	{
		public delegate void AnimationControl();
		public event AnimationControl OnStartAnimation;
		public event AnimationControl OnSpawnSomethingOnAnimation;
		public event AnimationControl OnEndAttackAnimation;
		public event AnimationControl OnEndAnimation;

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

		void EndAttackAnimation ()
		{
			if (OnEndAttackAnimation != null)
			{
				OnEndAttackAnimation();
			}
		}

		void EndAnimation ()
		{
			if (OnEndAnimation != null)
			{
				OnEndAnimation();
			}
		}
	}
}
