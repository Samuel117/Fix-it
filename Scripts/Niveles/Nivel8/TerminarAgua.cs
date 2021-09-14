using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminarAgua : MonoBehaviour
{

    [SerializeField] GameObject Agua;
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
        if(collision.GetComponent<Jugador>() != false)
        {
            Agua.gameObject.SetActive(false);
        }
    }

}
