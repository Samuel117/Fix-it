using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centro : MonoBehaviour
{
    private int contador = 0;
    private int posicion = -250;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(contador == 6)
        {
            //LLAMAE ETAPA 3
            FindObjectOfType<Generador_Gravitatorio>().LlamarEtapa3();
            contador++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Cristal>().codigo == contador)
        {
            collision.gameObject.GetComponent<Cristal>().posicionado = true;
            collision.gameObject.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + posicion);
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            posicion = posicion + 100;
            contador++;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Cristal>().codigo == contador)
        {
            collision.gameObject.GetComponent<Cristal>().posicionado = true;
            collision.gameObject.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + posicion);
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            posicion = posicion + 100;
            contador++;
        }
    }
}
