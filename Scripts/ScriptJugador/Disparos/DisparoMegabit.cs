using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoMegabit : Disparo
{
    [SerializeField] Sprite uno;
    [SerializeField] Sprite cero;

    // Start is called before the first frame update
    void Start()
    {
        velocidad = 14f;
        lifeSpan = 0.28f;
        dano = FindObjectOfType<Jugador>().DanoGetter();

        if (FindObjectOfType<Megabit>().formaBola)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = uno;
            FindObjectOfType<Megabit>().formaBola = false;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = cero;
            FindObjectOfType<Megabit>().formaBola = true;
        }
        Debug.Log(dano);

        Invoke("DestruirProyectil", lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        GenerarProyectil();
    }
}
