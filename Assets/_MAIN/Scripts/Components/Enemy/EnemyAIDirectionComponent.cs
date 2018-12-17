using System;
using Unity.Entities;
using Unity.Mathematics;
// using UnityEngine;
using UnityEngine.AI;

namespace Javatale.Prototype 
{
    [SerializableAttribute]
	public struct EnemyAIDirection : IComponentData
	{
		public float3 Value;
	}
	public class EnemyAIDirectionComponent : ComponentDataWrapper<EnemyAIDirection> {}
}
