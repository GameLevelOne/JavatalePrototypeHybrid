using UnityEngine;
// using Unity.Rendering;
using Unity.Entities;
using Unity.Mathematics;

namespace Javatale.Prototype
{
    public class PlayerAttackSlashBarrier : BarrierSystem {}

    public class JavataleSettings : MonoBehaviour
    {
        [HeaderAttribute("Attributes")]
        public float verticalBound = 10.0f;
        public float horizontalBound = 10.0f;
        public Vector3 worldToCameraRotation = Vector3.zero;

        [HeaderAttribute("Player Attributes")]
		public int maxPlayerAttackIndex = 0;
        public float playerMoveSpeed = 5.0f;
        // public float playerInitialHealth = 100.0f;
        // public float playerCollisionRadius = 1.0f;

        [HeaderAttribute("Enemy Attributes")]
		public int maxEnemy = 3;
        public float enemyMoveSpeed = 3.0f;
        public float enemyMinPatrolCooldown = 5.0f;
        public float enemyMaxPatrolCooldown = 10.0f;
        public float enemyMinIdleCooldown = 3.0f;
        public float enemyMaxIdleCooldown = 8.0f;
        // public float enemyInitialHealth = 100.0f;
        // public float enemyCollisionRadius = 1.0f;
        
        // public Vector3 worldToCameraRotation = new Vector3 (40f, 0f, 0f); 

        // [HeaderAttribute("Rendering")]
        // public Mesh playerMesh;
        // public Mesh beeEnemyMesh;
        // public Mesh playerAttackMesh;
        // public Material playerMaterial;
        // public Material enemyMaterial;

        // [HeaderAttribute("Index = 0 : Attack 1, 1 : Attack 2, 2 : Attack 3")]
        // public Material[] playerAttackMaterials;

        [SpaceAttribute(10f)]
        public GameObject playerChild;
        public GameObject beeEnemyChild;

        [HeaderAttribute("Index = 0 : Down, 1 : Left, 2 : Up, 3 : Right")]
        public GameObject[] playerAttack1Childs;
        public GameObject[] playerAttack2Childs;
        public GameObject[] playerAttack3Childs;
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
        MOVE_FLY
    }

	public enum DamageType
    {
        NORMAL,
        STUN,
        KNOCKDOWN
    }
}