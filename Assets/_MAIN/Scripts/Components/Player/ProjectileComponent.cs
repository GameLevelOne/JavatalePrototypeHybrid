using UnityEngine;
using Unity.Entities;

namespace Javatale.Prototype
{
	public struct Projectile : IComponentData
	{
		//
	}
	public class ProjectileComponent : ComponentDataWrapper<Projectile> {}
}