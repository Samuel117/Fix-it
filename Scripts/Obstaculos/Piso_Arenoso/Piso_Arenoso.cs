using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piso_Arenoso : MonoBehaviour
{
    [SerializeField] GameObject efecto;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Jugador>() != null)
        {
            Instantiate(efecto, new Vector2(collision.transform.position.x, this.transform.position.y + 0.3f), this.transform.rotation); 
            collision.gameObject.GetComponent<Jugador>().RecibirDanoRotom(0);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Jugador>() != null &&  collision.gameObject.GetComponent<Rigidbody2D>().velocity.x != 0f)
        {
            if(collision.gameObject.GetComponent<Jugador>().esperarRelentizar < Time.time)
            {
                collision.gameObject.GetComponent<Jugador>().RecibirDanoRotom(0);
            }
            Instantiate(efecto, new Vector2(collision.transform.position.x, this.transform.position.y + 0.3f), this.transform.rotation);
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Jugador>() != null)
        {
            FindObjectOfType<Jugador>().esperarRelentizar = 0;
        }
    }


}
