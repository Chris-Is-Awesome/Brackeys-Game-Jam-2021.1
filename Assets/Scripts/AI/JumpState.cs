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
		rb = GetComponentInParent<Rigidbody2D>();
		animator = GetComponentInParent<Animator>();
		jumpTime = jumpTimeMax;
		animator.SetBool("IsGrounded", false);
		ent.isGrounded = false;
	}

	private void OnDisable()
	{
		if (ent != null) ent.isGrounded = true;
	}

	private void FixedUpdate()
	{
		if (jumpTime > 0)
		{
			rb.velocity = new Vector2(rb.velocity.x, jumpForce / 2.25f);
			jumpTime -= Time.fixedDeltaTime;
		}
		else if (!animator.GetBool("IsFalling"))
		{
			jumpTime = 0;
			animator.SetBool("IsFalling", true);
		}
	}
}