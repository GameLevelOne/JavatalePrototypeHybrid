using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
// using Unity.Transforms;
// using UnityEngine;
// using Unity.Burst;

namespace Javatale.Prototype 
{
	public class PlayerInputDirectionSystem : JobComponentSystem 
	{
        [InjectAttribute] private PlayerAnimationSetBarrier playerAnimationSetBarrier;

		// [BurstCompileAttribute]
		struct PlayerInputDirectionJob : IJobProcessComponentDataWithEntity <PlayerInputDirection, MoveDirection, FaceDirection, Parent>
		{
            [ReadOnlyAttribute] public EntityCommandBuffer commandBuffer;

			// public List<Entity> childEntitiesInGame;

			public bool isUpDirectionHeld;
			public bool isDownDirectionHeld;
			public bool isRightDirectionHeld;
			public bool isLeftDirectionHeld;

			public float3 float3Zero;
			// public float3 float3Up;
			// public float3 float3Down;
			public float3 float3Left;
			public float3 float3Right;
			public float3 float3Front;
			public float3 float3Back;
			public float dirX;
			public float dirZ;

			public void Execute (
                [ReadOnlyAttribute] Entity entity, //IJobProcessComponentDataWithEntity
                [ReadOnlyAttribute] int index, //IJobProcessComponentDataWithEntity
				ref PlayerInputDirection playerInputDir,
				ref MoveDirection moveDir,
				ref FaceDirection faceDir,
				[ReadOnlyAttribute] ref Parent parent)
			{
				float3 currentDir = playerInputDir.Value;
				float currentDirX = currentDir.x;
				float currentDirZ = currentDir.z;
				float dirX = 0f;
				float dirZ = 0f;
				
				if (isUpDirectionHeld) dirZ += 1f;
				if (isDownDirectionHeld) dirZ -= 1f;
				if (isRightDirectionHeld) dirX += 1f;
				if (isLeftDirectionHeld) dirX -= 1f;

				if (currentDirX != dirX || currentDirZ != dirZ) 
				{
					if (dirX != 0f && dirZ != 0f) 
					{//DIAGONAL FACING
						if (currentDir.x==0f) 
						{//PREVIOUS MOVEMENT IS VERTICAL
							if (dirZ == -1f) 
							{
								faceDir.DirIndex = 0;//FACE DOWN
								faceDir.Value = float3Back;
							}
							else 
							{
								faceDir.DirIndex = 2;//FACE UP
								faceDir.Value = float3Front;
							}
						} else {//PREVIOUS MOVEMENT IS HORIZONTAL
							if (dirX == -1f) 
							{
								faceDir.DirIndex = 1;//FACE LEFT
								faceDir.Value = float3Left;
							}
							else 
							{
								faceDir.DirIndex = 3;//FACE RIGHT
								faceDir.Value = float3Right;
							}
						}
					} 
					else if (dirX != 0f) 
					{
						if (dirX == -1f) 
						{//FACE LEFT
							faceDir.DirIndex = 1;
							faceDir.Value = float3Left;
						} 
						else if (dirX == 1f) 
						{//FACE RIGHT
							faceDir.DirIndex = 3;
							faceDir.Value = float3Right;
						}

						commandBuffer.AddComponent(entity, new AnimatorNotZeroMovement { 
							dirIndex = faceDir.DirIndex,
							dirValue = faceDir.Value	 
						});
					}
					else if (dirZ != 0f)
					{
						if (dirZ == -1f) 
						{//FACE DOWN
							faceDir.DirIndex = 0;
							faceDir.Value = float3Back;
						} 
						else if (dirZ == 1f) 
						{//FACE UP
							faceDir.DirIndex = 2;
							faceDir.Value = float3Front;
						}

						commandBuffer.AddComponent(entity, new AnimatorNotZeroMovement { 
							dirIndex = faceDir.DirIndex,
							dirValue = faceDir.Value	 
						});
					}
					else 
					{ // IDLE
						commandBuffer.AddComponent(entity, new AnimatorZeroMovement {});
					}

					// int parentEntityIndex = parent.EntityIndex;

                    // List<GameObjectEntity> childEntitiesInGame = GameManager.childEntitiesInGame;
					// Entity childEntity = childEntitiesInGame[parentEntityIndex].Entity;

					// if (dirX != 0f || dirZ != 0f) 
					// {
                   	//  	commandBuffer.AddComponent(childEntity, new AnimationPlayerMoveRun());
                   	//  	commandBuffer.AddComponent(childEntity, new AnimatorPlayerDirection { dirIndex = faceDir.DirIndex, dirValue = faceDir.Value });
					// } 
					// else 
					// {
                   	//  	commandBuffer.AddComponent(childEntity, new AnimationPlayerIdleStand());
					// }

					float3 direction = new float3 (dirX, 0f, dirZ);
					
					playerInputDir.Value = direction;
					moveDir.Value = direction;
				}
			}
		}

		protected override JobHandle OnUpdate (JobHandle inputDeps)
		{
			PlayerInputDirectionJob playerInputDirectionJob = new PlayerInputDirectionJob
			{
                commandBuffer = playerAnimationSetBarrier.CreateCommandBuffer(),

				isUpDirectionHeld = GameInput.IsUpDirectionHeld,
				isDownDirectionHeld = GameInput.IsDownDirectionHeld,
				isRightDirectionHeld = GameInput.IsRightDirectionHeld,
				isLeftDirectionHeld = GameInput.IsLeftDirectionHeld,

				float3Zero = float3.zero,
				// float3Up = new float3 (0f, 1f, 0f),
				// float3Down = new float3 (0f, -1f, 0f),
				float3Right = new float3 (1f, 0f, 0f),
				float3Left = new float3 (-1f, 0f, 0f),
				float3Front = new float3 (0f, 0f, 1f),
				float3Back = new float3 (0f, 0f, -1f),

				// vector3Zero = Vector3.zero,
				// vector3Up = Vector3.up,
				// vector3Down = Vector3.down,
				// vector3Left = Vector3.left,
				// vector3Right = Vector3.right,
				// vector3Front = Vector3.forward,
				// vector3Back = Vector3.back,
			};

			JobHandle playerInputDirectionHandle = playerInputDirectionJob.Schedule(this, inputDeps);

			return playerInputDirectionHandle;
		}
	}
}
