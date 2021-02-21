using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLoadRoom()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<LoadingManager>())
            {
                LoadingManager loadThis = child.gameObject.GetComponent<LoadingManager>();
                loadThis.OnLoadRoom();
            }
        }
    }
}
