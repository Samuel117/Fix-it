using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PisoResbaloso : MonoBehaviour
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
            if (FindObjectOfType<Jugador>().MirandoDerechaGetter())
            {
                if (FindObjectOfType<Jugador>().disparando)
                {
                    this.gameObject.GetComponent<AreaEffector2D>().forceMagnitude = 200;
                }
                else
                {
                    this.gameObject.GetComponent<AreaEffector2D>().forceMagnitude = 600;
                }
            }
            else
            {
                if (FindObjectOfType<Jugador>().disparando)
                {
                    this.gameObject.GetComponent<AreaEffector2D>().forceMagnitude = -200;
                }
                else
                {
                    this.gameObject.GetComponent<AreaEffector2D>().forceMagnitude = -600;
                }
            }

            Instantiate(efecto, new Vector2(collision.transform.position.x, this.transform.position.y + 0.3f), this.transform.rotation);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Jugador>() != null) { 

            Instantiate(efecto, new Vector2(collision.transform.position.x, this.transform.position.y + 0.3f), this.transform.rotation);
        }
    }
}
