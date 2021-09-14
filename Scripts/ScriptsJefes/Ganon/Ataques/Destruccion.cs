using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruccion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Jugador>()!= null)
        {
            if (Input.GetAxisRaw("Vertical") == -1)
            {
                Debug.Log("REPARADO!!");
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Jugador>() != null)
        {
            if (Input.GetAxisRaw("Vertical") == -1)
            {
                Debug.Log("REPARADO!!");
                Destroy(this.gameObject);
            }
        }
    }
}
