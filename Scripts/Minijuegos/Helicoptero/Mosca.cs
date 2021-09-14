using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mosca : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movimiento();
    }

   
    
    private void Movimiento()
    {
        //Derecha a Izquierda
        this.transform.position = new Vector2(this.transform.position.x + 10f, this.transform.position.y);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destruir mosca fuera de pantalla

        if(collision.gameObject.name == "ZonaDestruirMoscas")
        {
            Destroy(this.gameObject);
        }
    }
}