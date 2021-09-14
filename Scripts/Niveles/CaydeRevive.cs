using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaydeRevive : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<Jugador>() != null && FindObjectOfType<Cayde6>() == null)
        {
            this.gameObject.SetActive(false);
        }
        else if (FindObjectOfType<Jugador>() != null && FindObjectOfType<Cayde6>() != null)
        {
            if (!FindObjectOfType<Cayde6>().fenix)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
