using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * CheckPoint objects are the actual triggers you intersect with. Send call for Game Manager to set 
 * standard/dynamic checkpoint positions via CheckPointManager.
 * 
 * @author DerpPrincess (Allison Mackenzie Johnson)
 * @version Created February 2, 2021
 */
public class CheckPoint : MonoBehaviour
{
    [SerializeField] private static GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            gameManager.GetCheckPointManager().SetStandardCheckPoint(this.gameObject);
        }
    }
}
