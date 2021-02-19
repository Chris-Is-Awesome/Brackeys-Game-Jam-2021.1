/*
 * Author(s):
	* Chris is Awesome
 */

using UnityEngine;

public class AIState : MonoBehaviour
{
	[Header("AI")]
	public bool isDefault;
	public bool isActive;
	public bool isSolo;

	private void Start()
	{
		if (isDefault) DoActivateState();
	}

	public void DoActivateState()
	{
		isActive = true;
		enabled = true;
		GetEntity().currentAIState = this;
	}

	public void DoDeactivateState()
	{
		isActive = false;
		enabled = false;
	}

	public Entity GetEntity()
	{
		return GetComponentInParent<Entity>();
	}
}