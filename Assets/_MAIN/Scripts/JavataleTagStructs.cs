using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using Unity.Mathematics;

namespace Javatale.Prototype 
{
#region ==========TAG==========

	public struct Player : IComponentData
	{
		public int AnimStateIndex;

		[HeaderAttribute("Current")]
		public PlayerAnimationState State;

		/// <summary>
		/// <para>Values: <br /></para>
		/// <para>0 OFF<br /></para>
		/// <para>1 Idle Stand<br /></para>
		/// <para>2 Idle Run<br /></para>
		/// <para>21 Attack 1<br /></para>
		/// <para>22 Attack 2<br /></para>
		/// <para>41 Normal Hit<br /></para>
		/// </summary>
		public int StartAnimationToggle;
		// public int EndAnimationToggle;
		public int AnimationToggleValue;
		
		/// <summary>
		/// <para>Values: <br /></para>
		/// <para>0 Attack 1<br /></para>
		/// <para>1 Attack 2<br /></para>
		/// <para>2 Attack 3<br /></para>
		/// </summary>
		public int AttackIndex;
	}
	public class PlayerComponent : ComponentDataWrapper<Player> {}

	public struct Projectile : IComponentData
	{
		//
	}
	public class ProjectileComponent : ComponentDataWrapper<Projectile> {}

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

#region ==========LIST==========

	public struct EntryAnimation 
    {
        public int DirIndex; //USELESS
        public float3 FaceDirValue;
        /// <summary>
		/// <para>Values: <br /></para>
		/// <para>0 OFF<br /></para>
		/// <para>1 Idle Stand<br /></para>
		/// <para>2 Idle Run<br /></para>
		/// <para>21 Attack 1<br /></para>
		/// <para>22 Attack 2<br /></para>
		/// <para>41 Normal Hit<br /></para>
		/// </summary>
		public int StartAnimationToggle;

        public EntryAnimation (int dirIndex, float3 faceDirValue, int startAnimToggle)
        {
            DirIndex = dirIndex;
            FaceDirValue = faceDirValue;
            StartAnimationToggle = startAnimToggle;
        }
    }

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

#region ==========ANIMATION EVENT DATA==========

	public struct StartAnimationData : IComponentData
	{
		public int Value;
	}

	public struct SpawnSomethingOnAnimationData : IComponentData
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
	
#region ==========DATA==========

	public struct PlayerAttackSpawnData : IComponentData
	{
		public Position pos;
		public Rotation rot;
		public MoveDirection moveDir;
		public FaceDirection faceDir;
        public Parent parent;
        public Projectile projectile;
        public int attackIndex;
	}

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

}
