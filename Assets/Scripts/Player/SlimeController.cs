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
	private Entity ent;

	private void Awake()
	{
		ent = GetComponentInParent<Entity>();
	}

	public void Split()
	{
		Debug.Log("Splitting");
	}

	public  void  UseAbility()
	{
		Debug.Log("Using ability");
	}
}