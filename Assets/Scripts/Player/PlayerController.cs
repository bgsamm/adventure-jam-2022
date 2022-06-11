using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Sprite")]
    [SerializeField] private float moveSpeed;

    public Interactable CurrentInteractable
    {
        get => _interactable;
        set {
            if (_interactable != null)
                _interactable.StopCanInteract();
            _interactable = value;
        }
    }
    private Interactable _interactable;

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

        bool moveX = horizInput != 0; // Signals moving on X axis, X higher priority than Y
        bool moving = moveX || vertInput != 0;
        direction = new Vector2(moveX ? horizInput : 0, moveX ? 0 : vertInput);

        // Maintain values when idle so the player continues to face the direction they were moving
        if (moving) {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
        }
        animator.SetBool("Moving", moving);

        if (CurrentInteractable != null && Input.GetButtonDown("Interact")) {
            CurrentInteractable.Interact();
        }
    }

    private void FixedUpdate() {
        var position = rigidbody2D.position + moveSpeed * Time.fixedDeltaTime * direction;
        // move the player by an integer # of pixels
        float i_x = Mathf.FloorToInt(position.x * pixelsPerUnit);
        float i_y = Mathf.FloorToInt(position.y * pixelsPerUnit);
        var p = new Vector2(i_x, i_y) / pixelsPerUnit;
        rigidbody2D.MovePosition(p);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        UpdateInteractable(collision);
    }

    private void OnTriggerStay2D(Collider2D collision) {
        UpdateInteractable(collision);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (CurrentInteractable != null && collision.gameObject == CurrentInteractable.gameObject) {
            CurrentInteractable = null;
        }
    }

    private void UpdateInteractable(Collider2D collision) {
        var interactable = collision.GetComponent<Interactable>();
        if (CurrentInteractable == null || CompareDist(interactable.gameObject, CurrentInteractable.gameObject) < 0) {
            CurrentInteractable = interactable;
            CurrentInteractable.StartCanInteract();
        }
    }

    private float CompareDist(GameObject a, GameObject b) {
        return Vector3.Distance(a.transform.position, transform.position) - Vector3.Distance(b.transform.position, transform.position);
    }
}