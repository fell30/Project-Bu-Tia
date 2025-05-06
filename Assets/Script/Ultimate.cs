using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Ultimate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform[] spawnPoints;
    public VideoPlayer videoPlayer;
    public GameObject VideoUltimate;
    public Image UltimateImage;
    public RenderTexture UltimateRawImage;
    public float CooldownTime;
    public float TimeUltimate;

    private float currentRotationX = 0f;
    private int spawnIndex = 0;

    void Update()
    {
        CooldownTime += Time.deltaTime;

        currentRotationX = Mathf.Clamp(currentRotationX, -30f, 60f);
        transform.localRotation = Quaternion.Euler(currentRotationX, 0f, 0f);


        if (Input.GetKeyDown(KeyCode.Space) && CooldownTime > TimeUltimate)
        {
            StartCoroutine(UltimateCombo());
            CooldownTime = 0f;

        }
    }
    private IEnumerator UltimateCombo()
    {
        videoPlayer.targetTexture = UltimateRawImage;
        videoPlayer.Prepare();

        // Tunggu sampai video siap
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        videoPlayer.time = 0; // Set ke waktu awal sebelum play

        videoPlayer.Play();

        VideoUltimate.SetActive(true);

        yield return new WaitForSeconds((float)videoPlayer.length);

        VideoUltimate.SetActive(false);
        Ultimate_MagicShoot();

    }


    private void Ultimate_MagicShoot()
    {
        if (projectilePrefab && spawnPoints.Length > 0)
        {
            // Loop melalui semua spawn points dan menembakkan proyektil dari setiap titik spawn
            foreach (Transform spawnPoint in spawnPoints)
            {
                // Membuat proyektil dari setiap spawn point dengan rotasi masing-masing
                GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
                Rigidbody rb = projectile.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    // Pastikan proyektil bergerak sesuai dengan arah masing-masing spawn point
                    rb.velocity = spawnPoint.up * 10f; // Kecepatan proyektil ke atas sesuai spawn point
                }
            }
        }
    }
}
