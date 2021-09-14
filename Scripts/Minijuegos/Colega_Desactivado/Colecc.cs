using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colecc : MonoBehaviour
{
    [SerializeField] private int Coleccionable;
    private bool[] coleccion = new bool[3];
    private bool recogido = false;
    [SerializeField] AudioSource player;
    [SerializeField] AudioClip obtener;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !recogido)
        {
            recogido = true;
            player.PlayOneShot(obtener);
            coleccion = Coleccionables.CargarColeccion();
            coleccion[Coleccionable] = true;
            Coleccionables.GuardarColeccion(coleccion);
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(0,0,0,0);
            Invoke("Destruir", 1f);
        }
    }

    private void Destruir()
    {
        Destroy(this.gameObject);
    }
}
