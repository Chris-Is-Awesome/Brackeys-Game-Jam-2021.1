using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftScript : MonoBehaviour
{
    [SerializeField]Transform origin;
    [SerializeField]Transform target;
    [SerializeField] bool startActive;
    [SerializeField] float speed;
    [SerializeField] bool harmful; //used for freeze lifts
    [SerializeField] float waitTime;
    
    bool active;
    bool ascending=true;
    Animator anim;
    ConnectionOutputScript myOutput;

    // Start is called before the first frame update
    void Start()
    {
        myOutput = GetComponentInParent<ConnectionOutputScript>();
        if (startActive) active = true;
        if (harmful) anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!startActive) active = myOutput.active;
    }

    void FixedUpdate()
    {
        if (active)
        {
            if (ascending)
            {
                transform.Translate(new Vector3(0, speed, 0));
                if (transform.position.y > target.position.y)
                {
                    ascending = false;
                    Wait(waitTime);
                }
            }
            else
            {
                transform.Translate(new Vector3(0, -speed, 0));
                if (transform.position.y < origin.position.y)
                {
                    ascending = true;
                    Wait(waitTime);
                }
            }
        }
    }

    private void OnEnable()
    {
        transform.position = origin.position;
        ascending = true;
    }
    IEnumerator Wait(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
    }
}
