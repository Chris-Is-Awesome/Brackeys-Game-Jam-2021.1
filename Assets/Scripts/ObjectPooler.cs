using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	static ObjectPooler _instance;
	public static ObjectPooler Instance
	{
		get
		{
			if (_instance == null)
				Debug.LogError("ObjectPooler is null.");

			return _instance;
		}
	}

	[SerializeField] GameObject objToPool;
	[SerializeField] Transform objPoolContainer;
	[SerializeField] int amountToPool;
	public List<GameObject> objPool;

	// Initialize instance
	void Awake()
	{
		_instance = this;
	}

	// Initial generation of pool
	void Start()
	{
		objPool = GeneratePool(amountToPool);
	}

	// Instantiates x objects and adds them to the pool
	List<GameObject> GeneratePool(int amount = 1)
	{
		for (int i = 0; i < amount; i++)
		{
			GameObject go = Instantiate(objToPool, objPoolContainer);
			go.SetActive(false);
			objPool.Add(go);
		}

		return objPool;
	}

	// Fetches already existing object or creates a new one if entire pool is in use
	public GameObject RequestObj()
	{
		for (int i = 0; i < objPool.Count; i++)
		{
			if (!objPool[i].activeInHierarchy)
				return objPool[i];
		}

		return GeneratePool()[objPool.Count];
	}
}