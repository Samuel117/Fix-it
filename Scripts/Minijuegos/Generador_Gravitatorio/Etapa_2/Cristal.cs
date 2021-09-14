using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cristal : MonoBehaviour
{
    bool debeSeguir = false;
    public bool posicionado = false;
    [SerializeField] public int codigo;
    private Vector2 posicionDefecto;

    // Start is called before the first frame update
    void Start()
    {
        posicionDefecto = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && debeSeguir)
        {
            transform.position = Input.mousePosition;
        }
        else if(!posicionado)
        {
            this.transform.position = posicionDefecto;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Puntero>() != null)
        {
            debeSeguir = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Puntero>() != null)
        {
            debeSeguir = false;
        }
    }
}
