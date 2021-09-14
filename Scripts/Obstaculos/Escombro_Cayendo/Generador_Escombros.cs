using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generador_Escombros : MonoBehaviour
{
    [SerializeField] private GameObject escombro;
    [SerializeField] private int contador;
    private float esperarEscombro = 5f;
    private Vector2 posicionEscombro;

    // Start is called before the first frame update
    void Start()
    {
        esperarEscombro = Time.time + 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<Jugador>()!= null && FindObjectOfType<Jugador>().MinijuegoAbierto)
        {
            esperarEscombro = Time.time + 3f;
        }
        if(contador > 0 && debeGenerarEscombro(esperarEscombro))
        {
            //Elegir posición aleatoria para esombro.
            
            //posicionEscombro = new Vector2(FindObjectOfType<Jugador>().transform.position.x + Random.Range(-5, 6), FindObjectOfType<Jugador>().transform.position.y + 10f);
            posicionEscombro = new Vector2(FindObjectOfType<Jugador>().transform.position.x, FindObjectOfType<Jugador>().transform.position.y + 10f);

            Instantiate(escombro, posicionEscombro, this.transform.rotation);
            contador--;
            esperarEscombro = Time.time + 15f;
        }
    }

    private bool debeGenerarEscombro(float esperarEscombro)
    {
        return Time.time > esperarEscombro;
    }
}
