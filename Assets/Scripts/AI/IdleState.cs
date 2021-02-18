/*
 * Author(s):
	* Chris is Awesome
 */

using UnityEngine;

public class IdleState : AIState
{
	private void OnEnable()
	{
		Entity ent = GetEntity();
		Rigidbody2D rb = ent.GetComponent<Rigidbody2D>();
		Animator animator = ent.GetComponent<Animator>();

		if (rb != null) rb.velocity = Vector3.zero;
		if (animator != null)
		{
			animator.SetBool("IsGrounded", true);
			animator.SetFloat("MoveSpeed", 0);
		}
	}
}