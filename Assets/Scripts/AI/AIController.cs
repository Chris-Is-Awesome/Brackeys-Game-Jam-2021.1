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

	private void Awake()
	{
		if (aiStates == null || aiStates.Count < 1) aiStates = GetAllStates();

		for (int i = 0; i < aiStates.Count; i++)
		{
			if (aiStates[i].enabled) aiStates[i].enabled = false;
			if (aiStates[i].isDefault) aiStates[i].DoActivateState();
		}
	}

	public AIState ChangeState(string toState)
	{
		AIState newState = GetStateByName(toState);

		if (newState != null)
		{
			DoChangeState(GetActiveState(), newState);
			return newState;
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

	public AIState GetStateByName(string state)
	{
		string stateName = state.ToLower();

		if (IsStateValid(state))
		{
			for (int i = 0; i < aiStates.Count; i++)
			{
				string currState = aiStates[i].GetType().ToString().ToLower();
				if (currState.StartsWith(stateName)) return aiStates[i];
			}
		}

		return null;
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

	public bool IsStateValid(string state)
	{
		string stateName = state.ToLower();

		for (int i = 0; i < aiStates.Count; i++)
		{
			if (aiStates[i].GetType().ToString().ToLower().StartsWith(stateName)) return true;
		}

		return false;
	}

	public bool IsStateActive(string state)
	{
		if (GetStateByName(state).isActive) return true;
		return false;
	}
}