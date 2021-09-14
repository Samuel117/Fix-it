using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextoAparecer : MonoBehaviour
{
    public bool direccion = false;
    private float esperar = 0f;
    private Color32 col;
    int alpha = 0;
    public bool desaparecer = false;
    private float esperarFadePerma;

    // Start is called before the first frame update
    void Start()
    {
        col = this.GetComponent<TextMeshProUGUI>().color;
        esperarFadePerma = Time.time + 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (esperarFadeColor(esperarFadePerma) && !direccion)
        {
            Fade();
        }
    }

    private void Fade()
    {
        if (esperarFadeColor(esperar))
        {
            FadeColor();
            esperar = Time.time + 0.01f;
        }
    }
    private void FadeColor()
    {
        if (!direccion)
        {
            this.GetComponent<TextMeshProUGUI>().color = new Color32(col.r, col.g, col.b, col.a++);
            this.GetComponent<TextMeshProUGUI>().alpha -= 0.1f;
            alpha = col.a;

            if (alpha >= 255)
            {
                direccion = true;
                desaparecer = false;
            }
        }
    }


    private bool esperarFadeColor(float esperar)
    {
        return Time.time > esperar;
    }
}
