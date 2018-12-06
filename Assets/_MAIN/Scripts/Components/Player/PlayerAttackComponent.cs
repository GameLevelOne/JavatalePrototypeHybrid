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
			animationEvent.OnEndAllAnimation += OnEndAllAnimation;
		}

		void OnDisable ()
		{
			animationEvent.OnEndAllAnimation -= OnEndAllAnimation;
		}

		void OnEndAllAnimation ()
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