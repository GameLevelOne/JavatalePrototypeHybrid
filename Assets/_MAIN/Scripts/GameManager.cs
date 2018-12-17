// using Unity.Collections;
using Unity.Entities;
// using Unity.Mathematics;
// using Unity.Transforms;
using UnityEngine;
// using Unity.Rendering;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public sealed class GameManager 
	{
		// public static EntityArchetype playerArchetype;
		// public static EntityArchetype beeEnemyArchetype;
		// public static EntityArchetype playerAttackArchetype;
		public static EntityManager entityManager;
		public static JavataleSettings settings;

		#region Universal Lists
		
		public static List<Entity> parentEntitiesInGame;
		public static List<GameObjectEntity> childEntitiesInGame;
		// public static List<bool> addedStateComponentsInGame;
		public static List<int> entitiesAnimationToggle;
		// public static List<int> entitiesActionAnimation;

		/// <summary>
		/// <para>Values : <br /></para>
		/// <para>0 OFF / Idle<br /></para>
		/// <para>1 Move<br /></para>
		/// </summary>
		public static List<int> entitiesIdleLoopAnimationChecker;
		
		#endregion

		[RuntimeInitializeOnLoadMethodAttribute(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void Initialize () 
		{
			entityManager = World.Active.GetOrCreateManager<EntityManager>();

			// playerArchetype = manager.CreateArchetype(
			// 	typeof(Player),
			// 	typeof(Position),
			// 	typeof(Rotation),
			// 	typeof(MoveDirection),
			// 	typeof(FaceDirection),
			// 	typeof(MoveSpeed),
			// 	typeof(PlayerInputDirection),
			// 	typeof(PlayerInputAttack)
			// );

			// beeEnemyArchetype = manager.CreateArchetype(
			// 	typeof(Bee),
			// 	typeof(Position),
			// 	typeof(Rotation),
			// 	typeof(MoveDirection),
			// 	typeof(FaceDirection),
			// 	typeof(MoveSpeed)
			// );

			// playerAttackArchetype = manager.CreateArchetype(
			// 	typeof(PlayerAttackSpawnData)
			// );

			
			parentEntitiesInGame = new List<Entity>();
			childEntitiesInGame = new List<GameObjectEntity>();
			// addedStateComponentsInGame = new List<bool>();
			entitiesAnimationToggle = new List<int>();
			entitiesIdleLoopAnimationChecker = new List<int>();
		}

		public static void NewGame () 
		{
			GameDebug.Log("NEW GAME");
			AddPlayer();
			AddBee(settings.maxEnemy);
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

			GameObject playerPrefab = settings.playerPrefab;
			// float3 float3Zero = float3.zero;

			// PARENT
			GameObject playerGO = GameObjectEntity.Instantiate(playerPrefab);
			Entity playerEntity = playerGO.GetComponent<GameObjectEntity>().Entity;

			parentEntitiesInGame.Add(playerEntity);
			int currentParentEntityIndex = parentEntitiesInGame.Count-1;

			manager.SetComponentData(playerEntity, new Parent { EntityIndex = currentParentEntityIndex });

			// CHILD
			ChildComponent childComponent = playerGO.GetComponentInChildren<ChildComponent>();
			GameObjectEntity playerChildGOEntity = childComponent.GetComponent<GameObjectEntity>();

			childEntitiesInGame.Add(playerChildGOEntity);
			int currentChildEntityIndex = childEntitiesInGame.Count-1;

			childComponent.EntityIndex = currentChildEntityIndex;

			// addedStateComponentsInGame.Add(false); // State Component
			entitiesAnimationToggle.Add(0);
			entitiesIdleLoopAnimationChecker.Add(0);
		}

		static void AddBee (int maxEnemy)
		{
			EntityManager manager = World.Active.GetOrCreateManager<EntityManager>();

			GameObject beePrefab = settings.beeEnemyPrefab;
			// float3 float3Zero = float3.zero;
			float horBound = settings.horizontalBound;
			float verBound = settings.verticalBound;

			for (int i=0; i<maxEnemy; i++)
			{
				float xVal = Random.Range(-horBound, horBound);
				float zVal = Random.Range(-verBound, verBound);

				GameObject beeGO = GameObjectEntity.Instantiate(beePrefab, new Vector3(xVal, 0f, zVal), Quaternion.identity);
				ChildComponent childComponent = beeGO.GetComponentInChildren<ChildComponent>();
				GameObjectEntity beeChildGOEntity = childComponent.GetComponent<GameObjectEntity>();
				Entity beeEntity = beeChildGOEntity.Entity;

				// PARENT
				parentEntitiesInGame.Add(beeEntity);
				int currentParentEntityIndex = parentEntitiesInGame.Count-1;

				manager.SetComponentData(beeEntity, new Parent { EntityIndex = currentParentEntityIndex });

				// CHILD
				childEntitiesInGame.Add(beeChildGOEntity);
				int currentChildEntityIndex = childEntitiesInGame.Count-1;

				childComponent.EntityIndex = currentChildEntityIndex;

				// addedStateComponentsInGame.Add(false); // State Component
				entitiesAnimationToggle.Add(0);
				entitiesIdleLoopAnimationChecker.Add(0);
			}
		}
	}
}
