using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateScript : MonoBehaviour
{
    [SerializeField] GameObject onSprite;
    [SerializeField] GameObject weightSprite;
    [SerializeField] GameObject offSprite;
    int playerCollisions = 0;
    ConnectionsInputScript myInput;
    [SerializeField] int reqWeight; //the number of slimes that must be on to turn on

    // Start is called before the first frame update
    void Start()
    {
        myInput = GetComponentInParent<ConnectionsInputScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerCollisions++;
        offSprite.SetActive(false);
        if (playerCollisions >= reqWeight)
        {
            onSprite.SetActive(true);
            myInput.Activate();
        }
        else weightSprite.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerCollisions--;
        if (playerCollisions < reqWeight)
        {
            if (playerCollisions>=1)
            {
                onSprite.SetActive(false);
                weightSprite.SetActive(true);
            }
            else
            {
                onSprite.SetActive(false);
                weightSprite.SetActive(false);
                offSprite.SetActive(true);
            }
            myInput.Activate();
        }
        }
    }
}
