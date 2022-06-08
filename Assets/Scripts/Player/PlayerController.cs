using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header ("Sprite")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject actionHotspot;

    private GameObject currInteractable;
    private const KeyCode INTERACT_KEY = KeyCode.E;

    // [Header ("SFX")]

    private Animator animator;
    private new Rigidbody2D rigidbody2D;

    private float pixelsPerUnit;
    private Vector2 direction, position;
    private Vector3 hotspotOffset;

    private void Awake() {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        //hotspotOffset = actionHotspot.transform.localPosition;
        // TODO can probably following to awake, if sprite is not initialized by a script
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
            //actionHotspot.transform.transform.localPosition = hotspotOffset + (Vector3)direction;
        }
        animator.SetBool("Moving", moving);

        checkInteractable();
        if(currInteractable != null && Input.GetKeyDown(INTERACT_KEY))
        {
            // logic for interacting, maybe new function
        }
    }

    private void FixedUpdate() {
        // keep track of the player's "real" underlying position,
        position += moveSpeed * Time.fixedDeltaTime * direction;
        // but only move the actual player object by an integer # of pixels
        float i_x = Mathf.FloorToInt(position.x * pixelsPerUnit);
        float i_y = Mathf.FloorToInt(position.y * pixelsPerUnit);
        var p = new Vector2(i_x, i_y) / pixelsPerUnit;
        rigidbody2D.MovePosition(p);
    }

    private void OnTriggerEnter2D(Collider2D collision) {

    }

    private void checkInteractable()
    {
        /* Function to check for interactables
         * Hightlight interactable here?
         *  Could save last interactable
         *      If none currently colliding: unhighlight, forget
         *  Should work because should only interact one thing at time
         *  Make sure to select new interactables over saved
        */
        return;
    }
}