using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvesPasando : MonoBehaviour
{
    [SerializeField] private int dano;
    [SerializeField] private GameObject senal;

    private bool Tocar;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 10);
        Instantiate(senal, this.transform.position, senal.transform.rotation);
        Tocar = false;
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();   
    }

    private void Movimiento()
    {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(15, this.GetComponent<Rigidbody2D>().velocity.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
      if(collision.gameObject.GetComponent<Jugador>() != null && !Tocar && !collision.gameObject.GetComponent<Jugador>().MinijuegoAbierto)
        {
            Tocar = true;
            var magnitude = 6000f;
            FindObjectOfType<Jugador>().RecibirDano(dano);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * magnitude);
        }   
    }
}
