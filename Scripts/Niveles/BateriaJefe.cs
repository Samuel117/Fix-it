using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BateriaJefe : MonoBehaviour
{
    private float SaludJefeMax = 0f;

    [SerializeField] protected Slider bateria;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SaludJefeMax == 0)
        {
            SaludJefeMax = FindObjectOfType<Jefes>().bateria;
            bateria.minValue = 1;
            bateria.maxValue = (int)SaludJefeMax;
            bateria.value = SaludJefeMax;
            bateria.fillRect.gameObject.GetComponent<Image>().color = Color.Lerp(Color.red, Color.yellow, bateria.value / SaludJefeMax);
        }
    }

    public void ActualizarBateriaJefe(int valor)
    {
        bateria.value = valor;
        bateria.fillRect.gameObject.GetComponent<Image>().color = Color.Lerp(Color.red, Color.yellow, bateria.value / SaludJefeMax);
    }
}
