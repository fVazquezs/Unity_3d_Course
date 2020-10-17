using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    public float rcsThrust = 200f;
    public float mainThrust = 200f;

    private Rigidbody _rigidBody;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        Thrust();
        Rotate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Fuel":
                break;
            default:
                print("Collided something not traced");
                break;
        }
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }
        else if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }
    private void Rotate()
    {
        _rigidBody.freezeRotation = true;

        float _rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            
            transform.Rotate(Vector3.forward * _rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(-Vector3.forward * _rotationThisFrame);
        }

        _rigidBody.freezeRotation = false;
    }

}
