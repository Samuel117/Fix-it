using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piso_Inestable : MonoBehaviour
{
    private bool debeRomperse = false;
    private float esperarRomper = 0f;
    [SerializeField] GameObject efectoRomper;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(debeRomperse && Romper(esperarRomper))
        {
            Instantiate(efectoRomper, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Jugador>()!= false && !debeRomperse)
        {
            debeRomperse = true;
            esperarRomper = Time.time + 0.8f;
        }
    }

    private bool Romper(float esperarRomper)
    {
        return Time.time > esperarRomper;
    }
}
