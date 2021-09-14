using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sistema_Seguridad : Minijuegos
{

    [SerializeField] private GameObject puntoAparicion;
    // Niveles de aparición 4,6,7,10 Rotom, DVDv1, Wind, Peonv2
    [SerializeField] private GameObject[] enemigos = new GameObject[5];

    private int enemigosInvocados = 0;
    private bool invocar = false;
    private float esperarInvocar = 0f;
    // Start is called before the first frame update
    void Start()
    {
        engranajes = 15;
    }

    // Update is called once per frame
    void Update()
    {
        if (invocar)
        {
            if (DebeInvocar(esperarInvocar))
            {
                int ran = Random.Range(0, 4);
                Instantiate(enemigos[ran], puntoAparicion.transform.position, enemigos[ran].transform.rotation);
                esperarInvocar = Time.time + 1f;
                enemigosInvocados++;
            }    
        }

        if(enemigosInvocados == 5)
        {
            invocar = false;
            enemigosInvocados = 0; 
        }
    }

    protected override void Recompensa()
    {
        //Recuperar 25 puntos de bateria.
        int bateria = FindObjectOfType<Jugador>().BateriaGetter();

        FindObjectOfType<Jugador>().BateriaSetter(bateria + 25);
        CerrarMinijuegoVictoria();
    }

    protected override void Sansion()
    {
        //Invocar 5 enemigos aleatorios.
      


        //Cerrar minijuego.
        CerrarMinijueogDerrota();
    }

    public void Victoria()
    {
        Recompensa();
    }

    public void Derrota()
    {
        invocar = true;
        Sansion();
    }

    private bool DebeInvocar(float esperarInvocar)
    {
        return Time.time > esperarInvocar;
    }
}
