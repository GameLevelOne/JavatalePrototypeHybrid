using UnityEngine;
using Unity.Entities;

namespace Javatale.Prototype
{
	public class BeeColliderComponent : MonoBehaviour 
	{
		[HeaderAttribute("References")]
		public GameObjectEntity entityGO;
		// public ColliderEvent colliderEvent;
		public DamagerEvent damagerEvent;

		[HeaderAttribute("Current")]
		public bool isCheckOnDamaged = false;

		void OnEnable ()
		{
			damagerEvent.OnDamageEvent += OnDamageEvent;
		}

		void OnDisable ()
		{
			damagerEvent.OnDamageEvent -= OnDamageEvent;
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
