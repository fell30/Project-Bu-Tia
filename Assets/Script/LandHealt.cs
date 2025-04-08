using UnityEngine;
using UnityEngine.UI; // Menambahkan namespace untuk menggunakan UI seperti Image

public class LandHealth : MonoBehaviour
{
    [SerializeField] private int health = 10; // Nilai awal health tanah
    [SerializeField] private int damagePerObject = 1; // Damage yang berkurang per objek yang dihancurkan
    [SerializeField] private Image healthBarImage; // Referensi ke Image UI (health bar)

    // Fungsi untuk mengurangi health tanah
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DestroyLand();
        }
        UpdateHealthBar();
    }

    // Fungsi untuk menghancurkan tanah jika health habis
    private void DestroyLand()
    {
        Debug.Log("Tanah hancur!");
        Destroy(gameObject); // Hancurkan objek tanah ketika health habis
    }

    // Fungsi untuk memperbarui health bar
    private void UpdateHealthBar()
    {
        if (healthBarImage != null)
        {
            // Mengubah fillAmount sesuai dengan health
            healthBarImage.fillAmount = (float)health / 100f; // 10 adalah nilai health penuh
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
