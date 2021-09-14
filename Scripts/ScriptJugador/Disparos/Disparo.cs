using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    protected float velocidad;
    protected float lifeSpan;
    protected float dano;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestruirProyectil", lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        GenerarProyectil();
    }

   protected void GenerarProyectil()
    {
            this.transform.Translate(Vector2.right * this.velocidad * Time.deltaTime, Space.Self);
    }
    
    
    void DestruirProyectil()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D otro)
    {
           if (otro.gameObject.layer == 16)
             {
                Destroy(this.gameObject);
             }
        
        if (otro.GetComponent<Enemigo>() != null)
        {
            Destroy(this.gameObject);
            otro.GetComponent<Enemigo>().RecibirDano(dano);
        }

        if(otro.GetComponent<SCRUM_Master>() != null)
        {
            if (otro.GetType() == typeof(BoxCollider2D) && otro.GetComponent<SCRUM_Master>().derribado == false)
            {
                otro.GetComponent<SCRUM_Master>().recibirDanoPatas(dano);
            }
            else if (otro.GetType() == typeof(CapsuleCollider2D) && otro.GetComponent<SCRUM_Master>().derribado == true)
            {
                otro.GetComponent<SCRUM_Master>().recibirDanoBateria(dano);
            } 
            
            Destroy(this.gameObject);

        }

        if(otro.GetComponent<GeneradorGOH>() != null)
        {
            FindObjectOfType<Ganon>().RecibirDanoGenerador((int)dano);
            Debug.Log("Generador dañado!");
            Destroy(this.gameObject);
        }

        if (otro.gameObject.name == "GeneradorFF")
        {
            FindObjectOfType<Ganon>().RecibirDanoGeneradorFF((int)dano);
            Debug.Log("GeneradorFF dañado!");
            Destroy(this.gameObject);
        }

        if (otro.gameObject.name == "GeneradorPrincipal")
        {
            FindObjectOfType<Ganon>().RecibirDanoPrincipal(dano);
            Debug.Log("GeneradorPrincipal dañado!");
            Destroy(this.gameObject);
        }
    }
}
