using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Imports;

public class LevelEventTrigger : MonoBehaviour
{
	#region Enums
	public enum TriggerConditions
	{
		MonoBehaviour,
		CollisionEnter,
		CollisionStay,
		CollisionExit,
		TriggerEnter,
		TriggerStay,
		TriggerExit,
		KeyPress,
		KeyPressDown,
		KeyPressUp,
		TimeInterval,
		OnEnable,
		OnDisable,
		BecameVisible,
		BecameInvisible,
		OnEventStart,
		OnEventEnd,
	}

	public enum Effects
	{
		NoEffect,
		OnTimer,
	}
	#endregion

	#region Enum Lists
	List<TriggerConditions> collisionConds = new List<TriggerConditions>()
	{
		TriggerConditions.CollisionEnter,
		TriggerConditions.CollisionStay,
		TriggerConditions.CollisionExit,
	};

	List<TriggerConditions> triggerConds = new List<TriggerConditions>()
	{
		TriggerConditions.TriggerEnter,
		TriggerConditions.TriggerStay,
		TriggerConditions.TriggerExit,
	};
	#endregion

	#region Serialized Vars
	[SerializeField]
	[TextArea]
	string description;

	[Header("How should event be triggered?")]
	[SerializeField]
	[Tooltip("How should this event be triggered?")]
	TriggerConditions eventTrigger = TriggerConditions.MonoBehaviour;
	[SerializeField]
	[ConditionalField("eventTrigger", false, TriggerConditions.CollisionEnter, TriggerConditions.CollisionStay, TriggerConditions.CollisionExit, TriggerConditions.TriggerEnter, TriggerConditions.TriggerStay, TriggerConditions.TriggerExit)]
	[Tooltip("What incoming collider's tag should trigger event on?")]
	string tagToCompare;

	[Header("What should the event do once triggered?")]
	[SerializeField]
	UnityEvent Action;

	[Header("What should the event do after being triggered?")]
	[SerializeField]
	[Tooltip("How should the effect be triggered?")]
	Effects postTriggerEffect = Effects.NoEffect;
	[SerializeField]
	[ConditionalField("postTriggerEffect", false, Effects.OnTimer)]
	[Tooltip("How long should it wait aftet triggering event before this effect happens?")]
	float effectDelay;
	[SerializeField]
	[ConditionalField("postTriggerEffect", true, Effects.NoEffect)]
	[Tooltip("Should the action be reset when effect fires?")]
	bool resetActionAfterFire;
	[Space]
	[SerializeField]
	UnityEvent PostActionEffect;
	#endregion

	#region Private Vars
	[Header("Debugging")]
	[SerializeField]
	bool doDebug;
	[SerializeField]
	[ConditionalField("doDebug", false, true)]
	bool hasFiredAction;
	[SerializeField]
	[ConditionalField("doDebug", false, true)]
	bool hasFiredEffect;
	[SerializeField]
	[ConditionalField("doDebug", false, true)]
	float effectDelayTimer;
	#endregion

	void Awake()
	{
		effectDelayTimer = effectDelay;
	}

	void Update()
	{
		if (hasFiredAction)
		{
			if (PostActionEffect != null && !hasFiredEffect)
			{
				if (effectDelayTimer > 0)
					effectDelayTimer -= Time.deltaTime;
				else if (!hasFiredEffect)
					DoConnectedEffect();
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (collisionConds.Contains(eventTrigger) && !string.IsNullOrEmpty(tagToCompare))
		{
			if (other.collider.CompareTag(tagToCompare))
				DoConnectedAction();
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (triggerConds.Contains(eventTrigger) && !string.IsNullOrEmpty(tagToCompare))
		{
			if (other.CompareTag(tagToCompare))
				DoConnectedAction();
		}
	}

	public void DoConnectedAction()
	{
		if (!hasFiredAction)
		{
			Action?.Invoke();
			hasFiredAction = true;
		}
	}

	public void DoConnectedEffect()
	{
		if (!hasFiredEffect)
		{
			PostActionEffect?.Invoke();
			hasFiredEffect = true;
		}

		if (resetActionAfterFire)
		{
			effectDelayTimer = effectDelay;
			hasFiredAction = false;
			hasFiredEffect = false;
		}
	}

	public void OutputMessage(string message)
	{
		Debug.Log(message);
	}
}