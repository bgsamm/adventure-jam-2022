using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Sprite")]
    [SerializeField] private float moveSpeed;

    private GameObject currInteractable;
    private const KeyCode INTERACT_KEY = KeyCode.E;

    private Animator animator;
    private new Rigidbody2D rigidbody2D;

    private float pixelsPerUnit;
    private Vector2 direction, position;

    private Plant selectedSeed; 

    const string PLOT_TAG = "Plot";

    private void Awake() {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        // TODO can probably following to awake, if sprite is not initialized by a script
        pixelsPerUnit = GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
    }

    private void Update() {
        /*
         * TODO
         * Call to move time along when exiting shop or sleeping
         */

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

        if (currInteractable != null && Input.GetKeyDown(INTERACT_KEY)) {
            if (currInteractable.CompareTag(PLOT_TAG)) {
                InteractPlant();
            }
            else {
                Debug.Log("No possible interactions with " + currInteractable.name);
            }
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

    private void InteractPlant() {
        Plot plot = currInteractable.GetComponent<Plot>();
        if (!plot.occupied) {
            plot.Plant(selectedSeed);
        }
        else if (plot.readyToHarvest) {
            plot.Harvest();
        }
        else {
            plot.Water();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(PLOT_TAG)) {
            currInteractable = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject == currInteractable) {
            currInteractable = null;
        }
    }
}