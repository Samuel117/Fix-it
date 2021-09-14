using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bateria : MonoBehaviour
{
    [SerializeField] protected Slider bateria;
    [SerializeField] protected GameObject valorBateria;

    private float SaludJugadorMax = 0f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if( SaludJugadorMax == 0)
        {
            SaludJugadorMax = FindObjectOfType<Jugador>().bateriaMax;
            bateria.minValue = 1;
            bateria.maxValue = (int)SaludJugadorMax;
            bateria.value = SaludJugadorMax;
            bateria.fillRect.gameObject.GetComponent<Image>().color = Color.Lerp(Color.red, new Color32(35, 159, 30, 255), bateria.value / SaludJugadorMax);
            valorBateria.GetComponent<TextMeshProUGUI>().text = SaludJugadorMax.ToString();

        }
    }

    public void ActualizarBateria( int valor)
    {
        bateria.value = valor;
        bateria.fillRect.gameObject.GetComponent<Image>().color = Color.Lerp(Color.red, new Color32(35,159,30,255), bateria.value / SaludJugadorMax);
        valorBateria.GetComponent<TextMeshProUGUI>().text = FindObjectOfType<Jugador>().BateriaGetter().ToString();
    }
}
