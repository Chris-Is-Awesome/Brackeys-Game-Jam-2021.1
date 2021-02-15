/*
* Author(s): Chris is Awesome
*/

/* Plans:
 * Basic stats for ent (hp, damage, etc.)
 * Reset to defaults function (resets Transform & stats)
 * Initiates AI state switch
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
	[SerializeField] [ReadOnly] AIState currentAIState;

	// Resets an Entity to its default parameters
	public void ResetEntity()
	{
		// TODO: Make respawn system call this

		Debug.Log("Reset Entity " + gameObject.name);
	}

	// Sets Entity AI State
	public void SetState(string toState)
	{
		if (aiController != null)
		{
			AIState newState = aiController.ChangeState(toState);
			if (newState != null) currentAIState = newState;
		}
		else Debug.LogError("AIController is not assigned.");
	}
}