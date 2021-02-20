/*
 * Author(s):
	* Chris is Awesome
 */

using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
	public delegate void CollisionEnter(Collision2D other, GameObject self);
	public static event CollisionEnter OnCollisionEnter;
	public delegate void CollisionStay(Collision2D other, GameObject self);
	public static event CollisionStay OnCollisionStay;
	public delegate void CollisionExit(Collision2D other, GameObject self);
	public static event CollisionExit OnCollisionExit;

	public delegate void TriggerEnter(Collider2D other, GameObject self);
	public static event TriggerEnter OnTriggerEnter;
	public delegate void TriggerStay(Collider2D other, GameObject self);
	public static event TriggerStay OnTriggerStay;
	public delegate void TriggerExit(Collider2D other, GameObject self);
	public static event TriggerExit OnTriggerExit;

	private void OnCollisionEnter2D(Collision2D other)
	{
		OnCollisionEnter?.Invoke(other, gameObject);
	}

	private void OnCollisionStay2D(Collision2D other)
	{
		OnCollisionStay?.Invoke(other, gameObject);
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		OnCollisionExit?.Invoke(other, gameObject);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		OnTriggerEnter?.Invoke(other, gameObject);
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		OnTriggerStay?.Invoke(other, gameObject);
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		OnTriggerExit?.Invoke(other, gameObject);
	}
}