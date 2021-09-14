using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Focos_Clima : MonoBehaviour
{
    [SerializeField] AudioSource player;
    [SerializeField] AudioClip click;
    [SerializeField] AudioClip cambiaColor;

    private Color[] Colores = new Color[5] { 
        new Color32(219, 65, 65, 255), //Rojo
        new Color32(66, 117, 219, 255), //Azul
        new Color32(66, 219, 96, 255), // Verde
        new Color32(244, 255, 1, 255), //Amarillo
         new Color32(152, 152, 152, 255) //Apagado
    }; 

    private Image FocoControl;
    private Image[] Focos = new Image[12];
    private int colorRandom;
    private int colorRandomControl;
    private float esperarCambioColor = 0f;
    private float esperarCambioColorControl = 0f;
    private float duracion;
    private int puntos = 0;
    private int tiempo = 30;
    private bool debeReiniciar = false;

    private float esperarTiempo;

    // Start is called before the first frame update
    void Start()
    {
        duracion = Time.time + 30f;
        //esperarTiempo = Time.time + 1f;

        FocoControl = this.transform.Find("Foco_Control").GetComponent<Image>();
        int indexInicarFocos = 1;
        for(int x = 0; x < Focos.Length; x++)
        {
            Focos[x] = this.transform.Find("Foco" + indexInicarFocos).GetComponent<Image>();
            indexInicarFocos++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (debeReiniciar)
        {
            duracion = Time.time + 30f;
            debeReiniciar = false;
            Debug.Log("Tiempo reiniciado");
        }

        if (DebeContinuar(duracion))
        {
            if (ActualizarTiempo(esperarTiempo)) {
                this.transform.Find("Tiempo").GetComponent<TextMeshProUGUI>().text = "Tiempo restante: " + tiempo.ToString();
                tiempo--;
                esperarTiempo = Time.time + 1f;
            }

            if (DebeCambiarColor(esperarCambioColor))
            {
                Secuencia();
                ReactivarBotones();
              
            }

            if (DebeCambiarColorControl(esperarCambioColorControl))
            {
                SecuenciaControl();
            }

            if (puntos >= 20)
            {
                FindObjectOfType<Control_Clima>().Victoria();
            }
        }
        else
        {
                FindObjectOfType<Control_Clima>().Derrota();
        }
    }

    private void Secuencia()
    {
        for(int x = 0; x < Focos.Length; x++)
        {
            colorRandom = Random.Range(0, 4);
            Focos[x].color = Colores[colorRandom];
        }
        esperarCambioColor = Time.time + 1.5f;
    }

    private void SecuenciaControl()
    {
        colorRandomControl = Random.Range(0, 4);
        FocoControl.color = Colores[colorRandomControl];
        player.PlayOneShot(cambiaColor);
        esperarCambioColorControl = Time.time + 4.5f;
    }

    public void PresionarBoton()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        //Debug.Log(name);
        int numFoco =(int)Char.GetNumericValue(name[name.Length-1]);

        if (numFoco == 0|| numFoco == 1 || numFoco == 2)
        {
            if((int)Char.GetNumericValue(name[name.Length - 2]) == 1)
            {
                numFoco = 10 + (int)Char.GetNumericValue(name[name.Length - 1]);
            }
        }

        Debug.Log("Foco: " + (numFoco-1));
        if (Focos[numFoco-1].color == FocoControl.color)
        {
            player.PlayOneShot(click);

            puntos++;
            this.transform.Find(name).GetComponent<Button>().interactable = false;
            this.transform.Find("Puntaje").GetComponent<TextMeshProUGUI>().text = "Puntos: "+ puntos + "/20";
            Debug.Log("Puntos: " + puntos);
        }
        //10,11,12
    }

    private void ReactivarBotones()
    {
        int indexInicarFocos = 1;
        for (int x = 0; x < Focos.Length; x++)
        {
             this.transform.Find("Foco" + indexInicarFocos).GetComponent<Button>().interactable = true;
            indexInicarFocos++;
        }
        
    }

    private bool DebeCambiarColor(float esperarCambiar)
    {
        return Time.time > esperarCambiar;
    }

    private bool DebeCambiarColorControl(float esperarCambiarControl)
    {
        return Time.time > esperarCambiarControl;
    }

    private bool DebeContinuar(float duracion)
    {
        return Time.time < duracion;
    }

    public void Reiniciar()
    {
        puntos = 0;
        tiempo = 30;
        debeReiniciar = true;
        this.transform.Find("Puntaje").GetComponent<TextMeshProUGUI>().text = "Puntos: 0/20";
        this.transform.Find("Tiempo").GetComponent<TextMeshProUGUI>().text = "Tiempo restante: " + tiempo.ToString();
    }

    private bool ActualizarTiempo(float esperar)
    {
        return Time.time > esperar;
    }
}
