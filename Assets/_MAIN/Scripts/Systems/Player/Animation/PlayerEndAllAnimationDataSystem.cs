using Unity.Collections;
using Unity.Entities;
// using UnityEngine;
using Unity.Burst;

namespace Javatale.Prototype 
{
	public class PlayerEndAllAnimationDataSystem : ComponentSystem 
	{
		[BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			[ReadOnlyAttribute] public ComponentDataArray<Player> Player;
			[ReadOnlyAttribute] public ComponentDataArray<EndAllAnimationData> EndAllAnimationData;
		}
		[InjectAttribute] private Data data;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;

			for (int i=0; i<data.Length; i++)
			{
				Entity entity = data.Entity[i];
				Player player = data.Player[i];
				EndAllAnimationData endAllAnimationData = data.EndAllAnimationData[i];

                int endAllAnimationValue = endAllAnimationData.Value;
				int playerAnimToggleValue = player.AnimationToggleValue;

                commandBuffer.RemoveComponent<EndAllAnimationData>(entity);
                
                switch (endAllAnimationValue)
                {
                    default : // CASE 0			
                        // if (playerAnimToggleValue == 0)
						// {
                            commandBuffer.AddComponent(entity, new AnimationPlayerIdleStand{});
						// }
                        
                        break;
                }
            }
		}	
	}
}
