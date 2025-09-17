using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float moveSpeed = 10f;

    private void CanMove()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        Vector3 direction = transform.right * hInput + transform.forward * vInput;
        controller.SimpleMove(direction * moveSpeed);
    }

    private void Start()
    {
        
    }
    private void Awake()
    {
        
    }
}
