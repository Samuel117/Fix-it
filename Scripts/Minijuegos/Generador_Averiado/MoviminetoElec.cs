using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoviminetoElec : MonoBehaviour
{
    [SerializeField] private GameObject[] puntos = new GameObject[4];
    private float velocidad = 300f;
    public int index = 0;
   [SerializeField] Vector2 posicionElec;
    [SerializeField] int code;
    // Start is called before the first frame update
    void Start()
    {
        //posicionElec = new Vector2(550, 800);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void MoverElect()
    {
        //Hacer waypints que simulen el cable.
        //Seguir camino.

        if(index < puntos.Length)
        {
            this.transform.position = Vector2.MoveTowards(transform.position, puntos[index].transform.position, velocidad * Time.deltaTime);
        }
       
        //AbajoDerecha();
        //this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 1); // abajo
        //this.transform.position = new Vector2(this.transform.position.x + 1, this.transform.position.y - 1); //abajo y derecha
        //this.transform.position = new Vector2(this.transform.position.x -1, this.transform.position.y - 1); //abajo e izquierda
        //this.transform.position = new Vector2(this.transform.position.x - 1, 0); //izquierda
        //this.transform.position = new Vector2(this.transform.position.x + 1, 0); //derecha
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "ParteInferior")
        {
            FindObjectOfType<Generador_Averiado>().cont++;
            //FindObjectOfType<Generador_Averiado>().debeMoverElec = false;
            FindObjectOfType<Generador_Averiado>().player.PlayOneShot(FindObjectOfType<Generador_Averiado>().llegar);

            if (FindObjectOfType<Generador_Averiado>().cont >= 4)
            {
                FindObjectOfType<Generador_Averiado>().CoomprovarResp();
            }
        }

        if (collision.gameObject.name == "punto" + code)
        {
            index++;
            Debug.Log("Index: " + index);
        }
    }

    public void ReiniciarPosicion()
    {
        this.transform.position = posicionElec;
        index = 0;
    }
}
