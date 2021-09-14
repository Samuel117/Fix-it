using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escombro_Cayendo : MonoBehaviour
{
    [SerializeField] GameObject senal;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 3);
        Instantiate(senal, this.transform.position, senal.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Jugador>() != null && !collision.gameObject.GetComponent<Jugador>().MinijuegoAbierto)
        {
            var magnitude = 6000f;
            var force = transform.position - collision.transform.position;
            force.Normalize();

            if (this.transform.position.x >= collision.gameObject.transform.position.x)
            {
                //collision.gameObject.transform.position = new Vector2(collision.gameObject.transform.position.x - 3, collision.gameObject.transform.position.y);
                
               collision.gameObject.GetComponent<Rigidbody2D>().AddForce( Vector2.left * magnitude);
            }
            else
            {
                //collision.gameObject.transform.position = new Vector2(collision.gameObject.transform.position.x + 3, collision.gameObject.transform.position.y);
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * magnitude);
            }
            
            FindObjectOfType<Jugador>().RecibirDano(5);

            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
