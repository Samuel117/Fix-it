using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoSCRUMMaster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
    }

    private void Movimiento()
    {
        this.GetComponent<Rigidbody2D>().velocity = Vector2.up * 15;
    }
}
