using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header ("Sprite")]
    [SerializeField] private float moveSpeed;

    private Animator Animator;
    private Rigidbody2D Rigidbody2D;

    private void Awake() {
        Animator = GetComponent<Animator>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        float horizInput = Input.GetAxisRaw("Horizontal"),
            vertInput = Input.GetAxisRaw("Vertical");

        bool moving = horizInput != 0 || vertInput != 0;
        // Maintain values when idle so the player continues to face the direction they were moving
        if (moving) {
            Animator.SetFloat("Horizontal", horizInput);
            Animator.SetFloat("Vertical", vertInput);

            // Mirror sprite when moving right
            if (horizInput > 0)
                transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = Vector3.one;
        }
        Animator.SetBool("Moving", moving);

        bool moveX = horizInput != 0; // Signals moving on X axis, X higher priority than Y
        Rigidbody2D.velocity = new Vector2(moveX ? horizInput : 0, moveX ? 0 : vertInput) * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision) {

    }
}