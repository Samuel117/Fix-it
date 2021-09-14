using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorTormentaArena : MonoBehaviour
{
    private float esperarTormenta = 60f;  //60
    [SerializeField] private GameObject tormentaArena;

    // Start is called before the first frame update
    void Start()
    {
        esperarTormenta = Time.time + 30f;
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<Jugador>().MinijuegoAbierto)
        {
            esperarTormenta = Time.time + 20f; //60f
        }
        if (DebeGenerarTormenta(esperarTormenta))
        {
            Instantiate(tormentaArena, this.transform.position, this.transform.rotation);
            esperarTormenta = Time.time + 60f; //60
        }
    }

    private bool DebeGenerarTormenta(float esperarTormenta)
    {
        return Time.time > esperarTormenta;
    }
}
