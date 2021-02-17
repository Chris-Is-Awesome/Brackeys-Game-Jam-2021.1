/*
 * Author(s):
	* Chris is Awesome
 */

using UnityEngine;

[CreateAssetMenu(fileName = "New SlimeData", menuName = "ScriptableObjects/Slimes/SlimeData")]
public class SlimeData : ScriptableObject
{
	public enum SlimeTypes
	{
		Core,
		Bomb,
		Freeze,
		Sticky,
	}

	[SerializeField] string _name;
	[SerializeField] [TextArea] string _description;
	[SerializeField] SlimeTypes _type;
	// TODO: Add EffectData fields for things like jump animation, death animation, etc. ?

	public string Name
	{
		get { return _name; }
	}

	public string  Description
	{
		get { return _description; }
	}

	public SlimeTypes Type
	{
		get { return _type; }
	}
}