using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InstruccionesMapa : MonoBehaviour
{
    public bool vieneTutorial = false;
    private bool activado = false;
    [SerializeField] GameObject instrucciones;
    private int index = 0;

    [SerializeField] AudioSource player;
    [SerializeField] AudioClip efecto;

    private string[] texto = new string[9] 
    { 
        "",
        "Bienvenido al mapa!, aqui podras seleccionar la siguente estructura que quieras reparar utilizando las flechas izquierda y derecha para moverte y enter para entrar al nivel.", 
        "Utiliza la letra 'M' para abrir el menú del mapa, donde encontraras las siguentes opciones: ",
        "Personajes: Podras ver los personajes jugables, comprarlos y selecccionarlos cuando esten desbloquedos, tambien podras mejrarlos para fortalecerlos.",
        "Manual de daños: Podras ver todos los daños que pueden causar los robots defectuosos y como  deben ser reparardos.",
        "Manual de enemigos: Aqui podras encontrar un registro de todos los tipos de enemigos a los que te hayas enfrentado.",
        "Salir: Volver al menú principal.",
        "Engranajes: En la parte superior derecha apareceran tus engranajes, necesarios para mejorar y obtener personajes, obtenlos derrotando enemigos, reparando y superando niveles!",
        "Ahora estas listo para comenzar, buena suerte!",
    };

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
       
        this.instrucciones.gameObject.GetComponent<TextMeshProUGUI>().text = texto[index];
    }

    // Update is called once per frame
    void Update()
    {
        if (vieneTutorial && !activado)
        {
            this.transform.Find("Canvas").gameObject.SetActive(true);
            activado = true; 
        }

        if (activado)
        {
            PasarInstrucciones();
        }
    }

    private void PasarInstrucciones()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.PlayOneShot(efecto);
            index++;
            if(index > 8)
            {
                Destroy(this.gameObject);
            }
            else
            {
                this.instrucciones.gameObject.GetComponent<TextMeshProUGUI>().text = texto[index] + " (Presiona espacio para continuar)";
            }
        }
    }
}
