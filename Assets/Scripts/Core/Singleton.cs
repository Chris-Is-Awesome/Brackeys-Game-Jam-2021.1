using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	static bool shuttingDown = false;
	static T _instance;

	public static T Instance
	{
		get
		{
			if (shuttingDown)
			{
				// Warn if object is getting disabled
				Debug.LogWarning("Singleton instance " + typeof(T) + " has already been destroyed. Returning null.");
				return null;
			}

			if (_instance == null)
			{
				// Look for existing instance
				_instance = (T)FindObjectOfType(typeof(T));

				if (_instance == null)
				{
					// Create new instance if one doesn't exist
					GameObject obj = new GameObject();
					_instance = obj.AddComponent<T>();
					obj.name = typeof(T).ToString() + " (Singleton)";

					// Make object persistant
					DontDestroyOnLoad(obj);
				}
			}

			return _instance;
		}
	}

	void OnApplicationQuit()
	{
		shuttingDown = true;
	}

	void OnDestroy()
	{
		shuttingDown = true;
	}
}