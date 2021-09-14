using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Taladro>() != null )
        {
            collision.gameObject.GetComponent<Taladro>().RecibirDano();

            var magnitude = 0.1f;

            var force = transform.position - collision.transform.position;

            force.Normalize();
            FindObjectOfType<Taladro>().gameObject.GetComponent<Rigidbody2D>().AddForce(-force * magnitude);

            //if(collision.gameObject.transform.position.x > this.transform.position.x)
            //{
            //    collision.transform.position = new Vector2(collision.transform.position.x + 50, collision.transform.position.y);
            //}
            //else
            //{
            //    collision.transform.position = new Vector2(collision.transform.position.x - 50, collision.transform.position.y);
            //}

           FindObjectOfType<Taladro>().esperar = Time.time + 0.4f;
           FindObjectOfType<Taladro>().cancelarMov = true;
        }
    }

    
}
