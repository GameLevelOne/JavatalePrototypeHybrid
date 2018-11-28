using UnityEngine;
using Unity.Entities;

namespace Javatale.Prototype
{
	public class PlayerColliderComponent : MonoBehaviour 
	{
		public ColliderEvent colliderEvent;
		public GameObjectEntity entityGO;

		[HeaderAttribute("Current")]
		public bool isCheckOnDamaged = false;

		void OnEnable ()
		{
			colliderEvent.OnDamageEvent += OnDamageEvent;
		}

		void OnDisable ()
		{
			colliderEvent.OnDamageEvent -= OnDamageEvent;
		}

		void OnDamageEvent (float damageValue, int damageType)
		{
			if (!isCheckOnDamaged) 
			{
				isCheckOnDamaged = true;
				
				gameObject.AddComponent<DamagedEventComponent>().entryDamage = new EntryDamage {Value = damageValue, Type = damageType};
				entityGO.enabled = false;
				entityGO.enabled = true;
			}
		}
	}
}
