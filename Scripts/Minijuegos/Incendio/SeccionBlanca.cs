using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeccionBlanca : MonoBehaviour
{
    private bool FlechaEnBlanco;
    [SerializeField] AudioSource player;
    [SerializeField] AudioClip correcto;
    [SerializeField] AudioClip fallido;

    private int num;
    // Start is called before the first frame update
    void Start()
    {
        num = Random.Range(-350, 350);
        this.transform.position = new Vector2(this.transform.position.x + num, this.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && FlechaEnBlanco)
        {
            FindObjectOfType<Flecha>().velocidadMovimiento += 200;
            FindObjectOfType<Incendio>().puntos++;
            Debug.Log("Bien hecho");

            player.PlayOneShot(correcto);
        }
        else if(Input.GetKeyUp(KeyCode.Space) && !FlechaEnBlanco)
        {
            //Perder una vida.
            FindObjectOfType<Incendio>().IntentosDentro--;
            //Aumntar velocidad.
            FindObjectOfType<Flecha>().velocidadMovimiento += 200;
            //Reducir tamaño barra blanca.
            this.GetComponent<BoxCollider2D>().size = new Vector2(this.GetComponent<BoxCollider2D>().size.x - 20, 100);
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x - 20, 100);
            Debug.Log("Fallaste");

            player.PlayOneShot(fallido);
        }
    }

    public void ReiniciarTamano()
    {
        this.GetComponent<BoxCollider2D>().size = new Vector2(100, 100);
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
      if(collision.gameObject.GetComponent<Flecha>() != null)
        {
            FlechaEnBlanco = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Flecha>() != null)
        {
            FlechaEnBlanco = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Flecha>() != null)
        {
            FlechaEnBlanco = false;
        }
    }
}