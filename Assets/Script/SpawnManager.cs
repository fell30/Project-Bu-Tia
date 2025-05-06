using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToSpawn;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float WaveTime = 5f;
    [SerializeField] private float SpawnIntervalWave;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            spawnInterval = SpawnIntervalWave;
            Debug.Log("Spawn interval diubah menjadi 0.5 detik");
            CancelInvoke(nameof(SpawnObject));
            InvokeRepeating(nameof(SpawnObject), 0f, spawnInterval);
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObject), 0f, spawnInterval);
    }

    private void SpawnObject()
    {
        if (objectsToSpawn.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogWarning("SpawnManager: Objek atau spawn point belum diatur!");
            return;
        }

        GameObject randomObject = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(randomObject, randomSpawnPoint.position, randomSpawnPoint.rotation);
    }

    public void StopSpawning()
    {
        CancelInvoke(nameof(SpawnObject));
        Debug.Log("Spawn berhenti");
    }

}
