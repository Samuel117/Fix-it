using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tormenta : MonoBehaviour
{

    [SerializeField] GameObject DropEffect;
    [SerializeField] GameObject DanoEffect;

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
        if (collision.gameObject.layer == 8)
        {
            Instantiate(DropEffect, new Vector2(this.transform.position.x, this.transform.position.y - 0.5f), DropEffect.transform.rotation);
            Destruir();
        }

        if(collision.gameObject.tag == "Player")
        {
            FindObjectOfType<Jugador>().RecibirDano(20);
            Instantiate(DanoEffect, FindObjectOfType<Jugador>().transform.position, DanoEffect.transform.rotation);
            Destruir();
        }
    }

    private void Destruir()
    {
        Destroy(this.gameObject);
    }
}
