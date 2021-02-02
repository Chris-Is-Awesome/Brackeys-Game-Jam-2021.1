﻿using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private enum PlayerStates
	{
		Idle,
		Run,
		Air,
		Die
	}

	[Header("References")]
	private InputController inputController;
	[SerializeField] Animator selfAnimator;
	[SerializeField] Rigidbody2D selfRigidbody;
	[SerializeField] CircleCollider2D groundCheck;

	[Header("Physics")]
	[SerializeField] float airGravity;
	[SerializeField] float groundedGravity;
	[SerializeField] float moveSpeed;
	[SerializeField] float jumpForce;

	[Header("Debug")]
	[SerializeField] PlayerStates playerState;
	[SerializeField] bool isGrounded;

	private void Awake()
	{
		inputController = new InputController();
		inputController.Player.Jump.started += ctx => Jump();
		inputController.Player.Pause.started += ctx => Pause();
	}

	private void OnEnable()
	{
		inputController.Enable();
	}

	private void OnDisable()
	{
		inputController.Disable();
	}

	private void FixedUpdate()
	{
		// Check for movement
		float movementDirection = inputController.Player.Movement.ReadValue<float>() * Time.fixedDeltaTime;
		if (movementDirection == 0) StoppedMoving();
		else Move(movementDirection);
	}

	private void Move(float direction)
	{
		// Move
		selfRigidbody.velocity = new Vector2(direction * (moveSpeed * 10), selfRigidbody.velocity.y);
		playerState = PlayerStates.Run;
		selfAnimator.SetBool("isRunning", true);

		// Flip sprite
		if (direction > 0) transform.localScale = Vector3.one;
		else if (direction < 0) transform.localScale = new Vector3(-1, 1, 1);
	}

	private void StoppedMoving()
	{
		if (isGrounded)
		{
			// Return to idle
			selfAnimator.SetBool("isRunning", false);
			playerState = PlayerStates.Idle;
		}
	}

	private void Jump()
	{
		if (isGrounded)
		{
			// Jump
			selfRigidbody.AddForce(new Vector2(0f, jumpForce * 25));
			selfAnimator.SetTrigger("Jump");
		}
	}

	private void Pause()
	{
		// Pause game
		Debug.Log("Player is paused! In the future, this will bring up pause menu UI. Click resume button to continue.");
		Debug.Break();
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Platform"))
		{
			if (groundCheck.IsTouching(other.collider) && !isGrounded)
			{
				// Set player to grounded state
				playerState = PlayerStates.Idle;
				selfRigidbody.gravityScale = groundedGravity;
				isGrounded = true;
				selfAnimator.SetBool("isGrounded", true);
			}
		}
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Platform"))
		{
			if (!groundCheck.IsTouching(other.collider) && isGrounded)
			{
				// Set player to air state
				playerState = PlayerStates.Air;
				selfRigidbody.gravityScale = airGravity;
				isGrounded = false;
				selfAnimator.SetBool("isGrounded", false);
			}
		}
	}
}