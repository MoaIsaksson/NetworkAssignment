using System;
using Unity.Netcode;
using UnityEngine;

public class CollectibleItems : NetworkBehaviour
{
    private bool isCollected;
   private void OnTriggerEnter(Collider collision)
    {
        if(!IsServer) return;

        if (collision.CompareTag("Player"))
        {
            ulong ClientId = collision.GetComponent<NetworkObject>().OwnerClientId;
            CollectClientRpc(ClientId);
        }
    }


    [Rpc(SendTo.Everyone)]
    private void CollectClientRpc(ulong ClientId)
    {
        if (isCollected) return;

        isCollected = true;
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();

        if (scoreManager != null)
        {
            scoreManager.AddScore(5, ClientId);
        }

        DestroyCollectibleServerRpc();
    }

    [Rpc(SendTo.Everyone)]
    private void DestroyCollectibleServerRpc()
    {
       Destroy(gameObject);
    }
}
