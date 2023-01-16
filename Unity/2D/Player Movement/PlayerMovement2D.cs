using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// This is a generic Player Movement Controller for top-down 2D movement
// intended to support up to 8-way movement animation
public class PlayerMovement2D : MonoBehaviour { 
    private Rigidbody2D rgbd2d;
    private Animator animator;
    // Player action using Unity Input System
    private PlayerActions playerActions;
    
    [SerializeField] private float speed = 4f;
    [HideInInspector] public Vector2 movementVector;

    // Set up player movement system before start
    private void Awake() {
        rgbd2d = GetComponent<Rigidbody2D>();
        playerActions = new PlayerActions();
        movementVector = new Vector2();
        animator = GetComponentInChildren<Animator>();
    }

    void Update() {
        InputAction action = playerActions.Player_Map.Movement;
        UpdateAnimator(action);
    }

    // Use FixedUpdate to ensure physics-based updates occur at the same frequenct as the physics systems
    private void FixedUpdate() {
        // Move player using normalized vector and speed
        // Fixed delta time is used to ensure movement is frame-independent and will occur at consistent speeds
        rgbd2d.MovePosition(rgbd2d.position + movementVector * speed * Time.fixedDeltaTime);
    }

    private void UpdateAnimator(InputAction action) {
        movementVector = action.ReadValue<Vector2>().normalized;

        // Start short delay when finger is lifted
        if (action.triggered && (movementVector.x == 0f || movementVector.y == 0f)) {
            StartCoroutine("DelayedIdleUpdate");
        }

        // Update animator if player is moving
        if (movementVector != Vector2.zero) {
            animator.SetFloat("Horizontal", movementVector.x);
            animator.SetFloat("Vertical", movementVector.y);
        }

        // Primary boolean to transition player between idle and moving animation states
        animator.SetBool("IsMoving", (movementVector.x != 0 || movementVector.y != 0));
    }

    private IEnumerator DelayedIdleUpdate() {
        yield return new WaitForSeconds(0.1f);

        if (movementVector.x != 0f || movementVector.y != 0f) {
            animator.SetFloat("LastHorizontal", movementVector.x);
            animator.SetFloat("LastVertical", movementVector.y);
        }
    }

    // Player Action Input Enabler
    private void OnEnable() {
        playerActions.Player_Map.Enable();
    }

    // Player Action Input Disabler
    private void OnDisable() {
        playerActions.Player_Map.Disable();
    }
}
