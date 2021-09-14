using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorEscombroTutorial : MonoBehaviour
{
    [SerializeField] private GameObject escombro;

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
            Instantiate(escombro, new Vector2(this.transform.position.x, this.transform.position.y + 10), this.transform.rotation);
        }
    }
}
