using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nucleo : MonoBehaviour
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
        if(collision.gameObject.GetComponent<Taladro>() != null )
        {
            FindObjectOfType<Taladro>().player.PlayOneShot(FindObjectOfType<Taladro>().victoria);
            FindObjectOfType<Escombro>().victoria = true;
            FindObjectOfType<Escombro>().Victoria();
        }
    }
}
