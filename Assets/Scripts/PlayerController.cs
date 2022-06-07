using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header ("Sprite")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private int pixelsPerUnit;
    [SerializeField] private GameObject actionHotspot;

    private Animator animator;
    private new Rigidbody2D rigidbody2D;

    public Vector2 direction, position;
    private Vector3 hotspotOffset;

    private void Awake() {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        //hotspotOffset = actionHotspot.transform.localPosition;
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
            //actionHotspot.transform.transform.localPosition = hotspotOffset + (Vector3)direction;
        }
        animator.SetBool("Moving", moving);

        //Rigidbody2D.velocity = moveSpeed * direction;
    }

    private void FixedUpdate() {
        // keep track of the player's "real" underlying position
        position += moveSpeed * Time.fixedDeltaTime * direction;
        // but only move the player visually by a whole # of pixels
        float i_x = Mathf.FloorToInt(position.x * pixelsPerUnit);
        float i_y = Mathf.FloorToInt(position.y * pixelsPerUnit);
        var p = new Vector2(i_x, i_y) / pixelsPerUnit;
        rigidbody2D.MovePosition(p);
    }

    private void OnTriggerEnter2D(Collider2D collision) {

    }
}