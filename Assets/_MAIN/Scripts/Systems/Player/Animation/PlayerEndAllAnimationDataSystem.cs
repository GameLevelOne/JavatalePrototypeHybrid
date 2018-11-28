using Unity.Collections;
using Unity.Entities;
// using UnityEngine;
// using Unity.Mathematics;
// using Unity.Burst;
// using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerEndAllAnimationDataSystem : ComponentSystem 
	{
		// [BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entities;
			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
			public ComponentDataArray<Player> Player;
			public ComponentDataArray<EndAllAnimationData> EndAllAnimationData;
		}
		[InjectAttribute] private Data data;

		// float3 float3Zero = float3.zero;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;

			for (int i=0; i<data.Length; i++)
			{
				Entity entity = data.Entities[i];
				Player player = data.Player[i];
				Parent parent = data.Parent[i];
				EndAllAnimationData endAllAnimationData = data.EndAllAnimationData[i];

                int endAllAnimationValue = endAllAnimationData.Value;
				int playerAnimToggleValue = player.AnimationToggleValue;

                commandBuffer.RemoveComponent<EndAllAnimationData>(entity);
                
                switch (endAllAnimationValue)
                {
                    default : // CASE 0			
                        if (playerAnimToggleValue == 0)
						{
                            commandBuffer.AddComponent(entity, new AnimationPlayerIdleStand{});
						}
                        
                        break;
                }
            }
		}	
	}
}
