using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField]GameObject targetTeleport;
    [SerializeField]RoomManagement targetRoom;
    RoomManagement myRoom;
    
    // Start is called before the first frame update
    void Start()
    {
        myRoom = transform.parent.transform.parent.GetComponent<RoomManagement>();
        if (targetRoom == null) Debug.LogError("A door in " + myRoom.transform.parent.name + " is missing a target!");
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Door Collision!");
            GameObject player = collision.gameObject;
            //Add combination code here
            player.transform.position = new Vector2(targetTeleport.transform.position.x, targetTeleport.transform.position.y);
            targetRoom.RoomEntered();
            myRoom.RoomLeft();
        }
    }
}
