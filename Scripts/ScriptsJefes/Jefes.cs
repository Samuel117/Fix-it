using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jefes : MonoBehaviour
{

    public float bateria;
    protected float cadenciaDeDisparoBasico;
    protected float alcance;
    protected bool disparando = false;
    protected float esperarDisparo = 5f;

    //ENGRANAJES.
    protected int engranajesDerrota;

    [SerializeField] protected GameObject prebafAtaqueBasico;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    protected virtual void AtaqueBasico()
    {
        //Verifica que sea posible disparar en el momento.
        if (PuedeDisparar(esperarDisparo))
        {
                Instantiate(this.prebafAtaqueBasico, this.transform.position, this.transform.rotation);

                this.disparando = true;

                // this.GetComponent<Animator>().SetTrigger("Disparar");
                this.esperarDisparo = Time.time + this.cadenciaDeDisparoBasico;
        }
    }

    protected virtual void AtaqueBasicoSM()
    {
        //Verifica que sea posible disparar en el momento.
        if (PuedeDisparar(esperarDisparo))
        {
            Instantiate(this.prebafAtaqueBasico, new Vector2(this.transform.position.x + 2f, this.transform.position.y -2f), this.transform.rotation);

            this.disparando = true;

            this.transform.Find("SMSprites").gameObject.GetComponent<Animator>().SetTrigger("Disparar");
            this.esperarDisparo = Time.time + this.cadenciaDeDisparoBasico;
        }
    }

    protected virtual void HabilidadEspecial()
    {

    }

    protected void RecibirDano()
    {

    }

    protected bool PuedeDisparar(float tiempoEsperaDisparo)
    {
        //Establece si el jugador puede disparar o usar su habilidad especial. 
        return Time.time > tiempoEsperaDisparo;
    }

}
