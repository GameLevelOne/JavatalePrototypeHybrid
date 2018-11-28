using Unity.Collections;
using Unity.Entities;
// using UnityEngine;
using Unity.Mathematics;
// using Unity.Burst;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	// public class AnimationBarrier : BarrierSystem {}

	public class PlayerAnimationSystem : ComponentSystem 
	{
		// [BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray AnimationEntities;
			public ComponentDataArray<Player> Player;
			public ComponentDataArray<MoveDirection> MoveDirection;
			[ReadOnlyAttribute] public ComponentDataArray<FaceDirection> FaceDirection;
			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
		}
		[InjectAttribute] private Data data;

		// Vector3 vector3Zero = Vector3.zero;
		float3 float3Zero = float3.zero;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;
			List<EntryAnimation> listAnim = GameManager.entitiesAnimation;
            // List<EntryPlayerAnim> listAnim = GameManager.entitiesPlayerAnim;

			for (int i=0; i<data.Length; i++)
			{
				Entity animEntity = data.AnimationEntities[i];
				Player player = data.Player[i];
				MoveDirection moveDir = data.MoveDirection[i];
				FaceDirection faceDir = data.FaceDirection[i];
				Parent parent = data.Parent[i];

				int animIndex = parent.AnimIndex;
				EntryAnimation entryAnim = listAnim[animIndex];
				
				int playerStartAnimToggle = player.StartAnimationToggle;

#region START ANIMATION
				if (playerStartAnimToggle != 0) 
				{
					switch (playerStartAnimToggle) 
					{
						case 1:
							commandBuffer.AddComponent(animEntity, new AnimationPlayerIdleStand{});
							
							break;
						case 2:
							commandBuffer.AddComponent(animEntity, new AnimationPlayerMoveRun{});
						
							break;
						case 21:
							commandBuffer.AddComponent(animEntity, new AnimationPlayerAttack1{});

							moveDir.Value = float3Zero;
							data.MoveDirection[i] = moveDir;

							break;
						case 22:
							commandBuffer.AddComponent(animEntity, new AnimationPlayerAttack2{});

							moveDir.Value = float3Zero;
							data.MoveDirection[i] = moveDir;

							break;
						case 23:
							commandBuffer.AddComponent(animEntity, new AnimationPlayerAttack3{});

							moveDir.Value = float3Zero;
							data.MoveDirection[i] = moveDir;

							break;
						case 41:
							commandBuffer.AddComponent(animEntity, new AnimationPlayerHitHurt{});

							moveDir.Value = float3Zero; //SET KNOCKBACK
							data.MoveDirection[i] = moveDir;
						
							break;
					}
                
					int dirIndex = faceDir.dirIndex;
					float3 faceDirValue = faceDir.Value;
					
					//SET LIST
					// int endAnimToggle = listAnim[animIndex].EndAnimationToggle;
					entryAnim.DirIndex = dirIndex;
					entryAnim.FaceDirValue = faceDirValue;
					// entryPlayerAnim.State = state;
					// entryPlayerAnim.StartAnimationToggle = 0;

					// listAnim[animIndex] = new EntryPlayerAnim(dirIndex, faceDirValue, state, 0, endAnimToggle);
					listAnim[animIndex] = entryAnim;	

					//SET TO PLAYER (PARENT)						
					player.StartAnimationToggle = 0;
					data.Player[i] = player;
				}
#endregion 
			}
		}	
	}
}
