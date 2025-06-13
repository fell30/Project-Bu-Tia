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
    [SerializeField] private float darkExposure = 0.1f; // Exposure untuk skybox gelap
    [SerializeField] private float darkDuration = 3f; // Durasi skybox gelap (detik)
    [SerializeField] private float transitionDuration = 1f; // Durasi transisi (detik)
    private float originalExposure; // Menyimpan exposure asli
    private Material originalSkybox; // Menyimpan skybox asli

    private float currentRotationX = 0f;
    private int spawnIndex = 0;

    void Start()
    {
        // Simpan exposure dan skybox awal
        originalExposure = RenderSettings.skybox.GetFloat("_Exposure");
        originalSkybox = RenderSettings.skybox;
    }

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
        // Mainkan video ultimate terlebih dahulu
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

        // Tunggu sampai video selesai
        yield return new WaitForSeconds((float)videoPlayer.length);

        VideoUltimate.SetActive(false);

        // Transisi menuju skybox gelap
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / transitionDuration;
            float newExposure = Mathf.Lerp(originalExposure, darkExposure, t);
            RenderSettings.skybox.SetFloat("_Exposure", newExposure);
            DynamicGI.UpdateEnvironment();
            yield return null;
        }

        // Pastikan exposure benar-benar mencapai nilai gelap
        RenderSettings.skybox.SetFloat("_Exposure", darkExposure);
        DynamicGI.UpdateEnvironment();

        // Jalankan ultimate magic shoot
        Ultimate_MagicShoot();

        // Tunggu durasi gelap (misalnya 3 detik)
        yield return new WaitForSeconds(darkDuration);

        // Transisi kembali ke exposure asli
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / transitionDuration;
            float newExposure = Mathf.Lerp(darkExposure, originalExposure, t);
            RenderSettings.skybox.SetFloat("_Exposure", newExposure);
            DynamicGI.UpdateEnvironment();
            yield return null;
        }

        // Pastikan exposure kembali ke nilai asli
        RenderSettings.skybox.SetFloat("_Exposure", originalExposure);
        DynamicGI.UpdateEnvironment();
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