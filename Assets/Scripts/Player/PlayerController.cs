using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float interactionDist;

    private Animator animator;
    private new Rigidbody2D rigidbody2D;

    private InteractionHotspot interactionHotspot;
    private Vector2 interactionOffset;

    private float pixelsPerUnit;
    private Vector2 direction;

    private void Awake() {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        pixelsPerUnit = GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;

        interactionHotspot = GetComponentInChildren<InteractionHotspot>();
        interactionOffset = interactionHotspot.transform.localPosition;
    }

    private void Update() {
        float horizInput = Input.GetAxisRaw("Horizontal"),
            vertInput = Input.GetAxisRaw("Vertical");

        bool moveX = horizInput != 0; // Signals moving on X axis, X higher priority than Y
        bool moving = moveX || vertInput != 0;
        direction = new Vector2(moveX ? horizInput : 0, moveX ? 0 : vertInput);

        // Maintain values when idle so the player continues to face the direction they were moving
        if (moving) {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            // keep interaction hotspot in front of player
            interactionHotspot.transform.localPosition = interactionOffset + interactionDist * direction;
        }
        animator.SetBool("Moving", moving);

        // Handle interactions
        var interactable = interactionHotspot.CurrentInteractable;
        if (Input.GetButtonDown("Interact") && interactable != null) {
            interactable.Interact();
        }
    }

    private void FixedUpdate() {
        var position = rigidbody2D.position + moveSpeed * Time.fixedDeltaTime * direction;
        // move the player by an integer # of pixels
        float p_x = Mathf.FloorToInt(position.x * pixelsPerUnit);
        float p_y = Mathf.FloorToInt(position.y * pixelsPerUnit);
        var p = new Vector2(p_x, p_y) / pixelsPerUnit;
        rigidbody2D.MovePosition(p);
    }
}