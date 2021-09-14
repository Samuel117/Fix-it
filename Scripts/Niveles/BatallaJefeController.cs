using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatallaJefeController : ControladorNvl
{
    [SerializeField] int EngranajesJefePrimera;
    [SerializeField] int EngranajesJefeSegunda;
    bool scrumMasterDerrotado = false;

   
    private void Start()
    {
        //Tocar musica 
        MusicPlayer.loop = true;
        MusicPlayer.clip = LevelTheme;
        MusicPlayer.Play();

        data = GeneralPlayerData.CargarInfo();
        engranajesGanados = FindObjectOfType<PortalJefe>().engranajesActuales;
        engranjes = int.Parse(data[1]);
        CargarPersonaje();
        //FindObjectOfType<Jugador>().gameObject.transform.position = puntoAparicion.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        FinNivel();
        Pausa();
        DerrotaNvl();
        DerrotaJefe();
    }

    private void DerrotaJefe()
    {
        if(FindObjectOfType<Jefes>() == null && !scrumMasterDerrotado)
        {
            scrumMasterDerrotado = true;
            MusicPlayer.Stop();
            MusicPlayer.PlayOneShot(Victoria);
        }
    }
}
