using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    [SerializeField] private int scoreValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.Instance.AddScore(scoreValue);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Destroyer"))
        {
            Destroy(gameObject);
        }
    }
}
