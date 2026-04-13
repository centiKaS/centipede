using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed=5f
    private RigidBody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = Getomponent<RigidBody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        rb.velocity = moveInput * moveSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        animator.SetBool("isWalking", true);
        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }

        moveInput = context.ReadValue<Vector2>();
        animator.Setfloat("InputX", moveInput.x);
        animator.Setfloat("InputY", moveInput.y);
    }
}