using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoEnemigo : MonoBehaviour
{
    protected float velocidad;
    protected float lifeSpan;
    protected int dano;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    protected void GenerarProyectil()
    {
        this.transform.Translate(Vector2.left * this.velocidad * Time.deltaTime, Space.Self);
    }


   protected void DestruirProyectil()
    {
        Destroy(this.gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.GetComponent<Jugador>() != null)
        {
            otro.GetComponent<Jugador>().RecibirDano(dano);
            Destroy(this.gameObject);
        }

        if(otro.gameObject.layer == 16)
        {
            Destroy(this.gameObject);
        }
    }

    public void danoRealizado(int _dano)
    {
        dano = _dano;
    }
}
