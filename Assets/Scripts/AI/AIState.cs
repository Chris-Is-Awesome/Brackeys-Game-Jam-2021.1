/*
 * Author(s): Chris is Awesome
 */

/* Plans:
 * Similar to LevelEventTrigger in regards to state switching events?
 */

using UnityEngine;

public class AIState : MonoBehaviour
{
	[TextArea] [SerializeField] string description;
	public string animName;
	[SerializeField] bool isDefault;
	[SerializeField] bool isActive;

	public bool IsActive
	{
		get
		{
			return isActive;
		}
	}

	private void Start()
	{
		if (isDefault) DoActivateState();
	}

	public void DoActivateState()
	{
		isActive = true;
	}

	public void DoDeactivateState()
	{
		isActive = false;
	}
}