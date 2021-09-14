using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escalable : MonoBehaviour
{
    private bool permitirSalto = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Jugador>() != null && permitirSalto)
        {
            FindObjectOfType<Jugador>().saltando = false;
            permitirSalto = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Jugador>() != null && permitirSalto)
        {
            FindObjectOfType<Jugador>().saltando = false;
            permitirSalto = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        permitirSalto = true;
    }
}
