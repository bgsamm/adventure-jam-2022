using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Sprite")]
    [SerializeField] private float moveSpeed;

    private GameObject currInteractable;
    private const KeyCode INTERACT_KEY = KeyCode.E;

    private Animator animator;
    private new Rigidbody2D rigidbody2D;
    private InventorySystem inventory;
    private GardenManager garden;

    private float pixelsPerUnit;
    private Vector2 direction;

    readonly int SEED_SPRITE_OFFSET = 3;
    readonly string PLOT_TAG = "Plot";
    readonly string GARDEN_TAG = "GardenManager"; 

    private void Awake() {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        inventory = GetComponent<InventorySystem>();
        garden = GameObject.FindGameObjectWithTag(GARDEN_TAG).GetComponent<GardenManager>();
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
        var position = rigidbody2D.position + moveSpeed * Time.fixedDeltaTime * direction;
        // move the player by an integer # of pixels
        float i_x = Mathf.FloorToInt(position.x * pixelsPerUnit);
        float i_y = Mathf.FloorToInt(position.y * pixelsPerUnit);
        var p = new Vector2(i_x, i_y) / pixelsPerUnit;
        rigidbody2D.MovePosition(p);
    }

    private void InteractPlant() {
        Plot plot = currInteractable.GetComponent<Plot>();
        if (!plot.occupied) {
            Debug.Log("Planting on " + gameObject.name);
            plot.Plant(garden.PlantItems[inventory.selectedSeed * SEED_SPRITE_OFFSET]);
        }
        else if (plot.readyToHarvest) {
            plot.Harvest();
        }
        else {
            plot.Water();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (currInteractable == null) {
            Debug.Log("On " + gameObject.name);
            currInteractable = collision.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (currInteractable == null)
        {
            currInteractable = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject == currInteractable) {
            Debug.Log("Off " + gameObject.name);
            currInteractable = null;
        }
    }
}