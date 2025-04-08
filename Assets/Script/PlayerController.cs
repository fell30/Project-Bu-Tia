using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed;
    [SerializeField] private float _turnSpeed = 360;
    [SerializeField] private Animator _animator;

    private Vector3 _input;

    void Update()
    {
        GetInput();
        UpdateAnimation();
    }

    void FixedUpdate()
    {
        Move();
        Rotate();
    }

    void GetInput()
    {
        // Mengambil input hanya pada sumbu horizontal (kiri-kanan)
        _input = new Vector3(Input.GetAxis("Horizontal"), 0, 0); // Tidak ada input untuk sumbu Z
    }

    void Move()
    {
        // Menggerakkan player hanya pada sumbu X (horizontal), tidak maju mundur
        _rb.MovePosition(transform.position + _input * _speed * Time.deltaTime);
    }

    void Rotate()
    {
        // Rotasi player sesuai dengan arah gerakannya
        if (_input != Vector3.zero)
        {
            // Rotasi ke kiri atau kanan berdasarkan input
            float rotationInput = _input.x > 0 ? 1 : -1; // Tentukan arah rotasi, 1 untuk kanan, -1 untuk kiri
            Quaternion targetRotation = Quaternion.Euler(0, rotationInput * 90, 0); // Rotasi 90 derajat di sumbu Y
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _turnSpeed * Time.deltaTime);
        }
        else
        {
            // Jika tidak ada input, rotasi kembali ke posisi awal (0 derajat)
            Quaternion targetRotation = Quaternion.Euler(0, 180, 0); // Rotasi kembali ke posisi awal
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _turnSpeed * Time.deltaTime);
        }
    }

    void UpdateAnimation()
    {
        // Update animasi berdasarkan input horizontal
        float speedValue = Mathf.Abs(_input.x); // Menggunakan nilai absolut untuk kecepatan horizontal
        _animator.SetFloat("Speed", speedValue);
    }
}
