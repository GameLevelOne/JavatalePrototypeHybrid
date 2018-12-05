using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Javatale.Prototype 
{
    [SerializableAttribute]
    public struct PlayerSpawnAttackData : IComponentData 
    {
        // public Position pos;
        public MoveDirection moveDir;
        public FaceDirection faceDir;
        public MoveSpeed moveSpeed;
        public int attackIndex;
    }

    public class PlayerSpawnAttackDataComponent : ComponentDataWrapper<PlayerSpawnAttackData> {}
}
