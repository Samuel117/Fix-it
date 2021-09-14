using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Botones : MonoBehaviour
{
    [SerializeField] AudioClip sonidoSobreBtn;
    [SerializeField] AudioSource AudioPlayer;
    [SerializeField] GameObject texto;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SonidoSobreBoton()
    {
        AudioPlayer.PlayOneShot(sonidoSobreBtn);
    }

    public void Brillar()
    {
        texto.GetComponent<TextMeshProUGUI>().color = new Color32(255,255,255, 255);
    }

    public void Apagar()
    {
        texto.GetComponent<TextMeshProUGUI>().color = new Color32(255,189,0, 255);
    }

    public void BrillarMapa()
    {
        texto.GetComponent<TextMeshProUGUI>().color = new Color32(255, 189, 0, 255);
        AudioPlayer.PlayOneShot(sonidoSobreBtn);
    }

    public void ApagarMapa()
    {
        texto.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
    }
}
