using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
// using Unity.Rendering;
using UnityEngine.SceneManagement;

using UnityRandom = UnityEngine.Random; //AMBIGUOUS ISSUE

namespace Javatale.Prototype 
{
	public sealed class GameManager 
	{
		public static EntityArchetype playerArchetype;
		public static EntityArchetype beeEnemyArchetype;
		public static EntityArchetype playerAttackArchetype;

		public static JavataleSettings settings;

		#region Universal Lists
		public static List<Entity> entitiesInGame;
		public static List<float3> entitiesPos;
		public static List<EntryAnimation> entitiesAnimation;
		#endregion

		#region Specific Entity Lists
		public static List<PlayerAnimationState> entitiesPlayerAnimState;
		public static List<BeeAnimationState> entitiesBeeAnimState;
		#endregion

		// public static List<EntryPlayerAnim> entitiesPlayerAnim;
		// public static List<EntryProjectileAnim> entitiesProjectileAnim;
		// public static List<EntryBeeAnim> entitiesBeeAnim;

		#region Empty List
		public static List<int> emptyEntitiesIndexes;
		public static List<int> emptyPosIndexes;
		public static List<int> emptyAnimIndexes;
		#endregion

		[RuntimeInitializeOnLoadMethodAttribute(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void Initialize () 
		{
			EntityManager manager = World.Active.GetOrCreateManager<EntityManager>();

			playerArchetype = manager.CreateArchetype(
				typeof(Player),
				typeof(Position),
				typeof(Rotation),
				typeof(MoveDirection),
				typeof(FaceDirection),
				typeof(MoveSpeed),
				typeof(Parent),
				typeof(PlayerInputDirection),
				typeof(PlayerInputAttack)

				// typeof(MeshInstanceRenderer)
			);

			beeEnemyArchetype = manager.CreateArchetype(
				typeof(Bee),
				typeof(Position),
				typeof(Rotation),
				typeof(MoveDirection),
				typeof(FaceDirection),
				typeof(MoveSpeed),
				typeof(Parent)

				// typeof(MeshInstanceRenderer)
			);

			playerAttackArchetype = manager.CreateArchetype(
				typeof(PlayerAttackSpawnData)
			);

			// UNIVERSAL
			entitiesInGame = new List<Entity>();
			entitiesPos = new List<float3>();
			entitiesAnimation = new List<EntryAnimation>();

			// SPECIFIC
			entitiesPlayerAnimState = new List<PlayerAnimationState>();
			entitiesBeeAnimState = new List<BeeAnimationState>();

			// EMPTY
			emptyEntitiesIndexes = new List<int>();
			emptyPosIndexes = new List<int>();
			emptyAnimIndexes = new List<int>();
		}

		public static void NewGame () 
		{
			AddPlayer();
			AddEnemy(settings.maxEnemy);
		}

		[RuntimeInitializeOnLoadMethodAttribute(RuntimeInitializeLoadType.AfterSceneLoad)]
		public static void InitializeAfterSceneLoad () 
		{
			GameObject settingGO = GameObject.FindGameObjectWithTag("Settings");
			if (settingGO != null) 
			{
				InitializeWithScene();
			}
			else
			{
				SceneManager.sceneLoaded += OnSceneLoaded;
			}
		}

		public static void InitializeWithScene ()
		{
			GameObject settingGO = GameObject.FindGameObjectWithTag("Settings");
			
			if (settingGO != null) 
			{
				settings = settingGO?.GetComponent<JavataleSettings>();

				if (settings != null)
				{
					NewGame();
				}
			}
			else
			{
				SceneManager.sceneLoaded += OnSceneLoaded;
			}
		}

		static void OnSceneLoaded (Scene scene, LoadSceneMode loadSceneMode)
		{
			InitializeWithScene();
		}

		static void AddPlayer ()
		{
			EntityManager manager = World.Active.GetOrCreateManager<EntityManager>();

			Entity playerEntity = manager.CreateEntity(playerArchetype);

			float3 float3Zero = float3.zero;

			manager.SetComponentData(playerEntity, new Player { State = PlayerAnimationState.IDLE_STAND, StartAnimationToggle = 1 });
			manager.SetComponentData(playerEntity, new PlayerInputDirection { Value = float3Zero });
			manager.SetComponentData(playerEntity, new Position { Value = float3Zero });
			manager.SetComponentData(playerEntity, new Rotation { Value = Quaternion.Euler(settings.worldToCameraRotation) });
			manager.SetComponentData(playerEntity, new MoveDirection { Value = float3Zero });
			manager.SetComponentData(playerEntity, new FaceDirection { Value = float3Zero, dirIndex = 0 });
			manager.SetComponentData(playerEntity, new MoveSpeed { Value = settings.playerMoveSpeed });

			#region ENTITY LIST
			entitiesInGame.Add(playerEntity);
			int currentEntitiesInGameListIndex = entitiesInGame.Count-1;
			#endregion

			#region POS LIST
			Position playerInitPos = manager.GetComponentData<Position>(playerEntity);
			//Add new Position into Pos List
			entitiesPos.Add(playerInitPos.Value); 
			int currentPosListIndex = entitiesPos.Count-1; //Get last List Index
			#endregion

			#region ANIMATION LIST
			FaceDirection playerInitFaceDir = manager.GetComponentData<FaceDirection>(playerEntity);
			//Add new EntryAnimation into Animation List (StartAnimationToggle = 1)
			entitiesAnimation.Add(new EntryAnimation(playerInitFaceDir.dirIndex, playerInitFaceDir.Value, 1));
			int currentAnimListIndex = entitiesAnimation.Count-1; //Get the last List Index in Animation List
			#endregion

			#region PLAYER ANIMATION STATE LIST
			//Add new EntryPlayerAnimState into Player Animation State List (IDLE_STAND)
			entitiesPlayerAnimState.Add(PlayerAnimationState.IDLE_STAND);
			int currentPlayerAnimStateListIndex = entitiesPlayerAnimState.Count-1;//Get the last List Index in Player Animation State List
			#endregion

			//Set All List Index to parent IComponentData and its child Component
			manager.SetComponentData(playerEntity, new Parent { EntityIndex = currentEntitiesInGameListIndex, PosIndex = currentPosListIndex, AnimIndex = currentAnimListIndex });
			manager.SetComponentData(playerEntity, new Player { AnimStateIndex = currentPlayerAnimStateListIndex });

			GameObject playerGO = GameObject.Instantiate(settings.playerChild, playerInitPos.Value, quaternion.identity);
			playerGO.GetComponent<ChildComponent>().EntityIndex = currentEntitiesInGameListIndex;
			playerGO.GetComponent<ChildComponent>().PosIndex = currentPosListIndex;
			playerGO.GetComponent<ChildComponent>().AnimIndex = currentAnimListIndex;
		}

		static void AddEnemy (int enemyCount) {
			EntityManager manager = World.Active.GetOrCreateManager<EntityManager>();
			
			NativeArray<Entity> entities = new NativeArray<Entity>(enemyCount, Allocator.Temp);
			manager.CreateEntity(beeEnemyArchetype, entities);

			float3 float3Zero = float3.zero;
			float horBound = settings.horizontalBound;
			float verBound = settings.verticalBound;
			float enemyMoveSpeed = settings.enemyMoveSpeed;
			Vector3 worldToCameraRotation = settings.worldToCameraRotation;

			for (int i=0; i<enemyCount; i++) {
				float xVal = UnityRandom.Range(-horBound, horBound);
				float zVal = UnityRandom.Range(-verBound, verBound);
				float randomIdleTimer = UnityRandom.Range(settings.enemyMinIdleCooldown, settings.enemyMaxIdleCooldown);
				float3 randomMoveDir = new float3 (UnityRandom.Range(-1,2) == 0 ? 1f : -1f, 0f, UnityRandom.Range(-1,2) == 0 ? 1f : -1f);

				manager.SetComponentData(entities[i], new Bee { State = BeeAnimationState.IDLE_FLY, StartAnimationToggle = 1, EndAnimationToggle = 0, IdleTimer = randomIdleTimer, PatrolTimer = 0f });
				manager.SetComponentData(entities[i], new Position { Value = new float3(xVal, 0f, zVal) });
				manager.SetComponentData(entities[i], new Rotation { Value = Quaternion.Euler(worldToCameraRotation) });
				manager.SetComponentData(entities[i], new MoveDirection { Value = float3Zero });
				manager.SetComponentData(entities[i], new FaceDirection { Value = randomMoveDir, dirIndex = 1 });
				manager.SetComponentData(entities[i], new MoveSpeed { Value = enemyMoveSpeed });

				// manager.SetSharedComponentData(entities[i], 
				// new MeshInstanceRenderer 
				// {
				// 	mesh = settings.enemyMesh,
				// 	material = settings.enemyMaterial
				// });

				#region ENTITY LIST
				entitiesInGame.Add(entities[i]);
				int currentEntitiesInGameListIndex = entitiesInGame.Count-1;
				#endregion

				#region POSITION LIST
				Position enemyInitPos = manager.GetComponentData<Position>(entities[i]);
				entitiesPos.Add(enemyInitPos.Value); 
				int currentPosListIndex = entitiesPos.Count-1; 
				#endregion

				#region ANIMATION LIST
				FaceDirection enemyInitDir = manager.GetComponentData<FaceDirection>(entities[i]);
				entitiesAnimation.Add(new EntryAnimation(enemyInitDir.dirIndex, enemyInitDir.Value, 1));
				int currentAnimListIndex = entitiesAnimation.Count-1; 
				#endregion

				#region BEE ANIMATION STATE LIST
				entitiesBeeAnimState.Add(BeeAnimationState.IDLE_FLY);
				int currentBeeAnimStateListIndex = entitiesBeeAnimState.Count-1;
				#endregion

				//OLD
				// entitiesBeeAnim.Add(new EntryBeeAnim(enemyInitDir.dirIndex, BeeAnimationState.IDLE_FLY)); //Add Entity Animation State to List
				// int currentAnimListIndex = entitiesBeeAnim.Count-1; //Get last List Index to Anim List

				manager.SetComponentData(entities[i], new Parent { EntityIndex = currentEntitiesInGameListIndex, PosIndex = currentPosListIndex, AnimIndex = currentAnimListIndex });
				manager.SetComponentData(entities[i], new Bee { AnimStateIndex = currentBeeAnimStateListIndex });

				GameObject enemyGO = GameObject.Instantiate(settings.beeEnemyChild, enemyInitPos.Value, quaternion.identity);
				enemyGO.GetComponent<ChildComponent>().EntityIndex = currentEntitiesInGameListIndex;
				enemyGO.GetComponent<ChildComponent>().PosIndex = currentPosListIndex;
				enemyGO.GetComponent<ChildComponent>().AnimIndex = currentAnimListIndex;
			}

			entities.Dispose();
		}
	}
}
