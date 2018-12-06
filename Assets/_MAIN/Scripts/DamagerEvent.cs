using UnityEngine;

namespace Javatale.Prototype
{
	public class DamagerEvent : MonoBehaviour 
	{
		public delegate void DamagerControl(float damageValue, int damageType);
		public event DamagerControl OnDamageEvent;

		void OnTriggerEnter (Collider other) {
			if (other.GetComponent<DamageComponent>() != null) 
			{
				DamageComponent damageComponent = other.GetComponent<DamageComponent>();
				float damageValue = damageComponent.Value;
				int damageType = (int) damageComponent.Type;
				
				if (OnDamageEvent != null) 
				{
					OnDamageEvent(damageValue, damageType);
				} 
			}
		}
	}	
}
