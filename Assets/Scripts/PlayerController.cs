using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*  Notes
    *   Can replace PlayerHasControl by disabling/enabling the script from others
    */

    [Header ("Sprite")]
    [SerializeField] private float moveSpeed;
    
    private Animator m_Animator;
    private Rigidbody2D m_Rigidbody;

    private bool moveX;
    private bool moving;

    private void Awake() {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        float horizInput = Input.GetAxisRaw("Horizontal"), vertInput = Input.GetAxisRaw("Vertical");
        moveX = horizInput != 0; // Signals moving on X axis, X higher priority than Y
        moving = moveX || vertInput != 0;

        // Flips/Unflips transform (& sprite) according to direction
        if (horizInput > 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if(moving)
            transform.localScale = Vector3.one;

        // Can use to manipulate the state of the animator, should set sprites itself
        m_Animator.SetBool("walking", moving);
        m_Animator.SetBool("isHorizontal", moveX);
        m_Animator.SetBool("facingFront", vertInput < 0);

        m_Rigidbody.velocity = new Vector2(moveX ? horizInput * moveSpeed : 0, moveX ? 0 : vertInput * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision) {

    }
}