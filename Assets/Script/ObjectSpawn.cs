using System.Runtime.CompilerServices;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    [SerializeField] private int scoreValue = 1;
    [SerializeField] private float DragForce = 1f;
    private Rigidbody rb;




    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = DragForce;
    }

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
