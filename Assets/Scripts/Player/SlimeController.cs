/*
 * Author(s):
	* Chris is Awesome
 */

using System;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
	[Header("Refs")]
	[SerializeField] SlimeData slimeData;
	private InputController inputs;
	private Entity ent;

	private void Awake()
	{
		inputs = new InputController();
		inputs.Player.Split.started += ctx => Split();
		inputs.Player.Ability.started += ctx => UseAbility();

		ent = GetComponentInParent<Entity>();
	}

	private void OnEnable()
	{
		inputs.Enable();
	}

	private void OnDisable()
	{
		inputs.Disable();
	}

	private void Update()
	{
		if (!ent.IsStateActive("jump") && Convert.ToBoolean(inputs.Player.Jump.ReadValue<float>())) Jump();
	}

	private void Jump()
	{
		ent.SetState("Jump");
	}

	private void Split()
	{
		Debug.Log("Splitting");
	}

	private void  UseAbility()
	{
		Debug.Log("Using ability");
	}
}