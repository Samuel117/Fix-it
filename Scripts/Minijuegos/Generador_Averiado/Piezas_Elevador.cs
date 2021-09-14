using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piezas_Elevador : MonoBehaviour
{
    //Deben rotar cuando son la siguente en el camino, o la elegimos por flechas?
    //Guiar la bola a la siguente dirección

    [SerializeField] AudioSource player;
    [SerializeField] AudioClip rotar;

    protected float[] rotaciones = new float[4]{0f, 90f, 180f, 270f};
    protected int index = 0;
    public bool reinicio = true;

    // Start is called before the first frame update
    void Start()
    {
        RandomPos();
    }

    // Update is called once per frame
    void Update()
    {
        Rotacion();
    }

    protected void Rotacion()
    {
        //0, 90, 180, 270
        float rotacion = this.transform.rotation.z;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            player.PlayOneShot(rotar);
            index--;

            if(index < 0)
            {
                index = 3;
            }
           
            this.transform.rotation = Quaternion.Euler(0, 0, rotaciones[index]);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            player.PlayOneShot(rotar);
            index++;

            if (index > 3)
            {
                index = 0;
            }

            this.transform.rotation = Quaternion.Euler(0, 0, rotaciones[index]);
        }
    }

    public void RandomPos()
    {
        index = Random.Range(0, 4);
        this.transform.rotation = Quaternion.Euler(0, 0, rotaciones[index]);
    }
}
