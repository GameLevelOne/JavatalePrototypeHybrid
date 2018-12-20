using Unity.Collections;
using Unity.Entities;
// using UnityEngine;
using Unity.Burst;
using Unity.Mathematics;

namespace Javatale.Prototype 
{
	public class BeeAnimatorDirectionSetterSystem : ComponentSystem 
	{
        [BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			[ReadOnlyAttribute] public ComponentArray<AnimatorDirectionComponent> AnimatorDirectionComponent;
			public ComponentArray<BeeAnimatorComponent> BeeAnimatorComponent;
		}
		[InjectAttribute] private Data data;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;

			string faceX = GameManager.settings.faceX;
			string faceY = GameManager.settings.faceY;

			for (int i=0; i<data.Length; i++) {
				Entity entity = data.Entity[i];
				BeeAnimatorComponent beeAnimatorComponent = data.BeeAnimatorComponent[i];
				AnimatorDirectionComponent animatorDirectionComponent = data.AnimatorDirectionComponent[i];

				commandBuffer.RemoveComponent<AnimatorDirectionComponent>(entity);
				GameObjectEntity.Destroy(animatorDirectionComponent);
                
				int dirIndex = animatorDirectionComponent.dirIndex;
				float3 faceDirValue = animatorDirectionComponent.dirValue;
				
				beeAnimatorComponent.animator.SetFloat(faceX, faceDirValue.x);
				beeAnimatorComponent.animator.SetFloat(faceY, faceDirValue.z);
				
				beeAnimatorComponent.currentDirIndex = dirIndex;
				beeAnimatorComponent.currentFaceDirValue = faceDirValue;
			}
		}
	}
}