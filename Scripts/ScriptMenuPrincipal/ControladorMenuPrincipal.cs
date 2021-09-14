using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ControladorMenuPrincipal : MonoBehaviour
{
    private int hora;
    [SerializeField] GameObject[] interfaces;
    [SerializeField] GameObject[] Fondos;
    [SerializeField] AudioClip Theme;
    [SerializeField] AudioSource player;
    [SerializeField] GameObject transicion;
    public bool salir = false;

    // Start is called before the first frame update
    void Start()
    {
        player.clip = Theme;
        player.loop = true;
        player.Play();
     
    
        //EnemigoData.GuardarEnemigos();

        // Obtenemos la hora actual.
      
        //Debug.Log(DateTime.Now.ToString("H:mm"));
        hora = DateTime.Now.Hour;

        if (hora >= 0 && hora < 7)
        {
            // NOCHE
            interfaces[0].SetActive(true);
            interfaces[1].SetActive(false);
            interfaces[2].SetActive(false);

            Fondos[0].SetActive(true);
            Fondos[1].SetActive(false);
            Fondos[2].SetActive(false);
        }

        if(hora >= 7 && hora < 17)
        {
            //DIA
            interfaces[0].SetActive(false);
            interfaces[1].SetActive(true);
            interfaces[2].SetActive(false);

            Fondos[0].SetActive(false);
            Fondos[1].SetActive(true);
            Fondos[2].SetActive(false);
        }

        if (hora >= 17 && hora < 20)
        {
            // ATARDECER
            interfaces[0].SetActive(false);
            interfaces[1].SetActive(false);
            interfaces[2].SetActive(true);

            Fondos[0].SetActive(false);
            Fondos[1].SetActive(false);
            Fondos[2].SetActive(true);
        }

        if (hora >= 20 && hora < 24)
        {
            //NOCHE
            interfaces[0].SetActive(true);
            interfaces[1].SetActive(false);
            interfaces[2].SetActive(false);

            Fondos[0].SetActive(true);
            Fondos[1].SetActive(false);
            Fondos[2].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ComenzarTutorial() // Manda al tutorial del juego.
    {
        JugadorData.InicializarArchivos();
        transicion.SetActive(true);
        StartCoroutine(CargarEscena(11));
        //SceneManager.LoadScene(11);
    }

    public void IrMapa() // Manda al mapa de niveles.
    {
        //JugadorData.InicializarArchivos();
        transicion.SetActive(true);
        StartCoroutine(CargarEscena(12));
        // SceneManager.LoadScene(12);
    }

    IEnumerator CargarEscena(int index)
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(index);
    }

    public void IrPrueba()
    {
        SceneManager.LoadScene(16);
    }

    public void SalirDelJuego() // Cierra el juego.
    {
        //EnemigoData.EnemyData[] ene = new EnemigoData.EnemyData[8];
        //ene = EnemigoData.CargarDatosEnemigos();
        //for(int x = 0; x < ene.Length; x++)
        //{
        //    Debug.Log(ene[x].Nombre);
        //    Debug.Log(ene[x].Descripciones[0]);
        //}
        Application.Quit();
    }

   
}
