/*
 * Author(s): Chris is Awesome
 */

/* Plans:
 * Similar to LevelEventTrigger in regards to state switching events?
 */

using UnityEngine;

public class AIState : MonoBehaviour
{
	public bool isDefault;
	public bool isActive;

	private void Start()
	{
		if (isDefault) DoActivateState();
	}

	public void DoActivateState()
	{
		isActive = true;
		enabled = true;
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