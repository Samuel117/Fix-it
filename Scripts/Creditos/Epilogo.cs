using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Epilogo : MonoBehaviour
{
    public bool direccion = false;
    private float esperar = 0f;
    private Color32 col;
    int alpha = 0;
    public bool desaparecer = false;
    private float esperarFadePerma;
    private bool creditosAct = false;
    [SerializeField] GameObject creditos;
    [SerializeField] AudioSource player;
    [SerializeField] AudioClip music;

    // Start is called before the first frame update
    void Start()
    {
        player.clip = music;
        player.Play();

        col = this.GetComponent<TextMeshProUGUI>().color;
        esperarFadePerma = Time.time + 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if (esperarFadeColor(esperarFadePerma) && !direccion)
        {
            Fade();

        }else if (direccion && !creditosAct)
        {
            creditos.SetActive(true);
            creditosAct = true;
        }
    }

    private void Fade()
    {
        if (esperarFadeColor(esperar))
        {
            FadeColor();
            esperar = Time.time + 0.03f;
        }
    }
    private void FadeColor()
    {
        if (!direccion)
        {
            this.GetComponent<TextMeshProUGUI>().color = new Color32(col.r, col.g, col.b, col.a--);
            this.GetComponent<TextMeshProUGUI>().alpha -= 0.1f;
            alpha = col.a;

            if (alpha <= 0)
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
