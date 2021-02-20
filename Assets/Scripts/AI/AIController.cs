/*
 * Author(s):
	* Chris is Awesome
 */

using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
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
			DoChangeState(GetActiveStates(), newState);
			return newState;
		}

		Debug.LogWarning("No AIState found for state " + toState.ToString() + " out of " + aiStates.Count + " AIState(s).");
		return null;
	}

	private void DoChangeState(List<AIState> fromStates, AIState toState)
	{
		for (int i = 0; i < fromStates.Count; i++)
		{
			if (toState.isSolo) fromStates[i].DoDeactivateState();
		}
		toState.DoActivateState();

		//Debug.Log("[" + gameObject.name + "] Changed state to " + toState.GetType() + "!");
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

	public List<AIState> GetActiveStates()
	{
		if (aiStates == null) aiStates = GetAllStates();

		List<AIState> activeStates = new List<AIState>();

		for (int i = 0; i < aiStates.Count; i++)
		{
			if (aiStates[i].isActive) activeStates.Add(aiStates[i]);
		}

		return activeStates;
	}

	public List<AIState> GetAllStates()
	{
		List<AIState> allAIStates = new List<AIState>();
		Transform aiStatesHolder = transform.Find("AIStates");

		foreach (AIState aiState in aiStatesHolder.GetComponents<AIState>())
		{
			allAIStates.Add(aiState);
		}

		return allAIStates;
	}

	public bool IsStateValid(string state)
	{
		if (aiStates == null || aiStates.Count < 1) aiStates = GetAllStates();
		string stateName = state.ToLower();

		for (int i = 0; i < aiStates.Count; i++)
		{
			if (aiStates[i].GetType().ToString().ToLower().StartsWith(stateName)) return true;
		}

		return false;
	}

	public bool IsStateActive(string state)
	{
		AIState foundState = GetStateByName(state);
		if (foundState != null && foundState.isActive) return true;
		return false;
	}
}