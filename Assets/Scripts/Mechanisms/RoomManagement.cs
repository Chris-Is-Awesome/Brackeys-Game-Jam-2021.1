using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class RoomManagement : MonoBehaviour
{
    [SerializeField] bool startLoaded = false;
    [SerializeField] Transform minimumBounds;
    [SerializeField] Transform maximumBounds;
    [SerializeField] float lightingIntensity=0.5f;
    CameraTargetScript cameraTarget;
    Light2D globalLight;

    // Start is called before the first frame update
    void Start()
    {
        if (!startLoaded) RoomLeft();
        cameraTarget = (CameraTargetScript)FindObjectOfType(typeof(CameraTargetScript));
        globalLight = GameObject.Find("Main Global Light").GetComponent<Light2D>();
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
        globalLight.intensity = lightingIntensity;
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
