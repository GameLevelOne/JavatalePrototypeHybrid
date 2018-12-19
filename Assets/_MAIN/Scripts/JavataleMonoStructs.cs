// using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

namespace Javatale.Prototype
{
#region ========== PLAYER ANIMATION ==========

	public class PlayerAnimationIdleStandComponent : MonoBehaviour {}
	public class PlayerAnimationMoveRunComponent : MonoBehaviour {}
	public class PlayerAnimationHitHurtComponent : MonoBehaviour {}
	
	public class PlayerAnimationAttack1Component : MonoBehaviour {}
	public class PlayerAnimationAttack2Component : MonoBehaviour {}
	public class PlayerAnimationAttack3Component : MonoBehaviour {}
	
#endregion
	
#region ========== BEE ANIMATION ==========

	public class BeeAnimationIdleFlyComponent : MonoBehaviour {}
	public class BeeAnimationMovePatrolComponent : MonoBehaviour {}
	public class BeeAnimationMoveAttackComponent : MonoBehaviour {}
	public class BeeAnimationHitHurtComponent : MonoBehaviour {}
	public class BeeAnimationMoveChaseComponent : MonoBehaviour {}

#endregion
	
#region ========== ANIMATION EVENT ==========
	
	public class StartAnimationEventComponent : MonoBehaviour
	{
		public int Value;
	}
	
	public class SpawnSomethingOnAnimationEventComponent : MonoBehaviour
	{
		public int Value;
	}
	
	public class SpawnAttackAnimationEventComponent : MonoBehaviour
	{
		public int Value;
	}

	public class EndAttackAnimationEventComponent : MonoBehaviour 
	{
		public int Value;
	}

	public class EndAllAnimationEventComponent : MonoBehaviour
	{
		public int Value;
	}

	public class EndHurtAnimationEventComponent : MonoBehaviour
	{
		public int Value;
	}

#endregion
	
#region ========== OTHER EVENT ==========
	
	public class DamagedEventComponent : MonoBehaviour 
	{
		public EntryDamage entryDamage;
	}

	public class DestroyedEventComponent : MonoBehaviour
	{
		public int Value;
	}
	
	public class NavMeshEventComponent : MonoBehaviour
	{
		public float3 Destination;
	}

#endregion
}
