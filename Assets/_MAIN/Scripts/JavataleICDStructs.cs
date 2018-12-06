using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using Unity.Mathematics;

namespace Javatale.Prototype 
{
#region ========== TAG ==========

	// BEE
	public struct Bee : IComponentData
	{
		public int AnimStateIndex;

		[HeaderAttribute("Current")]
		public BeeAnimationState State;
		public int StartAnimationToggle;
		public int EndAnimationToggle;
		public int AnimationToggleValue;
		
		public float IdleTimer;
		public float PatrolTimer;
	}
	public class BeeComponent : ComponentDataWrapper<Bee> {}

#endregion

#region ========== PLAYER ANIMATION DATA ==========

	public struct AnimationPlayerIdleStand : IComponentData {}
	// public class PlayerAnimationIdleStandComponent : ComponentDataWrapper<AnimationPlayerIdleStand> {}

	public struct AnimationPlayerMoveRun : IComponentData {}
	// public class PlayerAnimationMoveRunComponent : ComponentDataWrapper<AnimationPlayerMoveRun> {}

	public struct AnimationPlayerAttack1 : IComponentData {}
	// public class PlayerAnimationAttack1Component : ComponentDataWrapper<AnimationPlayerAttack1> {}

	public struct AnimationPlayerAttack2 : IComponentData {}
	// public class PlayerAnimationAttack2Component : ComponentDataWrapper<AnimationPlayerAttack2> {}

	public struct AnimationPlayerAttack3 : IComponentData {}
	// public class PlayerAnimationAttack3Component : ComponentDataWrapper<AnimationPlayerAttack3> {}

	public struct AnimationPlayerHitHurt : IComponentData {}
	// public class PlayerAnimationNormalHitHurtComponent : ComponentDataWrapper<AnimationPlayerHitHurt> {}
	
#endregion
	
#region ========== BEE ANIMATION DATA ==========
	public struct AnimationBeeIdleFly : IComponentData {}
	// public class BeeAnimationIdleComponent : ComponentDataWrapper<AnimationBeeIdleFly> {}

	public struct AnimationBeeMoveFly : IComponentData {}
	// public class BeeAnimationMoveFlyComponent : ComponentDataWrapper<AnimationBeeMoveFly> {}

#endregion

#region ========== ANIMATION EVENT DATA ==========

	public struct StartAnimationData : IComponentData
	{
		public int Value;
	}

	public struct SpawnSomethingOnAnimationData : IComponentData
	{
		public int Value;
	}

	public struct SpawnAttackAnimationData : IComponentData
	{
		public int Value;
	}

	public struct EndAttackAnimationData : IComponentData
	{
		public int Value;
	}

	public struct EndAllAnimationData : IComponentData
	{
		public int Value;
	}

#endregion

#region ========== LIST ==========

	public struct EntryDamage : IComponentData 
	{
		public float Value;
		public int Type;

        public EntryDamage (float value, int type)
        {
            Value = value;
            Type = type;
        }
	}

#endregion
	
#region ========== DATA ==========

	public struct DamagedData : IComponentData 
	{
		public float Value;
		public int Type;
	}

	public struct DestroyedData : IComponentData
	{
		//
	}

#endregion

#region ========== ANIMATOR DATA ==========

	public struct AnimatorPlayerMove : IComponentData 
	{
		public int dirIndex;
		public float3 dirValue;
		//
	}

	public struct AnimatorPlayerIdle : IComponentData 
	{
		//
	}

	public struct AnimatorPlayerSlashAttack : IComponentData 
	{
		//
	}
	
#endregion

#region OLD
	// public struct EntryAnimation 
    // {
    //     public int DirIndex; //USELESS
    //     public float3 FaceDirValue;
    //     /// <summary>
	// 	/// <para>Values: <br /></para>
	// 	/// <para>0 OFF<br /></para>
	// 	/// <para>1 Idle Stand<br /></para>
	// 	/// <para>2 Idle Run<br /></para>
	// 	/// <para>21 Attack 1<br /></para>
	// 	/// <para>22 Attack 2<br /></para>
	// 	/// <para>41 Normal Hit<br /></para>
	// 	/// </summary>
	// 	public int StartAnimationToggle;

    //     public EntryAnimation (int dirIndex, float3 faceDirValue, int startAnimToggle)
    //     {
    //         DirIndex = dirIndex;
    //         FaceDirValue = faceDirValue;
    //         StartAnimationToggle = startAnimToggle;
    //     }
    // }
#endregion
}
