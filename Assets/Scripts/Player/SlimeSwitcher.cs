/*
 * Author(s):
	* Chris is Awesome
 */

using System.Collections.Generic;
using UnityEngine;
using Imports;

public class SlimeSwitcher : MonoBehaviour
{
	[ReadOnly] [SerializeField] List<SlimeController> activeSlimes = new List<SlimeController>();

    public bool foundBomb = false;
    public bool foundIce = false;
    public bool foundSticky = false;
    public GameObject activeCore;
    [ReadOnly] public int coreSize = 1;

    void Start()
	{
		// Activate core slime by default
		SlimeController[] slimeControllers = GetComponentsInChildren<SlimeController>();
		for (int i = 0; i < slimeControllers.Length; i++)
		{
			if (slimeControllers[i].gameObject.name == "Core Slime") slimeControllers[i].TakeControl();
		}
        activeCore = transform.Find("Core Slime").gameObject;
		UpdateActiveSlimes();
	}

	public void UpdateActiveSlimes()
	{
		activeSlimes.Clear();

		foreach (SlimeController slime in GetComponentsInChildren<SlimeController>())
		{
			if (slime.isActive) activeSlimes.Add(slime);
		}
	}

	public int GetSlimeCount()
	{
		return activeSlimes.Count;
	}
}