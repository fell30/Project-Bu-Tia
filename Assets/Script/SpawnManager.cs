using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToSpawn;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 2f;

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

    // Fungsi untuk menghentikan spawn
    public void StopSpawning()
    {
        CancelInvoke(nameof(SpawnObject)); // Menghentikan panggilan fungsi InvokeRepeating
        Debug.Log("Spawn berhenti");
    }
}
