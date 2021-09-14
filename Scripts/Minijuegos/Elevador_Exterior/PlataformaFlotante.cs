using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaFlotante : MonoBehaviour
{
    [SerializeField] private int alturaMax;
    private Vector2 posicionMin;
    private Vector2 posicionMax;
    private bool direccion = true;
    private float esperar = 0f;

    // Start is called before the first frame update
    void Start()
    {
        posicionMin = this.transform.position;
        posicionMax = new Vector2(this.transform.position.x, this.transform.position.y + alturaMax);
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();   
    }

    private void Movimiento()
    {
       if(this.transform.position.y >= posicionMax.y && direccion)
        {
            direccion = false;
        }
        else if(this.transform.position.y <= posicionMin.y && !direccion)
        {
            direccion = true;
        }

            if (DebeMovese(esperar))
            {
                if (direccion)
                {
                     this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, 5);
                }
                else
                {
                    this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, -5);
                }

                esperar = Time.time + 0.5f;
            }
        
    }

    private bool DebeMovese(float esperar)
    {
        return Time.time > esperar;
    }
}
