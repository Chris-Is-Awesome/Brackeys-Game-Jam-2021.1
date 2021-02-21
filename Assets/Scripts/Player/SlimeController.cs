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

    [Header("Properties")]
    [SerializeField] bool core;
    [SerializeField] bool bomb;
    [SerializeField] bool ice;
    Animator myAnim;
    int coreSize=1;

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

        //GW stuff
        myAnim = GetComponentInChildren<Animator>();
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

            // Detonate
            if (bomb)
            {

            }

            // Combine

            // Split
		}
	}

	private SlimeController GetOrderedSlime(bool next)
	{
        List<SlimeController> orderedSlimes = new List<SlimeController>
        {
            GameObject.Find("Player").transform.Find("Core Slime").GetComponent<SlimeController>(),
            GameObject.Find("Player").transform.Find("Bomb Slime").GetComponent<SlimeController>(),
            GameObject.Find("Player").transform.Find("Ice Slime").GetComponent<SlimeController>(),
            GameObject.Find("Player").transform.Find("Medium Slime").GetComponent<SlimeController>(),
            GameObject.Find("Player").transform.Find("Big Slime").GetComponent<SlimeController>()
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
            

        GameObject mediumSlime = GameObject.Find("Player").transform.Find("Medium Slime").gameObject;
        GameObject bigSlime = GameObject.Find("Player").transform.Find("Big Slime").gameObject;
        //
        int targetSize = 0;
        if (core)
        {
            if (slimeSwitcher.foundBomb)
            {
                GameObject bombSlime = slimeSwitcher.transform.Find("Bomb Slime").gameObject;
                targetSize++;
                bombSlime.SetActive(false);
                mediumSlime.GetComponent<SlimeController>().bomb = true;
                bigSlime.GetComponent<SlimeController>().bomb = true;
            }
            if (slimeSwitcher.foundIce)
            {
                GameObject iceSlime = slimeSwitcher.transform.Find("Ice Slime").gameObject;
                targetSize++;
                iceSlime.SetActive(false);
                mediumSlime.GetComponent<SlimeController>().ice = true;
                bigSlime.GetComponent<SlimeController>().ice = true;
            }
            if (targetSize==1)
            {
                if (slimeSwitcher.coreSize == 1)
                {
                    mediumSlime.SetActive(true);
                    mediumSlime.transform.position = transform.position;
                    mediumSlime.GetComponent<SlimeController>().TakeControl();
                    slimeSwitcher.coreSize = 2;
                    slimeSwitcher.activeCore = mediumSlime;
                }
                else if (slimeSwitcher.coreSize ==2)
                {
                    bigSlime.SetActive(true);
                    bigSlime.transform.position = transform.position;
                    bigSlime.GetComponent<SlimeController>().TakeControl();
                    slimeSwitcher.coreSize = 3;
                    slimeSwitcher.activeCore = bigSlime;
                }
            }
            if (targetSize==2)
            {
                bigSlime.SetActive(true);
                bigSlime.transform.position = transform.position;
                bigSlime.GetComponent<SlimeController>().TakeControl();
                slimeSwitcher.coreSize = 3;
                slimeSwitcher.activeCore = bigSlime;
            }
            slimeSwitcher.UpdateActiveSlimes();
            gameObject.SetActive(false);
        }
        if (gameObject==slimeSwitcher.transform.Find("Bomb Slime").gameObject)
        {
            if (slimeSwitcher.coreSize == 1)
            {
                mediumSlime.SetActive(true);
                mediumSlime.transform.position = slimeSwitcher.activeCore.transform.position;
                mediumSlime.GetComponent<SlimeController>().TakeControl();
                mediumSlime.GetComponent<SlimeController>().bomb = true;
                slimeSwitcher.coreSize = 2;
                slimeSwitcher.activeCore.SetActive(false);
                slimeSwitcher.activeCore = mediumSlime;
            }
            else if (slimeSwitcher.coreSize == 2)
            {
                bigSlime.SetActive(true);
                bigSlime.transform.position = slimeSwitcher.activeCore.transform.position;
                bigSlime.GetComponent<SlimeController>().TakeControl();
                bigSlime.GetComponent<SlimeController>().bomb = true;
                slimeSwitcher.coreSize = 3;
                slimeSwitcher.activeCore.SetActive(false);
                slimeSwitcher.activeCore = bigSlime;
            }
            gameObject.SetActive(false);
        }
        if (gameObject == slimeSwitcher.transform.Find("Ice Slime").gameObject)
        {
            if (slimeSwitcher.coreSize == 1)
            {
                mediumSlime.SetActive(true);
                mediumSlime.transform.position = slimeSwitcher.activeCore.transform.position;
                mediumSlime.GetComponent<SlimeController>().TakeControl();
                mediumSlime.GetComponent<SlimeController>().ice = true;
                slimeSwitcher.coreSize = 2;
                slimeSwitcher.activeCore.SetActive(false);
                slimeSwitcher.activeCore = mediumSlime;
            }
            else if (slimeSwitcher.coreSize == 2)
            {
                bigSlime.SetActive(true);
                bigSlime.transform.position = slimeSwitcher.activeCore.transform.position;
                bigSlime.GetComponent<SlimeController>().TakeControl();
                bigSlime.GetComponent<SlimeController>().ice = true;
                slimeSwitcher.coreSize = 3;
                slimeSwitcher.activeCore.SetActive(false);
                slimeSwitcher.activeCore = bigSlime;
            }
            gameObject.SetActive(false);
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