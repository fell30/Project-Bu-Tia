using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening; // Jangan lupa menambahkan namespace DOTween

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int score;
    private int highScore;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        LoadHighScore();
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        AnimateScore(); // Memanggil animasi saat skor bertambah
        UpdateUI();
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void UpdateUI()
    {
        if (scoreText) scoreText.text = "Score: " + score;
        if (highScoreText) highScoreText.text = "High Score: " + highScore;
    }

    // Fungsi untuk menambahkan animasi pada perubahan skor
    private void AnimateScore()
    {
        // Animasi untuk teks skor
        if (scoreText != null)
        {
            // Ubah ukuran font dan kemudian kembali ke ukuran normal
            scoreText.transform.DOScale(1.2f, 0.2f).OnKill(() => scoreText.transform.DOScale(1f, 0.1f)); // Membesar dan kembali ke ukuran semula

            // Efek warna dengan DOTween
            scoreText.DOColor(Color.green, 0.2f).OnKill(() => scoreText.DOColor(Color.black, 0.2f)); // Merubah warna teks
        }

        // Animasi untuk high score (jika ingin efek serupa pada high score)
        if (highScoreText != null && score > highScore)
        {
            highScoreText.transform.DOShakePosition(0.5f, 10, 10); // Efek getar ketika high score tercapai
        }
    }
}
