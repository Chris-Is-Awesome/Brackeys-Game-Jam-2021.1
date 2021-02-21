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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Core"))
        {
            GameObject player = collision.gameObject;
            //Add combination code here
            player.transform.position = new Vector2(targetTeleport.transform.position.x, targetTeleport.transform.position.y);
            myRoom.RoomEntered();
            myRoom.RoomLeft();
        }
    }
}
