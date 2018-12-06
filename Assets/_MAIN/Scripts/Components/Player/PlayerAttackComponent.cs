using UnityEngine;
using Unity.Entities;

namespace Javatale.Prototype
{
	public class PlayerAttackComponent : MonoBehaviour 
	{
		public AnimationEvent animationEvent;
		public GameObjectEntity entityGO;
		
		bool isCheckOnEndAnimation = false;

		void OnEnable ()
		{
			animationEvent.OnEndAnimation += OnEndAnimation;
		}

		void OnDisable ()
		{
			animationEvent.OnEndAnimation -= OnEndAnimation;
		}

		void OnEndAnimation ()
		{
			if (!isCheckOnEndAnimation)
			{	
				isCheckOnEndAnimation = true;

				gameObject.AddComponent<DestroyedEventComponent>();
				entityGO.enabled = false;
				entityGO.enabled = true;
			}
		}
	}
}