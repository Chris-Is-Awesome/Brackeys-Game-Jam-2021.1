/*
 * Author(s):
	* Chris is Awesome
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Imports;

public class SlimeController : MonoBehaviour
{
	[Header("Refs")]
	[SerializeField] SlimeData slimeData;
	private InputController inputs;
	private SlimeSwitcher slimeSwitcher;
	private CinemachineVirtualCamera vCam;
	private Entity ent;

	[Header("Debug")]
	[ReadOnly] public bool isActive;
	[ReadOnly] [SerializeField] bool hasControls;

	public InputController GetInputData()
	{
		return inputs;
	}

	private void Awake()
	{
		// Input stuff
		inputs = new InputController();
		inputs.Player.PreviousSlime.performed += ctx => SwitchSlime(GetOrderedSlime(false));
		inputs.Player.NextSlime.performed += ctx => SwitchSlime(GetOrderedSlime(true));
		inputs.Player.Combine.performed += ctx => CombineSlimes(GetOrderedSlime(true));

		// Vars
		slimeSwitcher = transform.parent.GetComponent<SlimeSwitcher>();
		vCam = GameObject.Find("Cinemachine").GetComponent<CinemachineVirtualCamera>();
		ent = GetComponent<Entity>();

		// Event subs
		CollisionChecker.OnCollisionEnter += HandleCollisionEnter;
	}

	private void OnDisable()
	{
		inputs.Disable();
		hasControls = false;
	}

	private void Update()
	{
		if (hasControls)
		{
			// Walk
			ent.movingDirection = inputs.Player.Movement.ReadValue<float>();

			// Jump
			if (!ent.IsStateActive("jump") && Convert.ToBoolean(inputs.Player.Jump.ReadValue<float>())) ent.SetState("jump");
		}
	}

	private SlimeController GetOrderedSlime(bool next)
	{
		List<SlimeController> orderedSlimes = new List<SlimeController>
		{
			GameObject.Find("Core Slime").GetComponent<SlimeController>(),
			GameObject.Find("Bomb Slime").GetComponent<SlimeController>()
		};

		for (int i = 0; i < orderedSlimes.Count; i++)
		{
			if (orderedSlimes[i].hasControls)
			{
				if (next)
				{
					if (i == orderedSlimes.Count - 1) return orderedSlimes[0];
					return orderedSlimes[i + 1];
				}
				else
				{
					if (i == 0) return orderedSlimes[orderedSlimes.Count - 1];
					return orderedSlimes[i - 1];
				}
			}
		}

		return null;
	}

	public void TakeControl()
	{
		inputs.Enable();
		hasControls = true;
		isActive = true;
		vCam.Follow = transform.Find("Sprite & Physics");

	}

	private void ReleaseControl()
	{
		inputs.Disable();
		hasControls = false;
		isActive = false;
	}

	private void CombineSlimes(SlimeController combineWith)
	{
		if (slimeSwitcher.GetSlimeCount() == 1)
		{
			GameObject mediumSlime = slimeSwitcher.transform.Find("Medium Slime").gameObject;
			//
		}
		else if (slimeSwitcher.GetSlimeCount() == 2)
		{
			//
		}

		transform.parent.GetComponent<SlimeSwitcher>().UpdateActiveSlimes();
	}

	private void SwitchSlime(SlimeController slime)
	{
		Debug.Log("Switching to " + slime.gameObject.name);
		slime.TakeControl();
		ReleaseControl();

		transform.parent.GetComponent<SlimeSwitcher>().UpdateActiveSlimes();
	}

	private void ReturnToCore()
	{
		Debug.Log("Returning to core");
	}

	private void HandleCollisionEnter(Collision2D other, GameObject self)
	{
		if (other.gameObject.CompareTag("Platform") && !ent.isGrounded) ent.SetState("idle");
	}
}