using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
// using Unity.Rendering;
using UnityEngine.SceneManagement;

namespace Javatale.Prototype 
{
	public sealed class GameManager 
	{
		public static EntityArchetype playerArchetype;
		public static EntityArchetype beeEnemyArchetype;
		public static EntityArchetype playerAttackArchetype;

		public static JavataleSettings settings;

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
			);

			beeEnemyArchetype = manager.CreateArchetype(
				typeof(Bee),
				typeof(Position),
				typeof(Rotation),
				typeof(MoveDirection),
				typeof(FaceDirection),
				typeof(MoveSpeed),
				typeof(Parent)
			);

			playerAttackArchetype = manager.CreateArchetype(
				typeof(PlayerAttackSpawnData)
			);
		}

		public static void NewGame () 
		{
			GameDebug.Log("NEW GAME");
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
	}
}
