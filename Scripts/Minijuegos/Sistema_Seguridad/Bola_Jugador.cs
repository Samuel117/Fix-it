using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bola_Jugador : MonoBehaviour
{

    [SerializeField] public AudioClip vic;
    [SerializeField] AudioSource player;

    bool debeSeguir = false;
    public bool enNucleo = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && debeSeguir)
        {
            transform.position = Input.mousePosition;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Puntero>() != null)
        {
            debeSeguir = true;
        }

        if(collision.gameObject.name == "Nucleo")
        {
            player.PlayOneShot(vic);
            enNucleo = true;
            FindObjectOfType<Sistema_Seguridad>().Victoria();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Puntero>() != null)
        {
            debeSeguir = false;
        }
    }

    public void Reinicio()
    {
        debeSeguir = false;
        enNucleo = false;
        this.GetComponent<RectTransform>().position = new Vector2(300, 950);
    }
}
