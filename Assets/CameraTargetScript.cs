using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Imports;

public class CameraTargetScript : MonoBehaviour
{
    public Transform myTarget;
    public Transform maxBounds; // bottom right boundary, maximum x, minimum y
    public Transform minBounds; //top left boundary, minimum x, maximum y

    [ReadOnly] [SerializeField] Vector3 minPos;
    [ReadOnly] [SerializeField] Vector3 maxPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //minPos = minBounds.TransformPoint(0, 0, 0);
        //maxPos = maxBounds.TransformPoint(0, 0, 0);

        transform.position = myTarget.position;
        if (transform.position.x < minBounds.position.x) transform.position = new Vector2(minBounds.position.x, transform.position.y);
        if (transform.position.x > maxBounds.position.x) transform.position = new Vector2(maxBounds.position.x, transform.position.y);
        if (transform.position.y > minBounds.position.y) transform.position = new Vector2(transform.position.x, minBounds.position.y);
        if (transform.position.y < maxBounds.position.y) transform.position = new Vector2(transform.position.x, maxBounds.position.y);
    }
}
