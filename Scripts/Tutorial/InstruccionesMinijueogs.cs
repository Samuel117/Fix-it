using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstruccionesMinijueogs : MonoBehaviour
{
    [SerializeField] int NumMinijuego;
    MinijuegosData.MinijugosInfo info;
    [SerializeField] private Sprite[] imagenesMinijueogos = new Sprite[18];

    // Start is called before the first frame update
    void Start()
    {
        info = MinijuegosData.CargarInfo();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Find("Titulo").gameObject.GetComponent<TextMeshProUGUI>().text = info.nombre[NumMinijuego];
        this.transform.Find("Instrucciones").gameObject.GetComponent<TextMeshProUGUI>().text = info.reparar[NumMinijuego] + " (Presiona Espacio para continuar)";
        this.transform.Find("Img_Minijuego").gameObject.GetComponent<Image>().sprite = imagenesMinijueogos[NumMinijuego];
    }
}
