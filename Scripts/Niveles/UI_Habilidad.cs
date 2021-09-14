using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Habilidad : MonoBehaviour
{
    [SerializeField] Sprite[] LogoHabilidad = new Sprite[5];
    [SerializeField] Sprite[] LogoHabilidadDes = new Sprite[5];
    [SerializeField] AudioSource player;
    [SerializeField] AudioClip He_Ready;
    protected string[] data = new string[3];
    public bool ready = true;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        data = GeneralPlayerData.CargarInfo();

        for (int x = 0; x < LogoHabilidad.Length; x++)
        {
            if (data[2] == LogoHabilidad[x].name)
            {
                this.gameObject.GetComponent<Image>().sprite = LogoHabilidad[x];
                index = x;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HElista()
    {
        player.PlayOneShot(He_Ready);
        this.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        this.gameObject.GetComponent<Image>().sprite = LogoHabilidad[index];
        ready = true;
    }

    public void HEnoLista()
    {
        this.gameObject.GetComponent<Image>().color = new Color32(255,255,255,100);
        this.gameObject.GetComponent<Image>().sprite = LogoHabilidadDes[index];
        ready = false;
    }
}
