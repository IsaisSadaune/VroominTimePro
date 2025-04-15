using UnityEngine;

public class SpawnPointLogic : MonoBehaviour
{
    public int targetPlayer;

    void OnEnable()
    {
         AddSpawnPoint();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void AddSpawnPoint()
    {
        MultiplayerManager.Instance.spawnPoint[targetPlayer-1] = transform;

    }
}
