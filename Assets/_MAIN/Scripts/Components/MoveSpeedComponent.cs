using System;
using Unity.Entities;

namespace Javatale.Prototype 
{
	[SerializableAttribute]
	public struct MoveSpeed : IComponentData 
	{
		public float Value;
	}

	public class MoveSpeedComponent : ComponentDataWrapper<MoveSpeed> {}
}
