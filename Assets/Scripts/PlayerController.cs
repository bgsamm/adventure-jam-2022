using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*  Notes
    *   Can replace PlayerHasControl by disabling/enabling the script from others
    *   Can replace animator play with setting an animator bool to change animator states
    *   Can probably track the direction through the transform instead of direction/last direction
    *   Will not need sprites, animator should be able to handle switching sprites
    */

    [Header ("Sprite")]
    [SerializeField] private float moveSpeed;
    
    private Animator m_Animator;
    private Rigidbody2D m_Rigidbody;

    private void Awake() {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        float horizInput = Input.GetAxisRaw("Horizontal"), vertInput = Input.GetAxisRaw("Vertical");

        // Flips/Unflips transform (& sprite) according to direction
        if (horizInput > 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = Vector3.one;

        bool moveX = horizInput != 0; // Signals moving on X axis, X higher priority than Y

        // Can use to manipulate the state of the animator, should set sprites itself
        m_Animator.SetBool("walking", moveX || vertInput != 0);
        m_Animator.SetBool("isHorizontal", moveX); // Will switch horizontal/vertical sprite
        m_Animator.SetBool("facingFront", vertInput < 0);

        // Velocity will be accurate, and is simple
        m_Rigidbody.velocity = new Vector2(moveX ? horizInput * moveSpeed : 0, moveX ? 0 : vertInput * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision) {

    }
}