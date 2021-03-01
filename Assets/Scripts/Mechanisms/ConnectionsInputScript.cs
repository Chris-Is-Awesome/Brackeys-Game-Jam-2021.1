using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionsInputScript : MonoBehaviour
{
    [SerializeField] ConnectionOutputScript[] connections;
    public bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate(bool activate = true)
    {
        active = activate;
        for (int i = 0; i < connections.Length; i++)
        {
            connections[i].ConnectionUpdate();
        }
    }
}
