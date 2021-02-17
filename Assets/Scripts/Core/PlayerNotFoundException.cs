using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Exception for if we didn't put a player at all in a scene, this can be updated so if GameState is something like the main menu where
 * we don't need a player, it checks for gamestate first. The reason this is good to have an exception is to prevent laziness from us and 
 * if the player somehow gets deleted.
 * 
 * @author DerpPrincess (Allison Mackenzie Johnson)
 * @version Created February 5, 2021
 */
public class PlayerNotFoundException : System.Exception
{

    public PlayerNotFoundException()
    {
        Debug.LogError("Player not found");
        Debug.LogException(this);
    }
}
