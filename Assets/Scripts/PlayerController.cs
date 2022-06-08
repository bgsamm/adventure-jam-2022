using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header ("Sprite")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject actionHotspot;

    private Animator animator;
    private new Rigidbody2D rigidbody2D;

    private float pixelsPerUnit;
    private Vector2 direction;

    private void Awake() {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        pixelsPerUnit = GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
    }

    private void Update() {
        float horizInput = Input.GetAxisRaw("Horizontal"),
            vertInput = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", horizInput * moveSpeed);
        animator.SetFloat("Vertical", vertInput * moveSpeed);
        animator.SetBool("Moving", horizInput != 0 || vertInput != 0);
        rigidbody2D.velocity = new Vector2(horizInput * moveSpeed, vertInput * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision) {

    }
}