using UnityEngine;
using Unity.Entities;

namespace Javatale.Prototype
{
	public class PlayerAttackComponent : MonoBehaviour 
	{
		public AnimationEvent animationEvent;
		public GameObjectEntity entityGO;
		
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
			gameObject.AddComponent<DestroyComponent>();
			entityGO.enabled = false;
			entityGO.enabled = true;
		}
	}
}