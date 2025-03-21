using Unity.Burst;
using Unity.Entities;
using UnityEngine;
using Unity.NetCode;

[WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation)]
partial struct TestNetCodeEntitiesClientSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        
    }

    //[BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            //send rpc
            Entity rpcEntity = state.EntityManager.CreateEntity();
            state.EntityManager.AddComponentData(rpcEntity, new SimpleRPC
            {
                value = 56
            });
            state.EntityManager.AddComponentData(rpcEntity, new SendRpcCommandRequest());
            Debug.Log("Sending RPC...");
        }
    }
}
