using System.Collections.Generic;
using UnityEngine;
using Imports;

[CreateAssetMenu(fileName = "New StatusData", menuName = "ScriptableObjects/Stats/StatusData")]
public class StatusData : ScriptableObject
{
	public enum StatusTypes
	{
		Burn,
		Freeze,
	}

	[Header("Data")]
	[SerializeField] StatusTypes _type;
	[SerializeField] bool _randomDuration;
	[SerializeField] [ConditionalField("_randomDuration", false, true)] float _minDuration;
	[SerializeField] [ConditionalField("_randomDuration", false, true)] float _maxDuration;
	[SerializeField] [ConditionalField("_randomDuration", false, false)] float _duration;

	public StatusTypes Type
	{
		get { return _type; }
	}

	public float Duration
	{
		get
		{
			if (_randomDuration) return Random.Range(_minDuration, _maxDuration);
			return _duration;
		}
	}
}