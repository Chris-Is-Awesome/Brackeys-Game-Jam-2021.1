/*
 * Author(s):
	* Chris is Awesome
 */

using System;
using UnityEngine;
using Imports;

public class Entity : MonoBehaviour
{
	[Serializable]
	public class StatData
	{
		[SerializeField] float maxHp;
		[SerializeField] float currHp;
		[SerializeField] float speedMultiplier;
		[SerializeField] DamageData damageData;

		// Returns max HP
		public float GetMaxHp()
		{
			return maxHp;
		}

		// Updates max HP (can overwrite)
		public void SetMaxHp(float amount, bool overwrite)
		{
			if (overwrite) maxHp = amount;
			else maxHp += amount;
		}

		// Returns current HP
		public float GetCurrHp()
		{
			return currHp;
		}

		// Updates current HP (can overwrite)
		public void SetCurrHp(float amount, bool overwrite)
		{
			if (overwrite) currHp = amount;
			else currHp += amount;
		}

		// Returns speed multiplier
		public float GetSpeedMultiplier()
		{
			return speedMultiplier;
		}

		// Updates speed multiplier
		public void SetSpeedMultiplier(float amount)
		{
			speedMultiplier = amount;
		}
	}

	[SerializeField] AIController aiController;
	[Space]
	public StatData stats;

	[Header("Debug")]
	[ReadOnly] public AIState currentAIState;
	[ReadOnly] public bool isGrounded = true;
	[ReadOnly] public float movingDirection;

	private void Start()
	{
		isGrounded = true;
	}

	private void Update()
	{
		// Idle state
		if (isGrounded && movingDirection == 0 && !IsStateActive("idle")) SetState("idle");

		// Moving
		if (movingDirection != 0 && !IsStateActive("walk")) SetState("walk");
		if (!isGrounded && movingDirection == 0)
		{
			Rigidbody2D selfRb = GetComponent<Rigidbody2D>();
			if (selfRb != null) selfRb.velocity = new Vector2(0, selfRb.velocity.y);
		}
	}

	// Resets an Entity to its default parameters
	public void ResetEntity()
	{
		// TODO: Make respawn system call this

		Debug.Log("Reset Entity " + gameObject.name);
	}

	// Sets Entity AI State
	public void SetState(string toState)
	{
		if (aiController != null) aiController.ChangeState(toState);
		else Debug.LogError("AIController is not assigned.");
	}

	public bool IsStateActive(string state)
	{
		if (aiController != null && aiController.IsStateActive(state)) return true;
		return false;
	}
}