using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confusion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destruir", 0.5f);
        FindObjectOfType<Jugador>().Confundir();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void Destruir()
    {
        Destroy(this.gameObject);
    }
}
