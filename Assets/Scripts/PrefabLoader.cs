using System.Collections.Generic;
using UnityEngine;

public class PrefabLoader : MonoBehaviour
{
	[SerializeField] Transform parentOfPrefabs;
	[SerializeField] List<GameObject> prefabsNotInScene = new List<GameObject>();

	// Loads the specified prefab if it exists in scene or prefab folder
	public GameObject LoadPrefab(string prefab)
	{
		// Search for prefab from scene
		foreach (Transform child in parentOfPrefabs)
		{
			if (child.name == prefab)
			{
				Debug.Log("Found prefab '" + child.name + "' from scene!");
				return child.gameObject;
			}
		}

		// Search for prefab from list
		foreach (GameObject obj in prefabsNotInScene)
		{
			if (obj.name == prefab)
			{
				Debug.Log("Found prefab '" + obj.name + "' from list!");
				return obj;
			}
		}

		// Search for prefab from memory
		foreach (GameObject obj in Resources.LoadAll("Prefabs"))
		{
			if (obj.name == prefab)
			{
				Debug.Log("Found prefab '" + obj.name + "' from memory!");
				return obj;
			}
		}

		Debug.LogError("Did not find prefab '" + prefab + "' in scene, list, or memory");
		return null;
	}
}