using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Barra : MonoBehaviour
{
    private bool Atino = false;
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
        if (Input.inputString.ToUpper() == collision.gameObject.GetComponent<TextMeshProUGUI>().text)
        {
            Debug.Log("Le atinaste");
            Atino = true;
            Destroy(collision.gameObject);
            FindObjectOfType<Generador_Gravitatorio>().LetrasAtinadas++;
            Debug.Log(FindObjectOfType<Generador_Gravitatorio>().LetrasAtinadas);
        }
        else
        {
            Atino = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!Atino)
        {
            FindObjectOfType<Generador_Gravitatorio>().IntentosDentro--;
        }
        Atino = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.inputString.ToUpper() == collision.gameObject.GetComponent<TextMeshProUGUI>().text)
        {
            Debug.Log("Le atinaste");
            Atino = true;
            Destroy(collision.gameObject);
            FindObjectOfType<Generador_Gravitatorio>().LetrasAtinadas++;
            Debug.Log(FindObjectOfType<Generador_Gravitatorio>().LetrasAtinadas);
        }
        else
        {
            Atino = false;
        }
    }

}
