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

        bool moveX = horizInput != 0; // Signals moving on X axis, X higher priority than Y
        bool moving = moveX || vertInput != 0;
        var direction = new Vector2(moveX ? horizInput : 0, moveX ? 0 : vertInput);

        // Maintain values when idle so the player continues to face the direction they were moving
        if (moving) {
            Animator.SetFloat("Horizontal", direction.x);
            Animator.SetFloat("Vertical", direction.y);
        }
        Animator.SetBool("Moving", moving);

        Rigidbody2D.velocity = moveSpeed * direction;
    }

    private void OnTriggerEnter2D(Collider2D collision) {

    }
}