using UnityEngine;
// using Unity.Rendering;
using Unity.Entities;
using Unity.Mathematics;
using System.Collections.Generic;

namespace Javatale.Prototype
{
    public class PlayerAttackSlashBarrier : BarrierSystem {}

    public class PlayerAnimationSetBarrier : BarrierSystem {}

    public class JavataleSettings : MonoBehaviour
    {
        [HeaderAttribute("Reference Components")]
        // public PositionComponent positionComponent;
        // public MoveDirectionComponent moveDirectionComponent;

        [HeaderAttribute("Attributes")]
        public float verticalBound = 10.0f;
        public float horizontalBound = 10.0f;
        public Vector3 worldToCameraRotation = Vector3.zero;

        [HeaderAttribute("Player Attributes")]
		public int maxPlayerAttackIndex = 2;
        public float playerMoveSpeed = 300f;

        [HeaderAttribute("Enemy Attributes")]
		public int maxEnemy = 3;
        public float enemyMoveSpeed = 3.0f;
        public float enemyMinPatrolCooldown = 5.0f;
        public float enemyMaxPatrolCooldown = 10.0f;
        public float enemyMinIdleCooldown = 3.0f;
        public float enemyMaxIdleCooldown = 8.0f;

        [SpaceAttribute(10f)]
        public GameObject playerPrefab;
        public GameObject beeEnemyPrefab;

        [HeaderAttribute("Index = 0 : Down, 1 : Left, 2 : Up, 3 : Right")]
        public float3[] playerAttackRanges;
        public GameObject[] playerAttack1Childs;
        public GameObject[] playerAttack2Childs;
        public GameObject[] playerAttack3Childs;

        public List<GameObject[]> playerSlashAttackChilds = new List<GameObject[]>();

        void Start ()
        {
            playerSlashAttackChilds.Add(playerAttack1Childs);
            playerSlashAttackChilds.Add(playerAttack2Childs);
            playerSlashAttackChilds.Add(playerAttack3Childs);
        }
    }

	public enum PlayerAnimationState
    {
        IDLE_STAND,
        MOVE_RUN,
        ATTACK_1,
        ATTACK_2,
        ATTACK_3,
        HIT_HURT
    }

	public enum BeeAnimationState
    {
        IDLE_FLY,
        MOVE_PATROL,
        ATTACK,
        HIT_HURT,
        MOVE_CHASE
    }

	public enum DamageType
    {
        NORMAL,
        STUN,
        KNOCKDOWN
    }

	public enum SpawnAttackType
    {
        SLASH,
        BOW
    }
}