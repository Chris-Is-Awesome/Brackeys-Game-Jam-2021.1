using System.Collections.Generic;
using UnityEngine;
using Imports;

[CreateAssetMenu(fileName = "New DamageData", menuName = "ScriptableObjects/Stats/DamageData")]
public class DamageData : ScriptableObject
{
	public enum DamageTypes
	{
		Normal,
		Contact,
		Burn,
		Freeze,
	}

	[Header("Data")]
	[SerializeField] DamageTypes _type;
	[SerializeField] float _damage;
	[SerializeField] List<string> _tagsAffected;

	[Header("Status")]
	[SerializeField] bool _canInflictStatus;
	[SerializeField] [ConditionalField("_canInflictStatus", false, true)] StatusData _statusToInflict;
	[Range(0, 100)] [SerializeField] [ConditionalField("_canInflictStatus", false, true)] float _chance;

	public DamageTypes Type
	{
		get { return _type; }
	}

	public float Damage
	{
		get { return _damage; }
	}

	public List<string> TagsAffected
	{
		get { return _tagsAffected; }
	}

	public StatusData Status
	{
		get { return _statusToInflict; }
	}

	public float StatusChance
	{
		get { return _chance; }
	}
}