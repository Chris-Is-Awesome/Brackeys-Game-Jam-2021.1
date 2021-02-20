using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using Imports;

public class PlayerController : MonoBehaviour
{
	private enum PlayerStates
	{
		Idle,
		Run,
		Jump,
		Fall,
		Die
	}

	[Header("GameManager")]
	[SerializeField] private static GameManager gameManager;

	[Header("References")]
	private InputController inputController;
	[SerializeField] Animator selfAnimator;
	[SerializeField] Rigidbody2D selfRigidbody;
	[SerializeField] CircleCollider2D groundCheck;
	[SerializeField] CinemachineVirtualCamera virtualCamera;

	[Header("Physics")]
	[SerializeField] float gravity;
	[SerializeField] float moveSpeed;
	[SerializeField] float runSpeed;
	[SerializeField] float jumpForce;
	[SerializeField] float maxJumpTime;
	private float currJumpTime;
	[SerializeField] float fastFallSpeed;

	[Header("Debug")]
	[SerializeField] PlayerStates playerState;
	[SerializeField] bool isGrounded;
	[SerializeField] bool isFastFalling;
	private bool isJumping;
	private bool isMoving;
	[ConditionalField("isMoving", false, true)] private float direction;

	private void Awake()
	{
		gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

		inputController = new InputController();
		inputController.Player.Pause.started += ctx => Pause();
		inputController.Testing.Reset.started += ctx => Reset();
		selfRigidbody.gravityScale = gravity;
		currJumpTime = maxJumpTime;
	}

	private void OnEnable()
	{
		inputController.Enable();
	}

	private void OnDisable()
	{
		inputController.Disable();
	}

	private void Update()
	{
		// Check if jumping in corner
		if (isGrounded && Mathf.Abs(selfRigidbody.velocity.y) > 0.1) isGrounded = false;
		if (!isGrounded && selfRigidbody.velocity.y == 0 && !isJumping && playerState != PlayerStates.Idle) ChangeState(PlayerStates.Idle);

		// Check for jumping
		isJumping = Convert.ToBoolean(inputController.Player.Jump.ReadValue<float>());
	}

	private void FixedUpdate()
	{
		// Handle movement
		direction = inputController.Player.Movement.ReadValue<float>() * Time.fixedDeltaTime;
		HandleMovement();

		// Handle jumping
		if (isJumping) HandleJump();

        //Check if ascending
        selfAnimator.SetBool("isAscending", selfRigidbody.velocity.y>0);
	}

	private void ChangeState(PlayerStates toState)
	{
		switch (toState)
		{
			case PlayerStates.Idle:
				isGrounded = true;
				currJumpTime = maxJumpTime;
				if (!isFastFalling) selfRigidbody.gravityScale = gravity;
				selfAnimator.SetBool("isGrounded", true);
				selfAnimator.SetBool("isRunning", false);
				break;
			case PlayerStates.Run:
				selfAnimator.SetBool("isRunning", true);
				break;
			case PlayerStates.Jump:
				isGrounded = false;
				selfAnimator.SetBool("isGrounded", false);
                // selfAnimator.SetTrigger("Jump"); //Might need this later if a jump animation is made, but for now this is handled with isAscending
				break;
			case PlayerStates.Fall:
				isGrounded = false;
				selfAnimator.SetBool("isGrounded", false);
				break;
			case PlayerStates.Die:
				break;
		}

		playerState = toState;
	}

	private void HandleMovement()
	{
		if (direction != 0 && !isMoving) isMoving = true;
		else if (direction == 0 && isMoving) isMoving = false;

		if (isMoving) DoMove();
		if (!isMoving)
		{
			if (selfRigidbody.velocity.x != 0) selfRigidbody.velocity = new Vector2(0, selfRigidbody.velocity.y);

			if (isGrounded) ChangeState(PlayerStates.Idle);
		}
	}

	private void DoMove()
	{
		// Flip sprite
		if (direction > 0) transform.localScale = Vector3.one;
		else if (direction < 0) transform.localScale = new Vector3(-1, 1, 1);

		// Move
		selfRigidbody.velocity = new Vector2(direction * (moveSpeed * 10), selfRigidbody.velocity.y);

		if (playerState == PlayerStates.Idle) ChangeState(PlayerStates.Run);
	}

	private void DoRun(bool doRun)
	{
		if (doRun) moveSpeed += runSpeed;
		else moveSpeed -= runSpeed;
	}

	private void HandleJump()
	{
		// If jumping
		if (isJumping)
		{
			if (currJumpTime > 0)
			{
				DoJump();
				currJumpTime -= Time.fixedDeltaTime;
			}
			else currJumpTime = 0;
		}
	}

	private void DoJump()
	{
		// Jump
		selfRigidbody.velocity = new Vector2(selfRigidbody.velocity.x, jumpForce / 2.25f);
		ChangeState(PlayerStates.Jump);
	}

	private void DoFastFall(bool doFastFall)
	{
		if (doFastFall)
		{
			selfRigidbody.gravityScale += fastFallSpeed;
			isFastFalling = true;
		}
		else
		{
			selfRigidbody.gravityScale = gravity;
			isFastFalling = false;
		}
	}

	private void Pause()
	{
		// Pause game
		Debug.Log("Player is paused! In the future, this will bring up pause menu UI. Click resume button to continue.");
		Debug.Break();
	}

	private void Reset()
	{
		Debug.Log("Resetting scene.");
		SceneManager.LoadScene("Test");
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		// Ground check
		if (other.gameObject.CompareTag("Platform"))
		{
			if (!isGrounded)
			{
				if (groundCheck.IsTouching(other.collider) && other.GetContact(0).normal.y > 0 && playerState != PlayerStates.Idle)
				{
					// Set player to grounded state
					ChangeState(PlayerStates.Idle);
				}
			}
		}
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Platform"))
		{
			if (!groundCheck.IsTouching(other.collider) && playerState != PlayerStates.Jump && isGrounded)
			{
				// Set player to air state
				ChangeState(PlayerStates.Fall);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		//if (other.CompareTag("CameraTrackStop")) virtualCamera.Follow = null;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("CameraTrackStop")) gameManager.GetRoomManager().ReloadStandardCheckPointSolo();
	}
}