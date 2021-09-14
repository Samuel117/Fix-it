using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class txtStart : MonoBehaviour
{
    protected Color32 col;
    protected float esperar = 0f;
    protected int alpha = 0;
    bool direccion = false;
    [SerializeField] GameObject Menu;
    [SerializeField] AudioSource player;
    [SerializeField] AudioClip efecto;

    // Start is called before the first frame update
    void Start()
    {
       col = this.GetComponent<TextMeshProUGUI>().color;
        alpha = col.a;
    }

    // Update is called once per frame
    void Update()
    {
        if (esperarFadeColor(esperar))
        {
            FadeColor();
            esperar = Time.time + 0.01f;
        }

        ActivarMenu();
    }

    protected void FadeColor()
    {
       if(!direccion)
        {
            this.GetComponent<TextMeshProUGUI>().color = new Color32(col.r, col.g, col.b, col.a--);
            alpha = col.a;

            if (alpha <= 0)
            {
                direccion = true;
            }
        }
       
       if(direccion)
        {
            this.GetComponent<TextMeshProUGUI>().color = new Color32(col.r, col.g, col.b, col.a++);
            alpha = col.a;

            if(alpha >= 255)
            {
                direccion = false;
            }
        }
    }

    protected bool esperarFadeColor(float esperar)
    {
       return Time.time > esperar;
    }

    private void ActivarMenu()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            player.PlayOneShot(efecto);
            this.gameObject.SetActive(false);
            Menu.gameObject.SetActive(true);
        }
    }
}
