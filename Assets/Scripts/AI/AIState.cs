/*
 * Author(s):
	* Chris is Awesome
 */

using UnityEngine;

public class AIState : MonoBehaviour
{
	[Header("AI")]
	public bool isDefault;
	public bool isActive;
	public bool isSolo;

	private void Start()
	{
		if (isDefault) DoActivateState();
	}

	public void DoActivateState()
	{
		isActive = true;
		enabled = true;
		GetEntity().currentAIState = this;
	}

	public void DoDeactivateState()
	{
		isActive = false;
		enabled = false;
	}

	public Entity GetEntity()
	{
		return transform.parent.GetComponent<Entity>();
	}

	public SlimeController GetSlimeController()
	{
		return transform.parent.GetComponent<SlimeController>();
	}

	private Transform GetSpriteAndPhysicsObject()
	{
		return transform.parent.Find("Sprite & Physics");
	}

	public Rigidbody2D GetRigidbody()
	{
		return GetSpriteAndPhysicsObject().GetComponent<Rigidbody2D>();
	}

	public Collider2D GetCollider()
	{
		return GetSpriteAndPhysicsObject().GetComponent<Collider2D>();
	}

	public SpriteRenderer GetRenderer()
	{
		return GetSpriteAndPhysicsObject().GetComponent<SpriteRenderer>();
	}

	public Animator GetAnimator()
	{
		return GetSpriteAndPhysicsObject().GetComponent<Animator>();
	}
}