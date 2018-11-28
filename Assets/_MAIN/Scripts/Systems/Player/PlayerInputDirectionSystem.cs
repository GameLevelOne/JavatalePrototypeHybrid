using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
// using Unity.Mathematics;
// using Unity.Transforms;
using UnityEngine;
using Unity.Burst;

namespace Javatale.Prototype 
{
	public class PlayerInputDirectionSystem : JobComponentSystem 
	{
		[BurstCompileAttribute]
		struct PlayerInputDirectionJob : IJobProcessComponentData <PlayerInputDirection, MoveDirection, FaceDirection, Player>
		{
			public bool isUpDirectionHeld;
			public bool isDownDirectionHeld;
			public bool isRightDirectionHeld;
			public bool isLeftDirectionHeld;

			public Vector3 vector3Zero;
			public Vector3 vector3Up;
			public Vector3 vector3Down;
			public Vector3 vector3Left;
			public Vector3 vector3Right;
			public Vector3 vector3Front;

			public Vector3 vector3Back;
			public float dirX;
			public float dirZ;

			public void Execute (
				ref PlayerInputDirection playerInputDir,
				ref MoveDirection moveDir,
				ref FaceDirection faceDir,
				ref Player player)
			{
				Vector3 currentDir = playerInputDir.Value;
				
				if (isUpDirectionHeld) dirZ += 1f;
				if (isDownDirectionHeld) dirZ -= 1f;
				if (isRightDirectionHeld) dirX += 1f;
				if (isLeftDirectionHeld) dirX -= 1f;

				Vector3 direction = new Vector3 (dirX, 0f, dirZ);

				if (currentDir != direction) 
				{
					if (dirX!=0f && dirZ!=0f) 
					{//DIAGONAL FACING
						if (currentDir.x==0f) 
						{//PREVIOUS MOVEMENT IS VERTICAL
							if (dirZ == -1f) 
							{
								faceDir.dirIndex = 0;//FACE DOWN
								faceDir.Value = vector3Back;
							}
							else 
							{
								faceDir.dirIndex = 2;//FACE UP
								faceDir.Value = vector3Front;
							}
						} else {//PREVIOUS MOVEMENT IS HORIZONTAL
							if (dirX == -1f) 
							{
								faceDir.dirIndex = 1;//FACE LEFT
								faceDir.Value = vector3Left;
							}
							else 
							{
								faceDir.dirIndex = 3;//FACE RIGHT
								faceDir.Value = vector3Right;
							}
						}
					} 
					else if (dirZ == -1f) 
					{//FACE DOWN
						faceDir.dirIndex = 0;
						faceDir.Value = vector3Back;
					} 
					else if (dirZ == 1f) 
					{//FACE UP
						faceDir.dirIndex = 2;
						faceDir.Value = vector3Front;
					} 
					else if (dirX == -1f) 
					{//FACE LEFT
						faceDir.dirIndex = 1;
						faceDir.Value = vector3Left;
					} 
					else if (dirX == 1f) 
					{//FACE RIGHT
						faceDir.dirIndex = 3;
						faceDir.Value = vector3Right;
					}

					if (direction != vector3Zero) 
					{
						// faceDir.Value = direction;
						player.StartAnimationToggle = 2;
					} 
					else 
					{
						player.StartAnimationToggle = 1;
					}
					
					playerInputDir.Value = direction;
					moveDir.Value = direction;
				}
			}
		}

		protected override JobHandle OnUpdate (JobHandle inputDeps)
		{
			PlayerInputDirectionJob playerInputDirectionJob = new PlayerInputDirectionJob
			{
				isUpDirectionHeld = GameInput.IsUpDirectionHeld,
				isDownDirectionHeld = GameInput.IsDownDirectionHeld,
				isRightDirectionHeld = GameInput.IsRightDirectionHeld,
				isLeftDirectionHeld = GameInput.IsLeftDirectionHeld,

				vector3Zero = Vector3.zero,
				vector3Up = Vector3.up,
				vector3Down = Vector3.down,
				vector3Left = Vector3.left,
				vector3Right = Vector3.right,
				vector3Front = Vector3.forward,
				vector3Back = Vector3.back,
				dirX = 0f,
				dirZ = 0f

				// direction = new Vector3(
				// 	GameInput.IsRightDirectionHeld ? 1f : GameInput.IsLeftDirectionHeld ? -1f : 0f, 
				// 	0f, 
				// 	GameInput.IsUpDirectionHeld ? 1f : GameInput.IsDownDirectionHeld ? -1f : 0f)
			};

			JobHandle playerInputDirectionHandle = playerInputDirectionJob.Schedule(this, inputDeps);

			return playerInputDirectionHandle;
		}
	}
}
