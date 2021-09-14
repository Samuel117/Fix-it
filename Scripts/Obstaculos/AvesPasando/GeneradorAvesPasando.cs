using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorAvesPasando : MonoBehaviour
{
    [SerializeField] private int contador;
    [SerializeField] private GameObject aves;
    private float esperarAves = 10f;
    private Vector2 posicion;


    // Start is called before the first frame update
    void Start()
    {
        esperarAves = Time.time + 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<Jugador>()!= null && FindObjectOfType<Jugador>().MinijuegoAbierto)
        {
            esperarAves = Time.time + 5f;
        }
        if (debeGenerar(esperarAves) && contador > 0)
        {
            posicion = new Vector2(FindObjectOfType<Jugador>().transform.position.x - 20f, FindObjectOfType<Jugador>().transform.position.y + Random.Range(0, 1));
            Instantiate(aves, posicion, this.transform.rotation);
            esperarAves = Time.time + 10f;
            contador--;
        }
    }

    private bool debeGenerar(float esperarAves)
    {
        return Time.time > esperarAves;
    }
}
