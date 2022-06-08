using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header ("Sprite")]
    [SerializeField] private float moveSpeed;

    [Header ("Collider")]
    [SerializeField] private float xOffsetRaw;
    [SerializeField] private float yOffsetRaw;

    private Animator animator;
    private new Rigidbody2D rigidbody2D;
    private new CapsuleCollider2D collider;

    private void Awake() {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    private void Update() {
        float horizInput = Input.GetAxisRaw("Horizontal"),
            vertInput = Input.GetAxisRaw("Vertical");
        bool movingX = horizInput != 0, movingY = vertInput != 0;

        if (movingX || movingY)
        {   // Sets blend tree floats and collider offsets, prioritizes horizontal
            animator.SetFloat("Horizontal", horizInput);
            animator.SetFloat("Vertical", vertInput);

            float xOffset = movingX ? Math.Sign(horizInput) * xOffsetRaw : horizInput;
            float yOffset = movingX ? 0 : Math.Sign(vertInput) * yOffsetRaw;
            collider.offset = new Vector2(xOffset, yOffset);
        }
        animator.SetBool("Moving", movingX || movingY);

        // prioritizes moving horizontally over vertically. Moving in both shakes screen
        rigidbody2D.velocity = new Vector2(movingX ? horizInput * moveSpeed : 0, movingX ? 0 : vertInput * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision) {

    }
}