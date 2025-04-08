using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    [SerializeField] private int scoreValue = 1;
    [SerializeField] private float DragForce = 1f;
    public int Damage;
    private Rigidbody rb;
    private LandHealth landHealth; // Komponen untuk mengakses kesehatan tanah

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
            // Cek apakah objek tanah ada di bawah dan mengurangi health tanah
            landHealth = other.GetComponent<LandHealth>(); // Dapatkan komponen LandHealth dari objek yang memiliki tag "Destroyer"
            if (landHealth != null)
            {
                landHealth.TakeDamage(Damage);
                Debug.Log("Damage to land: " + Damage); // Debug log untuk melihat damage yang diterima tanah
            }
            Destroy(gameObject);
        }
    }
}
