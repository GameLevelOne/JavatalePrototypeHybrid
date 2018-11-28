using Unity.Collections;
using Unity.Entities;
// using UnityEngine;
// using Unity.Mathematics;
// using Unity.Burst;
// using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerEndAttackAnimationDataSystem : ComponentSystem 
	{
		// [BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entities;
			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
			public ComponentDataArray<Player> Player;
			public ComponentDataArray<EndAttackAnimationData> EndAttackAnimationData;
		}
		[InjectAttribute] private Data data;

		// float3 float3Zero = float3.zero;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;
            
			int maxPlayerAttackIndex = GameManager.settings.maxPlayerAttackIndex;

			for (int i=0; i<data.Length; i++)
			{
				Entity entity = data.Entities[i];
				Player player = data.Player[i];
				Parent parent = data.Parent[i];
				EndAttackAnimationData endAttackAnimationData = data.EndAttackAnimationData[i];

                int endAllAnimationValue = endAttackAnimationData.Value;

                commandBuffer.RemoveComponent<EndAttackAnimationData>(entity);
                
                switch (endAllAnimationValue)
                {
                    default : // CASE 0		
                        commandBuffer.AddComponent(entity, new PlayerInputDirection{});
                        commandBuffer.AddComponent(entity, new PlayerInputAttack{});

                        int attackIndex = player.AttackIndex >= maxPlayerAttackIndex ? 0 : player.AttackIndex+1;
                        player.AttackIndex = attackIndex;
                        player.AnimationToggleValue = 0;
                        data.Player[i] = player;
                        
                        break;
                }
            }
		}	
	}
}
