/*
 * Author(s):
	* Chris is Awesome
 */

using UnityEngine;
using Imports;

public class JumpState : AIState
{
	private Entity ent;
	private Rigidbody2D rb;
	private Animator animator;

	[Header("State-specific")]
	[SerializeField] float jumpForce;
	[SerializeField] float jumpTimeMax;
	[ReadOnly] [SerializeField] float jumpTime;

	private void OnEnable()
	{
		ent = GetEntity();
		rb = GetRigidbody();
		animator = GetAnimator();
		jumpTime = jumpTimeMax;
	}

	private void OnDisable()
	{
		if (ent != null) ent.isGrounded = true;
		if (animator != null) animator.SetBool("IsFalling", false);
	}

	private void FixedUpdate()
	{
		// Handle jump
		if (jumpTime > 0)
		{
			rb.velocity = new Vector2(rb.velocity.x, jumpForce / 2.25f);
			jumpTime -= Time.fixedDeltaTime;

			if (GetSlimeController().GetInputData().Player.Jump.ReadValue<float>() == 0) jumpTime = 0;
		}
		else jumpTime = 0;

		// Handle animations
		if (rb.velocity.y > 0)
		{
			animator.SetBool("IsGrounded", false);
			ent.isGrounded = false;
		}
		else if (rb.velocity.y < 0) animator.SetBool("IsFalling", true);
	}
}