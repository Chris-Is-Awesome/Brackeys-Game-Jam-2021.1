/*
 * Author(s): Chris is Awesome
 */

/* Plans:
 * Allow switching states (takes args: Entity entity, AIState fromState, AIState toState)
 */

using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
	[SerializeField] Transform stateHolder;
	private List<AIState> aiStates;

	public AIState ChangeState(string toState)
	{
		if (aiStates == null || aiStates.Count < 1) aiStates = GetAllStates();

		// Check if state exists
		for (int i = 0; i < aiStates.Count; i++)
		{
			if (aiStates[i].GetType().ToString().StartsWith(toState.ToString()))
			{
				DoChangeState(GetActiveState(), aiStates[i]);
				return aiStates[i];
			}
		}

		Debug.LogWarning("No AIState found for state '" + toState.ToString() + "' out of " + aiStates.Count + " AIState(s).");
		return null;
	}

	private void DoChangeState(AIState fromState, AIState toState)
	{
		fromState.DoDeactivateState();
		toState.DoActivateState();

		Debug.Log("Changed state from '" + fromState.GetType() + "' to '" + toState.GetType() + "'!");
	}

	public AIState GetActiveState()
	{
		if (aiStates == null) aiStates = GetAllStates();

		for (int i = 0; i < aiStates.Count; i++)
		{
			if (aiStates[i].IsActive) return aiStates[i];
		}

		Debug.LogWarning("No AIState is active, returning null.");
		return null;
	}

	public List<AIState> GetAllStates()
	{
		List<AIState> allAIStates = new List<AIState>();
		Transform aiStatesHolder = stateHolder;

		foreach (AIState aiState in aiStatesHolder.GetComponents<AIState>())
		{
			allAIStates.Add(aiState);
		}

		return allAIStates;
	}
}