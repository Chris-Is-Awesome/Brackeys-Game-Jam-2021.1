using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Controls standard and dynamic check points.
 * 
 * @author DerpPrincess (Allison Mackenzie Johnson)
 * @version Created February 3, 2021
 */
public class CheckPointManager : Singleton<CheckPointManager>
{
    [SerializeField] private static GameManager gameManager;

    [SerializeField] private GameObject[] checkPoints;
    [SerializeField] private Vector2 standardCheckPointPosition;
    [SerializeField] private Vector2 dynamicCheckPointPosition;

    private void Awake()
    {    
        gameManager = this.gameObject.GetComponent<GameManager>();
        checkPoints = GameObject.FindGameObjectsWithTag("CheckPoint");
    }

    public void SetStandardCheckPoint(GameObject checkPoint)
    {
        standardCheckPointPosition = new Vector2(checkPoint.transform.position.x, checkPoint.transform.position.y);
        Debug.Log("CheckPoint set at position X/Y: (" + checkPoint.transform.position.x + ", " + checkPoint.transform.position.y + ")");
    }

    public void SetDynamicCheckPoint()
    {
        //There was an attempt here. It's gone now.
        //https://i.redd.it/l4sxbl1fpqa01.jpg
    }

    public Vector2 GetStandardCheckPointPositon()
    {
        return standardCheckPointPosition;
    }

    public Vector2 GetDynamicCheckPointPosition()
    {
        return dynamicCheckPointPosition;
    }
}