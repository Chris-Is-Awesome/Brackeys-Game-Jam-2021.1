/*
 * Author(s):
	* Chris is Awesome
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SlimeController : MonoBehaviour
{
	[Header("Refs")]
	[SerializeField] SlimeData slimeData;
	private InputController inputs;
	private Entity ent;

	[Header("Debug")]
	[SerializeField] bool hasControls;

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

		// Vars
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
		GameObject.Find("Cinemachine").GetComponent<CinemachineVirtualCamera>().Follow = transform.Find("Sprite & Physics");
	}

	public void ReleaseControl()
	{
		inputs.Disable();
		hasControls = false;
	}

	private void SwitchSlime(SlimeController slime)
	{
		Debug.Log("Switching to " + slime.gameObject.name);
		slime.TakeControl();
		ReleaseControl();
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