using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Senal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector2(FindObjectOfType<Jugador>().transform.position.x - 8, this.transform.position.y);
    }

    public void Destruir()
    {
        Destroy(this.gameObject);
    }
}
