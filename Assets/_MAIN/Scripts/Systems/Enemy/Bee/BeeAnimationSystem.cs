using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	// public class AnimationBarrier : BarrierSystem {}

	public class BeeAnimationSystem : ComponentSystem 
	{
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray AnimationEntities;
			public ComponentDataArray<Bee> Bee;
			public ComponentDataArray<MoveDirection> MoveDirection;
			[ReadOnlyAttribute] public ComponentDataArray<FaceDirection> FaceDirection;
			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
		}
		[InjectAttribute] private Data data;

		// float3 float3Zero = float3.zero;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;
			List<EntryAnimation> listAnim = GameManager.entitiesAnimation;

			for (int i=0; i<data.Length; i++)
			{
				Entity animEntity = data.AnimationEntities[i];
				Bee bee = data.Bee[i];
				MoveDirection moveDir = data.MoveDirection[i];
				FaceDirection faceDir = data.FaceDirection[i];
				Parent parent = data.Parent[i];

				int animIndex = parent.AnimIndex;
				EntryAnimation entryAnim = listAnim[animIndex];
				
				int beeStartAnimToggle = bee.StartAnimationToggle;
				// int beeEndAnimToggle = bee.EndAnimationToggle;
				// int beeAnimToggleValue = bee.AnimationToggleValue;

				if (beeStartAnimToggle != 0) 
				{
					switch (beeStartAnimToggle) 
					{
						case 1:
							commandBuffer.AddComponent(animEntity, new AnimationBeeIdleFly{});
							break;
						case 2:
							commandBuffer.AddComponent(animEntity, new AnimationBeeMoveFly{});
							break;
					}
                
					int dirIndex = faceDir.dirIndex;
					float3 faceDirValue = faceDir.Value;
					
					entryAnim.DirIndex = dirIndex;
					entryAnim.FaceDirValue = faceDirValue;
					
					listAnim[animIndex] = entryAnim;

					bee.StartAnimationToggle = 0;
					data.Bee[i] = bee;
				}
			}
		}	
	}
}
