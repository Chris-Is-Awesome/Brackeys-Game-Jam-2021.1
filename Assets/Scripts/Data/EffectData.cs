/*
 * Author(s):
	* Chris is Awesome
 */

using UnityEngine;

[CreateAssetMenu(fileName = "New SlimeData", menuName = "ScriptableObjects/Effects/EffectData")] 
public class EffectData : ScriptableObject
{
	[SerializeField] [TextArea] string description;
	[SerializeField] Animation _animation;
	[SerializeField] ParticleSystem _particlesToSpawn;

	public Animation Animation
	{
		get { return _animation; }
	}

	public ParticleSystem ParticlesToSpawn
	{
		get { return _particlesToSpawn; }
	}
}