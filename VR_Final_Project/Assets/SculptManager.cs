using UnityEngine;
using Unity.Netcode;

public class SculptManager : NetworkBehaviour
{
    public GameObject cubePrefab;
    public GameObject spherePrefab;
    public Transform spawnPoint;

    public void SpawnCube()
    {
        if (!NetworkManager.Singleton || !NetworkManager.Singleton.IsListening)
        {
            SpawnCubeLocal();
            return;
        }

        if (IsServer || IsHost)
        {
            SpawnCubeServer();
        }
        else
        {
            SpawnCubeServerRpc();
        }
    }

    public void SpawnSphere()
    {
        if (!NetworkManager.Singleton || !NetworkManager.Singleton.IsListening)
        {
            SpawnSphereLocal();
            return;
        }

        if (IsServer || IsHost)
        {
            SpawnSphereServer();
        }
        else
        {
            SpawnSphereServerRpc();
        }
    }

    void SpawnCubeServer()
    {
        if (cubePrefab == null || spawnPoint == null) return;

        GameObject go = Instantiate(cubePrefab, spawnPoint.position, spawnPoint.rotation);
        NetworkObject netObj = go.GetComponent<NetworkObject>();
        if (netObj != null) netObj.Spawn();
    }

    void SpawnSphereServer()
    {
        if (spherePrefab == null || spawnPoint == null) return;

        GameObject go = Instantiate(spherePrefab, spawnPoint.position, spawnPoint.rotation);
        NetworkObject netObj = go.GetComponent<NetworkObject>();
        if (netObj != null) netObj.Spawn();
    }

    [ServerRpc(RequireOwnership = false)]
    void SpawnCubeServerRpc(ServerRpcParams rpcParams = default)
    {
        SpawnCubeServer();
    }

    [ServerRpc(RequireOwnership = false)]
    void SpawnSphereServerRpc(ServerRpcParams rpcParams = default)
    {
        SpawnSphereServer();
    }

    void SpawnCubeLocal()
    {
        if (cubePrefab == null || spawnPoint == null) return;
        Instantiate(cubePrefab, spawnPoint.position, spawnPoint.rotation);
    }

    void SpawnSphereLocal()
    {
        if (spherePrefab == null || spawnPoint == null) return;
        Instantiate(spherePrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
