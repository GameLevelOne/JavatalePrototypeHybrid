using System;
using Unity.Entities;

namespace Javatale.Prototype
{
	[SerializableAttribute]
	public struct Health : IComponentData
	{
		public float Value;
	}
	public class HealthComponent : ComponentDataWrapper<Health> {}
}
