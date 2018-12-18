// using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

namespace Javatale.Prototype 
{
	public class EnemyAIComponent : MonoBehaviour
    {
        [HeaderAttribute("Refferences")]
		public GameObjectEntity entityGO;
        public NavMeshAgent navMeshAgent;

        [HeaderAttribute("Current")]
        public float3 destination;
    }
}
