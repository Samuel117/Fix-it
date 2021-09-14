using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agua : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<Jugador>().MinijuegoAbierto)
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponent<Jugador>()!= null)
        {
            Destroy(collision.gameObject);
        }
    }
}
