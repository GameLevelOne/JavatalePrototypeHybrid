using Unity.Collections;
using Unity.Entities;
// using UnityEngine;
// using Unity.Mathematics;
using Unity.Burst;

namespace Javatale.Prototype 
{
	public class PlayerEndAttackAnimationDataSystem : ComponentSystem 
	{
		[BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entity;
			public ComponentDataArray<Player> Player;
			[ReadOnlyAttribute] public ComponentDataArray<EndAttackAnimationData> EndAttackAnimationData;
		}
		[InjectAttribute] private Data data;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;
            
			int maxPlayerAttackIndex = GameManager.settings.maxPlayerAttackIndex;

			for (int i=0; i<data.Length; i++)
			{
				Entity entity = data.Entity[i];
				Player player = data.Player[i];
				EndAttackAnimationData endAttackAnimationData = data.EndAttackAnimationData[i];

                commandBuffer.RemoveComponent<EndAttackAnimationData>(entity);

                int endAttackAnimationValue = endAttackAnimationData.Value;
                
                switch (endAttackAnimationValue)
                {
                    default : // CASE 0		
                        commandBuffer.AddComponent(entity, new PlayerInputDirection{});
                        commandBuffer.AddComponent(entity, new PlayerInputAttack{});

                        int attackIndex = player.AttackIndex;

						if (attackIndex >= maxPlayerAttackIndex) attackIndex = 0; 
						else attackIndex++;

						player.AttackIndex = attackIndex;
                        player.AnimationToggleValue = 0;
                        data.Player[i] = player;
                        
                        break;
                }
            }
		}	
	}
}
