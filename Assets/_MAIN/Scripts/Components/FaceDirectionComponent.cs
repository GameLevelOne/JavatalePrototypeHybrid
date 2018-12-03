using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Javatale.Prototype 
{
	[SerializableAttribute]
	public struct FaceDirection : IComponentData 
	{
		public float3 Value;
		public int DirIndex;
	}

	public class FaceDirectionComponent : ComponentDataWrapper<FaceDirection> {}
}
