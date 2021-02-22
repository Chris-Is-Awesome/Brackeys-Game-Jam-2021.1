using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManagement : MonoBehaviour
{
    [SerializeField] bool startLoaded = false;
    [SerializeField] Transform minimumBounds;
    [SerializeField] Transform maximumBounds;
    CameraTargetScript cameraTarget;

    // Start is called before the first frame update
    void Start()
    {
        if (!startLoaded) RoomLeft();
        cameraTarget = (CameraTargetScript)FindObjectOfType(typeof(CameraTargetScript));
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
        cameraTarget.minBounds = minimumBounds;
        cameraTarget.maxBounds = maximumBounds;
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
