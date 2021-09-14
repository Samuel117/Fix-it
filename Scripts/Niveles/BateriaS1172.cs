using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BateriaS1172 : MonoBehaviour
{
    [SerializeField] protected Slider bateria;

    private float SaludJugadorMax = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SaludJugadorMax == 0)
        {
            if (FindObjectOfType<Jugador>() != null && FindObjectOfType<S117>() == null)
            {
                this.gameObject.SetActive(false);
            }
            else if (FindObjectOfType<Jugador>() != null && FindObjectOfType<S117>() != null)
            {
                SaludJugadorMax = FindObjectOfType<S117>().bateria2S117;
                bateria.minValue = 1;
                bateria.maxValue = (int)SaludJugadorMax;
                bateria.value = SaludJugadorMax;
                bateria.fillRect.gameObject.GetComponent<Image>().color = Color.Lerp(Color.red, new Color32(89, 180, 251, 255), bateria.value / SaludJugadorMax);
            }
        }
    }

    public void ActualizarBateriaS1172(int valor)
    {
        bateria.value = valor;
        bateria.fillRect.gameObject.GetComponent<Image>().color = Color.Lerp(Color.red, new Color32(89, 180, 251, 255), bateria.value / SaludJugadorMax);
    }
}
