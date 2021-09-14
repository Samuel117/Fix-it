using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfusionEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(FindObjectOfType<Jugador>()!= null)
        {
            this.transform.position = new Vector2(FindObjectOfType<Jugador>().transform.position.x, FindObjectOfType<Jugador>().transform.position.y + 1f);
        }
        Destruir();
    }

    private void Destruir()
    {
        if ( FindObjectOfType<Jugador>()!= null && !FindObjectOfType<Jugador>().confundido)
        {
            Destroy(this.gameObject);
        }
    }
}
