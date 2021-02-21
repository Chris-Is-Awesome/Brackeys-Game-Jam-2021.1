using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManagement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RoomEntered() //enable everything in the room
    {
        foreach (Transform child in transform)
        {
            if (!child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    public void RoomLeft() //disable everything in the room
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

}
