using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    [SerializeField] private int scoreValue = 1;
    [SerializeField] private float DragForce = 1f;
    public int Damage;
    private Rigidbody rb;
    private LandHealth landHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = DragForce;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Projectile"))
        {
            ScoreManager.Instance.AddScore(scoreValue);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Destroyer"))
        {

            landHealth = other.GetComponent<LandHealth>();
            if (landHealth != null)
            {
                landHealth.TakeDamage(Damage);
                Debug.Log("Damage to land: " + Damage);
            }
            Destroy(gameObject);
        }
    }
}
