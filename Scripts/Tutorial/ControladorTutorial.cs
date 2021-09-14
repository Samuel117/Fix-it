using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorTutorial : MonoBehaviour
{
    bool textoMovimiento;
    bool textoDano;
    [SerializeField] GameObject puntoAparicion;
    [SerializeField] GameObject transicion;
    //bool InstruccionesMinijuego;
    public int contador = 0;

    [SerializeField] AudioSource player;
    [SerializeField] AudioClip efecto;

    // Start is called before the first frame update
    void Start()
    {
        textoMovimiento = false;
        textoDano = false;
        //InstruccionesMinijuego = false;
    }

    // Update is called once per frame
    void Update()
    {
        DesactivarTextoMovimiento();
        DesactivarTextoSalto();
        DesactivarTextoDefectuoso();
        DesactivarInstruccionesMinijuego();
        DesactivarQueEsMinijuego();
        DesactivarObstaculo();
        DesactivarFin();

        ActivaTextoDano();
        DesactivarTextoDano();

        if(FindObjectOfType<Jugador>().BateriaGetter() <= 10)
        {
            FindObjectOfType<Jugador>().BateriaSetter(FindObjectOfType<Jugador>().bateriaMax);
        }
    }

    private void DesactivarTextoMovimiento()
    {
        if((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) && !textoMovimiento)
        {
            player.PlayOneShot(efecto);
            Time.timeScale = 1;
            this.transform.Find("Canvas").Find("UI_movimiento").gameObject.SetActive(false);
            textoMovimiento = true;
        }
    }

    private void DesactivarTextoSalto()
    {
        if (this.transform.Find("Canvas").Find("UI_Salto").gameObject.activeSelf == true && Input.GetKeyDown(KeyCode.UpArrow))
        {
            player.PlayOneShot(efecto);
            Time.timeScale = 1;
            this.transform.Find("Canvas").Find("UI_Salto").gameObject.SetActive(false);
        }
    }

    private void DesactivarTextoDefectuoso()
    {
        if (this.transform.Find("Canvas").Find("UI_Defectuoso").gameObject.activeSelf == true && (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.LeftAlt)))
        {
            player.PlayOneShot(efecto);
            Time.timeScale = 1;
            this.transform.Find("Canvas").Find("UI_Defectuoso").gameObject.SetActive(false);
        }
    }

    private void ActivaTextoDano()
    {
        if(FindObjectOfType<Jugador>().BateriaGetter() < FindObjectOfType<Jugador>().bateriaMax && !textoDano)
        {
            player.PlayOneShot(efecto);
            Time.timeScale = 0;
            this.transform.Find("Canvas").Find("UI_Dano").gameObject.SetActive(true);
            textoDano = true;
        }
    }

    private void DesactivarTextoDano()
    {
        if (this.transform.Find("Canvas").Find("UI_Dano").gameObject.activeSelf == true && Input.GetKeyDown(KeyCode.Space))
        {
            player.PlayOneShot(efecto);
            Time.timeScale = 1;
            this.transform.Find("Canvas").Find("UI_Dano").gameObject.SetActive(false);
        }
    }

    private void DesactivarInstruccionesMinijuego()
    {
        if (this.transform.Find("Canvas").Find("UI_Instruccion").gameObject.activeSelf == true && Input.GetKeyDown(KeyCode.Space))
        {
            player.PlayOneShot(efecto);
            Time.timeScale = 1;
            this.transform.Find("Canvas").Find("UI_Instruccion").gameObject.SetActive(false);
            //InstruccionesMinijuego = true;
        }
    }

    private void DesactivarQueEsMinijuego()
    {
        if (this.transform.Find("Canvas").Find("UI_Minijuego").gameObject.activeSelf == true && Input.GetKeyDown(KeyCode.Space))
        {
            player.PlayOneShot(efecto);
            Time.timeScale = 1;
            this.transform.Find("Canvas").Find("UI_Minijuego").gameObject.SetActive(false);
            //InstruccionesMinijuego = true;
        }
    }

    private void DesactivarObstaculo()
    {
        if (this.transform.Find("Canvas").Find("UI_Obstaculo").gameObject.activeSelf == true && Input.GetKeyDown(KeyCode.Space))
        {
            player.PlayOneShot(efecto);
            Time.timeScale = 1;
            this.transform.Find("Canvas").Find("UI_Obstaculo").gameObject.SetActive(false);
        }
    }

    private void DesactivarFin()
    {
        if (this.transform.Find("Canvas").Find("UI_Fin").gameObject.activeSelf == true && Input.GetKeyDown(KeyCode.Space))
        {
            player.PlayOneShot(efecto);
            Time.timeScale = 1;
            this.transform.Find("Canvas").Find("UI_Fin").gameObject.SetActive(false);
            transicion.SetActive(true);
            StartCoroutine(CargarEscena(12));
            FindObjectOfType<InstruccionesMapa>().vieneTutorial = true;
            //SceneManager.LoadScene(12);
        }
    }

    IEnumerator CargarEscena(int index)
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(index);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Jugador>() != null)
        {
            collision.transform.position = puntoAparicion.transform.position;
        }
    }
}
