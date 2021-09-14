using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaFinalCont : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Jugador>() != null)
        {
            //Simepre es true.
            FindObjectOfType<ControladorNvl>().nivelCompletadoSetter();
            Debug.Log("Nivel superado!");
        }
    }
}
