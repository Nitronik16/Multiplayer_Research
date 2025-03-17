using Unity.Burst;
using Unity.Entities;
using Unity.NetCode;
using UnityEngine;

[WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation)]
partial struct TestNetCodeEntitiesServerSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        
    }

    //[BurstCompile]
    public void OnUpdate(ref SystemState state)
    {

        EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);

        foreach ((RefRO<SimpleRPC> simpleRpc, RefRO<ReceiveRpcCommandRequest> recieveRPCCommandRequest, Entity entity)
            in SystemAPI.Query<RefRO<SimpleRPC>, RefRO<ReceiveRpcCommandRequest>>().WithEntityAccess()) 
        { 

            Debug.Log("Recieved Rpc: " + simpleRpc.ValueRO.value + "::" + recieveRPCCommandRequest.ValueRO.SourceConnection);
            entityCommandBuffer.DestroyEntity(entity);

        }

        entityCommandBuffer.Playback(state.EntityManager);
    }
}
