using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 
 * @author DerpPrincess (Allison Mackenzie Johnson)
 * @version Created February 5, 2021
 */
public class RoomManager : Singleton<RoomManager>
{
    [SerializeField] private static GameManager gameManager;

    private void Awake()
    {
        gameManager = this.gameObject.GetComponent<GameManager>();
    }
    public void ReloadStandardCheckPointSolo()
    {
        Vector2 standardCheckPointPosition = gameManager.GetCheckPointManager().GetStandardCheckPointPositon();
        gameManager.GetSoloPlayer().transform.position = standardCheckPointPosition;

        //reset enemies here??
    }
}
