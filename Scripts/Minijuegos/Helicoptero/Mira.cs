using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mira : MonoBehaviour
{
    private bool sobreMosca = false;
    private GameObject mosca;

    [SerializeField] AudioSource player;
    [SerializeField] AudioClip laser;
    [SerializeField] AudioClip errorLaser;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition - canvas.transform.localPosition);
        ////transform.position = (pos);
        ////this.gameObject.GetComponent<RectTransform>().position = (pos);
        //this.gameObject.transform.localPosition = pos;

        
        transform.position = Input.mousePosition;

        if (Input.GetMouseButtonUp(0) && sobreMosca)
        {
            player.PlayOneShot(laser);
            Destroy(mosca.gameObject);
            sobreMosca = false;
            FindObjectOfType<Helicoptero>().puntos--;
            FindObjectOfType<Helicoptero>().actualizarContador();

        }else if (Input.GetMouseButtonUp(0) && !sobreMosca)
        {
            player.PlayOneShot(errorLaser);
            FindObjectOfType<Helicoptero>().IntentosDentro--;
            FindObjectOfType<Helicoptero>().actualizarIntentos();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Mosca>())
        {
            sobreMosca = true;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Mosca>())
        {
            sobreMosca = true;
            mosca = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Mosca>())
        {
            sobreMosca = false;
            mosca = null;
        }
    }
}
