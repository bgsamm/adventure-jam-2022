using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool PlayerHasControl { get; set; }

    public float moveSpeed;
    public Sprite IdleLeft;
    public Sprite IdleRight;
    public Sprite IdleUp;
    public Sprite IdleDown;

    public Vector2 Direction
    {
        get {
            if (direction == null)
                direction = Vector2.zero;
            return direction;
        }
        set {
            if (Direction.magnitude != 0)
                LastDirection = Direction;
            direction = value;
            direction.Normalize();
        }
    }
    public Vector2 LastDirection { get; private set; }

    private Sprite idleSprite;
    private Vector2 direction;
    private bool isMoving;

    private SpriteRenderer m_SpriteRenderer;
    private Animator m_Animator;
    private Rigidbody2D m_Rigidbody;

    private void Start() {
        PlayerHasControl = true;
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (PlayerHasControl) {
            float horizInput = Input.GetAxisRaw("Horizontal"),
                vertInput = Input.GetAxisRaw("Vertical");
            if (horizInput > 0)
                Direction = Vector2.right;
            else if (horizInput < 0)
                Direction = Vector2.left;
            else if (vertInput > 0)
                Direction = Vector2.up;
            else if (vertInput < 0)
                Direction = Vector2.down;
            else
                Direction = Vector2.zero;
        }

        isMoving = Direction.x != 0 || Direction.y != 0;
        bool isMovingHoriz = Mathf.Abs(Direction.x) > Mathf.Abs(Direction.y);
        // moving right
        if (isMovingHoriz && Direction.x > 0) {
            m_Animator.Play("Walk Right");
        }
        // moving left
        else if (isMovingHoriz && Direction.x < 0) {
            m_Animator.Play("Walk Left");
        }
        // moving up
        else if (!isMovingHoriz && Direction.y > 0) {
            m_Animator.Play("Walk Up");
        }
        // moving down
        else if (!isMovingHoriz && Direction.y < 0) {
            m_Animator.Play("Walk Down");
        }
        // idle
        else {
            if (Mathf.Abs(LastDirection.x) > Mathf.Abs(LastDirection.y)) {
                if (LastDirection.x > 0)
                    idleSprite = IdleRight;
                else if (LastDirection.x < 0)
                    idleSprite = IdleLeft;
            }
            else {
                if (LastDirection.y > 0)
                    idleSprite = IdleUp;
                else if (LastDirection.y < 0)
                    idleSprite = IdleDown;
            }
        }
    }

    private void FixedUpdate() {
        var delta = moveSpeed * Time.fixedDeltaTime * Direction;
        m_Rigidbody.MovePosition(m_Rigidbody.position + delta);
    }

    private void LateUpdate() {
        if (!isMoving)
            m_SpriteRenderer.sprite = idleSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision) {

    }
}