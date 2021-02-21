using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionOutputScript : MonoBehaviour //This goes on outputs, like gates and lifts
{
    [SerializeField] ConnectionsInputScript[] permConnections; //Permanent connections. ALL need to be active to trigger.
    [SerializeField] ConnectionsInputScript tempConnection; //Temporariy connection, requires constant connection. Only one per output.
    public bool permActive;
    public bool active;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConnectionUpdate()
    {
        if (!permActive)
        {
            if (tempConnection != null)
            {
                if (tempConnection.active)
                {
                    active = true;
                }
                else
                {
                    active = false;
                }
            }
            int connectionCount=0;
            for (int i = 0; i < permConnections.Length; i++)
            {
                if (permConnections[i].active) connectionCount++;
            }
            if (connectionCount >= permConnections.Length) permActive = true;
            active = true;
        }
    }
}
