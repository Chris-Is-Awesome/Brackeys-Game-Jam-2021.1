using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    bool active = false;
    BoxCollider2D myCollider;
    SpriteRenderer spriteRen;
    ConnectionOutputScript myOutput;
    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponentInChildren<BoxCollider2D>();
        spriteRen = GetComponentInChildren<SpriteRenderer>();
        myOutput = GetComponent<ConnectionOutputScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active!=myOutput.active)
        {
            active = myOutput.active;
            if (active) Activate(); else Deactivate();
        }
    }

    public void Activate()
    {
        myCollider.enabled = false;
        spriteRen.enabled = false;
    }

    public void Deactivate()
    {
        myCollider.enabled = true;
        spriteRen.enabled = true;
    }
}
