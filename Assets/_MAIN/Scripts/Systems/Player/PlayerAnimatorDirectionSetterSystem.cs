using Unity.Collections;
using Unity.Entities;
// using UnityEngine;
using Unity.Burst;
using Unity.Mathematics;

namespace Javatale.Prototype 
{
	public class PlayerAnimatorDirectionSetterSystem : ComponentSystem 
	{
        [BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			[ReadOnlyAttribute] public ComponentArray<AnimatorDirectionComponent> AnimatorDirectionComponent;
			public ComponentArray<PlayerAnimatorComponent> PlayerAnimatorComponent;
		}
		[InjectAttribute] private Data data;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;
			
			string faceX = Constants.AnimatorParameter.Float.FACE_X;
			string faceY = Constants.AnimatorParameter.Float.FACE_Y;

			for (int i=0; i<data.Length; i++) {
				Entity entity = data.Entity[i];
				PlayerAnimatorComponent playerAnimatorComponent = data.PlayerAnimatorComponent[i];
				AnimatorDirectionComponent animatorDirectionComponent = data.AnimatorDirectionComponent[i];

				commandBuffer.RemoveComponent<AnimatorDirectionComponent>(entity);
				GameObjectEntity.Destroy(animatorDirectionComponent);
				// UpdateInjectedComponentGroups();
                
				int dirIndex = animatorDirectionComponent.dirIndex;
				float3 faceDirValue = animatorDirectionComponent.dirValue;
				
				playerAnimatorComponent.animator.SetFloat(faceX, faceDirValue.x);
				playerAnimatorComponent.animator.SetFloat(faceY, faceDirValue.z);
				
				playerAnimatorComponent.currentDirIndex = dirIndex;
				playerAnimatorComponent.currentFaceDirValue = faceDirValue;
			}
		}
	}
}