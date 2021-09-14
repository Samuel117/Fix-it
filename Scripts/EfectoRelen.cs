using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoRelen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(FindObjectOfType<Jugador>() != null)
        {
            this.transform.position = FindObjectOfType<Jugador>().transform.position;
        }
        Destruir(); 
    }

    public void Destruir()
    {
        if(FindObjectOfType<Jugador>() != null && FindObjectOfType<Jugador>().velocidad == FindObjectOfType<Jugador>().velocidadEstandar)
        {
            Destroy(this.gameObject);
        }
    }
}
