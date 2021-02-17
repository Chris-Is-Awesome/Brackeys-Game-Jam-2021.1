/*
 * Author(s):
	* Chris is Awesome
 */

using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
	[SerializeField] Transform stateHolder;
	private List<AIState> aiStates;

	private void Start()
	{
		if (aiStates == null || aiStates.Count < 1) aiStates = GetAllStates();

		for (int i = 0; i < aiStates.Count; i++)
		{
			if (aiStates[i].isDefault)
			{
				aiStates[i].DoActivateState();
				break;
			}
		}
	}

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

		Debug.LogWarning("No AIState found for state " + toState.ToString() + " out of " + aiStates.Count + " AIState(s).");
		return null;
	}

	private void DoChangeState(AIState fromState, AIState toState)
	{
		if (fromState != null) fromState.DoDeactivateState();
		if (toState != null) toState.DoActivateState();

		if (fromState != null) Debug.Log("[" + gameObject.name + "] Changed state from " + fromState.GetType() + " to " + toState.GetType() + "!");
		else Debug.Log("[" + gameObject.name + "] Changed state to " + toState.GetType() + "!");
	}

	public AIState GetActiveState()
	{
		if (aiStates == null) aiStates = GetAllStates();

		for (int i = 0; i < aiStates.Count; i++)
		{
			if (aiStates[i].isActive) return aiStates[i];
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