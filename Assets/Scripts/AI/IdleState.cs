/*
 * Author(s):
	* Chris is Awesome
 */

using UnityEngine;

public class IdleState : AIState
{
	private void OnEnable()
	{
		Rigidbody2D rb = GetRigidbody();
		Animator animator = GetAnimator();

		if (rb != null) rb.velocity = Vector3.zero;
		if (animator != null)
		{
			animator.SetBool("IsGrounded", true);
			animator.SetFloat("MoveSpeed", 0);
		}
	}
}