/*
 * Author(s):
	* Chris is Awesome
 */

using System;
using UnityEngine;

public class WalkState : AIState
{
	private Entity ent;
	private Rigidbody2D rb;
	private Animator animator;

	[Header("State-specific")]
	[SerializeField] float speed;

	private void OnEnable()
	{
		ent = GetEntity();
		rb = GetComponentInParent<Rigidbody2D>();
		animator = GetComponentInParent<Animator>();
	}

	private void OnDisable()
	{
		animator.SetFloat("MoveSpeed", 0);
	}

	private void FixedUpdate()
	{
		// Move
		rb.velocity = new Vector2(ent.movingDirection * Time.fixedDeltaTime * (speed * 10), rb.velocity.y);

		// Flip sprite
		if (ent.movingDirection != 0)
		{
			int facingDirection = ent.movingDirection >= 0 ? -1 : 1;
			ent.transform.localScale = new Vector2(facingDirection, 1);
		}

		// Start animation
		animator.SetFloat("MoveSpeed", Mathf.Abs(ent.movingDirection));
	}
}