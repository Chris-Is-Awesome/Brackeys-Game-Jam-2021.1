/*
 * Author(s):
	* Chris is Awesome
 */

/*
 * Plans:
	* Change which slime the camera tracks
 */

using UnityEngine;

public class SlimeSwitcher : MonoBehaviour
{
	void Start()
	{
		// Activate core slime by default
		SlimeController[] slimeControllers = GetComponentsInChildren<SlimeController>();
		for (int i = 0; i < slimeControllers.Length; i++)
		{
			if (slimeControllers[i].gameObject.name == "Core Slime") slimeControllers[i].TakeControl();
		}
	}
}