using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relampago : MonoBehaviour
{
    // Start is called before the first frame update+
    [SerializeField] AudioSource player;
    [SerializeField] AudioClip sonido;
    void Start()
    {
        player.PlayOneShot(sonido);
        Invoke("Desactivar", 0.8f);
        Destroy(this.gameObject, 6f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Jugador>() != null && !collision.gameObject.GetComponent<Jugador>().MinijuegoAbierto)
        {
            FindObjectOfType<Jugador>().RecibirDano(15);
            var magnitude = 6000f;

            if (this.transform.position.x >= FindObjectOfType<Jugador>().transform.position.x)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * magnitude);
            }
            else
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * magnitude);
            }
           
        }
    }

    private void Desactivar()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
