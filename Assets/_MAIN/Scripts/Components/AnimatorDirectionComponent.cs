using UnityEngine;
using Unity.Mathematics;

namespace Javatale.Prototype 
{
	public class AnimatorDirectionComponent : MonoBehaviour 
	{
		public int dirIndex;
		public float3 dirValue;

		public void SetValue (int index, float3 value)
		{
			dirIndex = index;
			dirValue = value;
		}
	}
}
