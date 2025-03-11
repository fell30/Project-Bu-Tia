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
        Look();
        UpdateAnimation();
    }

    void FixedUpdate()
    {
        Move();
    }

    void GetInput()
    {
        _input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    void Look()
    {
        if (_input != Vector3.zero)
        {
            var relative = (transform.position + _input) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
        }
    }

    void Move()
    {
        _rb.MovePosition(transform.position + (transform.forward * _input.magnitude) * _speed * Time.deltaTime);
    }

    void UpdateAnimation()
    {
        float speedValue = _input.magnitude;
        _animator.SetFloat("Speed", speedValue);
    }
}
