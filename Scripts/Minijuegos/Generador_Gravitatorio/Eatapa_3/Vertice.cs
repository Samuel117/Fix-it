using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertice : MonoBehaviour
{
    private bool sobreMi = false;
    private int contador = 1;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sobreMi && Input.GetMouseButtonDown(0) && FindObjectOfType<Puntero>().Origen)
        {
            FindObjectOfType<Puntero>().Origen = false;
            FindObjectOfType<Puntero>().Destino = true;

            FindObjectOfType<Hexagono>().Origen(this.transform.position);
            
            contador++;
        }
        else if (sobreMi && Input.GetMouseButtonDown(0) && FindObjectOfType<Puntero>().Destino)
        {
           
            if(FindObjectOfType<Hexagono>().origen != (Vector2)this.transform.position)
            {
                //FindObjectOfType<Puntero>().Origen = true;
                //FindObjectOfType<Puntero>().Destino = false;
                 FindObjectOfType<Hexagono>().Destino(this.transform.position);

                FindObjectOfType<Hexagono>().GenerarBarra();

                //Establecer el ultimo punto presionado como origen
                FindObjectOfType<Puntero>().Origen = false;
                FindObjectOfType<Puntero>().Destino = true;
                FindObjectOfType<Hexagono>().Origen(this.transform.position);

                contador++;
            }
        }

        if(contador == 2)
        {
            this.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        sobreMi = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sobreMi = false;
    }
}
