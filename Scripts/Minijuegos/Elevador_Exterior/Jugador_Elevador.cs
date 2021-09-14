using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador_Elevador : MonoBehaviour
{
    public bool arriba, abajo, izquierda, derecha;
    public Vector2 pos;

   
    // Start is called before the first frame update
    void Start()
    {
        abajo = true;
        arriba = izquierda = derecha = false;
        pos = this.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movimiento();
    }

    private void Movimiento()
    {
        if (abajo)
        {
            this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 4f);
            arriba = izquierda = derecha = false;
        }
        if (arriba)
        {
            this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 4f);
            abajo = izquierda = derecha = false;
        }
        if (derecha)
        {
            this.transform.position = new Vector2(this.transform.position.x + 4f, this.transform.position.y);
            arriba = izquierda = abajo = false;
        }
        if (izquierda)
        {
            this.transform.position = new Vector2(this.transform.position.x - 4f, this.transform.position.y);
            arriba = abajo = derecha = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetType() == typeof(CapsuleCollider2D))
        {
            FindObjectOfType<Elevador_Exterior>().Derrota();
        }
    }
}
