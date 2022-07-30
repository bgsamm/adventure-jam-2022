using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    // I don't like enabling/disabling the PlayerController directly for a few reasons:
    // - Any script that wants to disable control would
    //      need to obtain a reference to the player
    // - If multiple objects have a PlayerController component,
    //      external scripts would need references to each of them
    // - If multiple components are involved in player control,
    //      external scripts would need to disable all of them
    // I feel a static boolean allows for a single point of access that other scripts
    // involved in player control can then reference, simplifying the above cases.
    public static bool playerHasControl;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [Header("Interaction")]
    [SerializeField] private TextMeshProUGUI interactMessage;
    [SerializeField] private float interactionDist;

    private Animator animator;
    private new Rigidbody2D rigidbody2D;

    private InteractionHotspot interactionHotspot;
    private Vector2 interactionOffset;

    private float pixelsPerUnit;
    private Vector2 direction;

    private float stepTime;

    private UnityAudioManager sfxManager => ResourceLocator.instance.SFXManager;

    private void Awake() {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        pixelsPerUnit = GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;

        interactionHotspot = GetComponentInChildren<InteractionHotspot>();
        interactionOffset = interactionHotspot.transform.localPosition;

        playerHasControl = true;
    }

    private void Update() {
        // check if player currently has control
        if (!playerHasControl) {
            // stop movement
            direction = Vector2.zero;
            animator.SetBool("Moving", false);
            // clear interaction text
            interactMessage.text = "";
            return;
        }

        float horizInput = Input.GetAxisRaw("Horizontal"),
            vertInput = Input.GetAxisRaw("Vertical");

        bool moving = horizInput != 0 || vertInput != 0;
        direction = new Vector2(horizInput, vertInput);

        // Maintain values when idle so the player continues to face the direction they were moving
        if (moving) {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            // keep interaction hotspot in front of player
            interactionHotspot.transform.localPosition = interactionOffset + interactionDist * direction;
            //footsteps
            if (Time.time - stepTime > 0.4) {
                if (sfxManager.onBridge)
                    sfxManager.PlayOneShot(sfxManager.footstepsWood);
                else
                    sfxManager.PlayOneShot(sfxManager.footstepsGrass);
                stepTime = Time.time;
            }
        }

        animator.SetBool("Moving", moving);

        // Handle interactions
        var interactable = interactionHotspot.CurrentInteractable;
        if (interactable != null) {
            interactMessage.text = interactable.InteractMessage;
            if (Input.GetButtonDown("Interact")) {
                Debug.Log($"Interacting with {interactable.name}");
                interactable.Interact();
            }
        }
        else {
            interactMessage.text = "";
        }
    }

    private void FixedUpdate() {
        var position = rigidbody2D.position + moveSpeed * Time.fixedDeltaTime * direction;
        // Move the player by an integer # of pixels
        float p_x = Mathf.RoundToInt(position.x * pixelsPerUnit);
        float p_y = Mathf.RoundToInt(position.y * pixelsPerUnit);
        var p = new Vector2(p_x, p_y) / pixelsPerUnit;
        rigidbody2D.MovePosition(p);
    }
}