using Unity.Collections;
using Unity.Entities;
// using UnityEngine;
// using Unity.Mathematics;
// using Unity.Burst;
// using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerDamagedDataSystem : ComponentSystem 
	{
		// [BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entities;
			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
			public ComponentDataArray<Player> Player;
			public ComponentDataArray<DamagedData> DamagedData;
		}
		[InjectAttribute] private Data data;

		// float3 float3Zero = float3.zero;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;

			for (int i=0; i<data.Length; i++)
			{
				Entity entity = data.Entities[i];
				DamagedData damagedData = data.DamagedData[i];
                Player player = data.Player[i];

                float damagedValue = damagedData.Value; // FOR HEALTH
                int damagedType = (int) damagedData.Type;

                commandBuffer.RemoveComponent<DamagedData>(entity);
                
                switch (damagedType)
                {
                    default : // CASE 0	NORMAL		
                        player.StartAnimationToggle = 41;
                        data.Player[i] = player;
                        
                        break;
                }
            }
		}	
	}
}
