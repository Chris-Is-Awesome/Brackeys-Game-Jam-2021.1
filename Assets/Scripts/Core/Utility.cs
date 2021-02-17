using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Utility
{
	#region Scene Stuff

	// Loads the scene via name
	public static void LoadScene(string name, bool async = false, bool additive = false)
	{
		LoadSceneMode loadMode = additive ? LoadSceneMode.Additive : LoadSceneMode.Single;

		if (async)
		{
			SceneManager.LoadSceneAsync(name, loadMode);
			return;
		}

		SceneManager.LoadScene(name, loadMode);
	}

	// Loads the scene via index
	public static void LoadScene(int index, bool async = false, bool additive = false)
	{
		LoadSceneMode loadMode = additive ? LoadSceneMode.Additive : LoadSceneMode.Single;

		if (async)
		{
			SceneManager.LoadSceneAsync(index, loadMode);
			return;
		}

		SceneManager.LoadScene(index, loadMode);
	}

	// Returns the name of the currently loaded scene
	public static string GetCurrentSceneName()
	{
		return SceneManager.GetActiveScene().name;
	}

	#endregion

	#region Object stuff

	// Returns the specified child from the specified parent and/or indirect parent (recursive search)
	private static List<Transform> children = new List<Transform>();
	public static Transform FindNestedChild(string indirectParentName, string childName, string directParentName = "")
	{
		GameObject indirectParentObj = GameObject.Find(indirectParentName);
		string output = string.Empty;

		// If indirect parent found
		if (indirectParentObj != null)
		{
			// Get all children
			FindNestedChildRecursive(indirectParentObj.transform);

			// If children were found
			if (children.Count > 0)
			{
				// Check each child
				for (int i = 0; i < children.Count; i++)
				{
					Transform child = children[i];

					// Check child name
					if (child.name == childName)
					{
						// Check for direct parent
						if (!string.IsNullOrEmpty(directParentName))
						{
							// If direct parent is what is wanted
							if ((child.parent.name == directParentName || child.parent.name.ToLower() == directParentName.ToLower())) { return child; }
							// If direct parent is NOT what is wanted
							else { output = "No direct parent with name '" + directParentName + "' was found for child '" + childName + "' with indirect parent of '" + indirectParentName + "'. Returning null."; }
						}
						else { return child; }
					}
					// If no child matching the wanted name is found
					else { output = "No child with name '" + childName + "' was found under indirect parent '" + indirectParentName + "'. Returning null."; }
				}
			}
			// If no children were found
			else { output = "'" + indirectParentName + "' has no children. Returning null."; }
		}
		// If indirect parent NOT found
		else { output = "Indirect parent '" + indirectParentName + "' was not found. Returning null."; }

		Debug.LogWarning(output);
		return null;
	}

	// Recursively cycles through all Transforms of parent adding each child to the list
	private static void FindNestedChildRecursive(Transform parent)
	{
		// Add existing children to list
		foreach (Transform child in parent)
		{
			if (child == null) continue;
			children.Add(child);
			FindNestedChildRecursive(child);
		}
	}

	// Returns component of type T on specified object
	public static T GetObjComp<T>(string objName) where T : Component
	{
		GameObject go = GameObject.Find(objName);

		if (go != null)
		{
			// Check if component is on the GameObject
			T foundComp = go.GetComponent<T>();
			if (foundComp != null) { return foundComp; }

			// Check if component is on any of its  children
			foreach (Transform trans in go.transform)
			{
				foundComp = trans.GetComponent<T>();
				if (foundComp != null) { return foundComp; }
			}
		}
		else
		{
			// If GameObject not found
			Debug.LogWarning("GameObject with name '" + objName + "' was not found. Returning null.");
			return null;
		}

		// If component not found
		Debug.LogWarning("Component of type '" + typeof(T).ToString() + "' was not found on GameObject named '" + objName + "'. Returning null.");
		return null;
	}

	// Return closest object to another object from a list of objects
	public static GameObject FindClosestObject(GameObject closestToThis, List<GameObject> objectsToCompare)
	{
		GameObject closestObj = null;
		Vector3 pos2 = closestToThis.transform.position;
		float closestDistance = Mathf.Infinity;

		// For each object in list of gos, compare distances
		foreach (GameObject go in objectsToCompare)
		{
			// Get distance
			Vector3 pos1 = go.transform.position;
			float distance = Vector3.Distance(pos1, pos2);

			// If closestToThis is included in list of gos, skip it since obviously it'll be closest since it's on top of itself!
			if (distance <= 0) { continue; }
			else if (distance < closestDistance)
			{
				closestObj = go;
				closestDistance = distance;
			}
		}

		return closestObj;
	}

	// Returns closest object with type T to specified object
	public static GameObject FindClosestObjectWithType<T>(GameObject closestToThis) where T : Component
	{
		GameObject closestObj = null;
		Vector3 pos2 = closestToThis.transform.position;
		float closestDistance = Mathf.Infinity;

		// For each object in list of gos, compare distances
		foreach (T go in GameObject.FindObjectsOfType<T>())
		{
			// Get distance
			Vector3 pos1 = go.transform.position;
			float distance = Vector3.Distance(pos1, pos2);

			// If closestToThis is included in list of gos, skip it since obviously it'll be closest since it's on top of itself!
			if (distance <= 0) { continue; }
			else if (distance < closestDistance)
			{
				closestObj = go.gameObject;
				closestDistance = distance;
			}
		}

		return closestObj;
	}

	#endregion
}