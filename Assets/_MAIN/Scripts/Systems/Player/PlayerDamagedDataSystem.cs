using Unity.Collections;
using Unity.Entities;
// using UnityEngine;
// using Unity.Mathematics;
using Unity.Burst;
// using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerDamagedDataSystem : ComponentSystem 
	{
		[BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entities;
			[ReadOnlyAttribute] public ComponentDataArray<Player> Player;
			public ComponentDataArray<Health> Health;
			[ReadOnlyAttribute] public ComponentDataArray<DamagedData> DamagedData;
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
                Health health = data.Health[i];
				DamagedData damagedData = data.DamagedData[i];

                float damagedValue = damagedData.Value; // FOR HEALTH
                int damagedType = (int) damagedData.Type;

				float remainHP = health.Value;
				remainHP-=damagedValue;
				health.Value = remainHP;
				data.Health[i] = health;

                commandBuffer.RemoveComponent<DamagedData>(entity);
                
                switch (damagedType)
                {
                    default : // CASE 0	NORMAL		
						commandBuffer.AddComponent(entity, new AnimatorPlayerHurt {});
						GameDebug.Log("AnimatorPlayerHurt");
                        
                        break;
                }
            }
		}	
	}
}
