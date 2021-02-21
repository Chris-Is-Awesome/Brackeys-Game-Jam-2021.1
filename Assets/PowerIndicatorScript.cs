using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerIndicatorScript : MonoBehaviour
{

    [SerializeField] GameObject onSprite;
    [SerializeField] GameObject tempSprite;
    [SerializeField] GameObject offSprite;
    ConnectionOutputScript myOutput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (myOutput.permActive)
        {
            onSprite.SetActive(true);
            tempSprite.SetActive(false);
            offSprite.SetActive(false);
        }
        else
        {
            if (myOutput.active)
            {
                onSprite.SetActive(false);
                tempSprite.SetActive(true);
                offSprite.SetActive(false);
            }
            else
            {
                onSprite.SetActive(false);
                tempSprite.SetActive(false);
                offSprite.SetActive(true);
            }
        }
    }
}
