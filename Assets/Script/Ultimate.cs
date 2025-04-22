using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform[] spawnPoints;

    private float currentRotationX = 0f;
    private int spawnIndex = 0;

    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");


        currentRotationX -= verticalInput * rotationSpeed * Time.deltaTime;
        currentRotationX = Mathf.Clamp(currentRotationX, -30f, 60f);
        transform.localRotation = Quaternion.Euler(currentRotationX, 0f, 0f);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Ultimate_MagicShoot();

        }
    }


    private void Ultimate_MagicShoot()
    {
        if (projectilePrefab && spawnPoints.Length > 0)
        {

            Transform spawnPoint = spawnPoints[spawnIndex];


            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            if (rb != null)
            {

                rb.velocity = spawnPoint.up * 10f;
            }


            spawnIndex = (spawnIndex + 1) % spawnPoints.Length;
        }
    }
}
