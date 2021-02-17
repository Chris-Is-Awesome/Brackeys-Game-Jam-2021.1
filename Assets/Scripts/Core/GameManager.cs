using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * GameManager is brain of the game.
 * 
 * @author DerpPrincess (Allison Mackenzie Johnson)
 * @version Created February 2, 2021 
 */
[RequireComponent(typeof(CheckPointManager))]
[RequireComponent(typeof(RoomManager))]
public class GameManager : Singleton<GameManager>
{
    // Main Important Items
    [SerializeField] private static CheckPointManager cpm;
    [SerializeField] private static RoomManager RoomManager;
    [SerializeField] private GameObject[] players;


    private void Awake()
    {
        cpm = this.gameObject.GetComponent<CheckPointManager>();
        RoomManager = this.gameObject.GetComponent<RoomManager>();
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    public CheckPointManager GetCheckPointManager()
    {
        return cpm;
    }

    public RoomManager GetRoomManager()
    {
        return RoomManager;
    }

    public GameObject[] GetPlayer()
    {
        return players;
    }

    public GameObject GetSoloPlayer()
    {
        // If 1 player exists, good.
        if (players.Length == 1)
        {
            return players[0];
        }

        // If you somehow have more than 1 player in solo mode... that's bad.
        if(players.Length >= 1)
        {
            Debug.LogError("There should only be one player in Solo mode, did you use this method by mistake?!");
        }

        // If player doesn't exist... that's very bad. Throw exception to yell at devs to fix it.
        if (players.Length == 0)
        {
            throw new PlayerNotFoundException();
        }

        // If player can't be found
        return null;
    }

    public GameObject GetSpecificPlayer(string character)
    {
        // If player doesn't exist... that's very bad. Throw exception to yell at devs to fix it.
        if (players.Length == 0)
        {
            throw new PlayerNotFoundException();
        }

        // If player does exist
        for (int i = 0; i <= players.Length; i++)
        {
            if(players[i].name.ToLower() == character.ToLower())
            {
                return players[i].gameObject;
            }
        }

        // If player can't be found
        return null;
    }
}