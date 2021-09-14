using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaAracnida : MonoBehaviour
{
    [SerializeField] private GameObject red;

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
        if(collision.gameObject.GetComponent<Jugador>() != null)
        {

            FindObjectOfType<Jugador>().AtrapadoPorSCRUM(5);
            Instantiate(red, FindObjectOfType<Jugador>().transform.position, red.transform.rotation);
            Destroy(this.gameObject, 1f);
        }
    }
}
