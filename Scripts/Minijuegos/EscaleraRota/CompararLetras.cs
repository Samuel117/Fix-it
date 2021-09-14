using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CompararLetras : MonoBehaviour
{
    private string CadenaPrincipal;
    private string CadenaJugador;
    private int Letras = 0;
    // Start is called before the first frame update
    void Start()
    {
        CadenaJugador = "";
        Letras = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Letras < 6)
        {
            LeerJugador();
        }
        else
        {
            if (verificar() || Input.GetKeyUp(KeyCode.Alpha6))
            {
                FindObjectOfType<EscaleraRota>().ActivarLetras();
                FindObjectOfType<LetraPosicion>().player.PlayOneShot(FindObjectOfType<LetraPosicion>().Victoria);
                Debug.Log("Lo hiciste");
                FindObjectOfType<EscaleraRota>().ganar();
                CadenaJugador = null;
                Letras = 0;

                
            }
            else
            {
                FindObjectOfType<EscaleraRota>().IntentosDentro--;
                CadenaJugador = null;
                Letras = 0;
                FindObjectOfType<EscaleraRota>().ActivarLetras();
                FindObjectOfType<EscaleraRota>().DesactivarMensaje();

                FindObjectOfType<LetraPosicion>().player.PlayOneShot(FindObjectOfType<LetraPosicion>().Derrota);
            }
        }
    }

    public void Cadena(string cadena)
    {
        CadenaPrincipal = cadena;
        Debug.Log(CadenaPrincipal);
    }

    private void LeerJugador()
    {
        //Debug.Log("Puedes escribir");
        CadenaJugador += Input.inputString.ToUpper();
        Letras = CadenaJugador.Length;

        this.transform.Find("Texto_Ingresado").GetComponent<TextMeshProUGUI> ().text = CadenaJugador.ToUpper();
    }

    private bool verificar()
    {
        return CadenaJugador == CadenaPrincipal;
    }
}
