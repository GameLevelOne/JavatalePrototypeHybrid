﻿using UnityEngine;

namespace Javatale.Prototype
{
	public class ColliderEvent : MonoBehaviour 
	{
		public delegate void ColliderControl(GameObject GO);
		public event ColliderControl OnCollisionEnterEvent;
		public event ColliderControl OnCollisionStayEvent;
		public event ColliderControl OnCollisionExitEvent;
		public event ColliderControl OnTriggerEnterEvent;
		public event ColliderControl OnTriggerStayEvent;
		public event ColliderControl OnTriggerExitEvent;

		void OnCollisionEnter (Collision other) {
			if (OnCollisionEnterEvent != null) OnCollisionEnterEvent(other.gameObject);
		}

		void OnCollisionStay (Collision other) {
			if (OnCollisionStayEvent != null) OnCollisionStayEvent(other.gameObject);
		}

		void OnCollisionExit (Collision other) {
			if (OnCollisionExitEvent != null) OnCollisionExitEvent(other.gameObject);
		}

		void OnTriggerEnter (Collider other) {
			if (OnTriggerEnterEvent != null) OnTriggerEnterEvent(other.gameObject);
		}

		void OnTriggerStay (Collider other) {
			if (OnTriggerStayEvent != null) OnTriggerStayEvent(other.gameObject);
		}

		void OnTriggerExit (Collider other) {
			if (OnTriggerExitEvent != null) OnTriggerExitEvent(other.gameObject);
		}
	}	
}
