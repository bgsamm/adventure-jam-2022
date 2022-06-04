using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool PlayerHasControl { get; set; }

    public float moveSpeed;

    public Vector2 Direction;
    private Rigidbody2D m_Rigidbody;

    private void Start() {
        PlayerHasControl = true;
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
    }

    private void FixedUpdate() {
        var delta = moveSpeed * Time.fixedDeltaTime * Direction;
        m_Rigidbody.MovePosition(m_Rigidbody.position + delta);
    }

    private void OnTriggerEnter2D(Collider2D collision) {

    }
}