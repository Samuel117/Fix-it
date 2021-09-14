using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Numeros_Puente : MonoBehaviour
{
    private int Num_1, Num_2;
    private bool Win = false;
    [SerializeField] public AudioSource player;
    [SerializeField] AudioClip flechas;
    [SerializeField] AudioClip confirmar;
    [SerializeField] AudioClip victoria;
    [SerializeField] AudioClip derrota;


    // Start is called before the first frame update
    void Start()
    {
        Num_1 = 0;
        this.transform.Find("BG_Numero1").Find("Numero_1").GetComponent<TextMeshProUGUI>().text = Num_1.ToString();
        Num_2 = 0;
        this.transform.Find("BG_Numero2").Find("Numero_2").GetComponent<TextMeshProUGUI>().text = Num_1.ToString();
        Win = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void incrementarNum1()
    {
        player.PlayOneShot(flechas);
        if (Num_1 == 9)
        {
            Num_1 = 0;
        }
        else
        {
            Num_1++;
        }
        this.transform.Find("BG_Numero1").Find("Numero_1").GetComponent<TextMeshProUGUI>().text = Num_1.ToString();
    }

    public void decrementarNum1()
    {
        player.PlayOneShot(flechas);
        if (Num_1 == 0)
        {
            Num_1 = 9;
        }
        else
        {
            Num_1--;
        }
        this.transform.Find("BG_Numero1").Find("Numero_1").GetComponent<TextMeshProUGUI>().text = Num_1.ToString();
    }

    public void incrementarNum2()
    {
        player.PlayOneShot(flechas);
        if (Num_2 == 9)
        {
            Num_2 = 0;
        }
        else
        {
            Num_2++;
        }
        this.transform.Find("BG_Numero2").Find("Numero_2").GetComponent<TextMeshProUGUI>().text = Num_2.ToString();
    }

    public void decrementarNum2()
    {
        player.PlayOneShot(flechas);
        if (Num_2 == 0)
        {
            Num_2 = 9;
        }
        else
        {
            Num_2--;
        }
        this.transform.Find("BG_Numero2").Find("Numero_2").GetComponent<TextMeshProUGUI>().text = Num_2.ToString();
    }

    public void Confirmar()
    {
        player.PlayOneShot(confirmar);

        FindObjectOfType<MensajePuente>().verificar(Num_1, Num_2);

        if (FindObjectOfType<MensajePuente>().Num1Con())
        {
            this.transform.Find("BG_Numero1").Find("Aumentar_1").gameObject.SetActive(false);
            this.transform.Find("BG_Numero1").Find("Decrementar_1").gameObject.SetActive(false);
        }
        if (FindObjectOfType<MensajePuente>().Num2Con())
        {
            this.transform.Find("BG_Numero2").Find("Aumentar_2").gameObject.SetActive(false);
            this.transform.Find("BG_Numero2").Find("Decrementar_2").gameObject.SetActive(false);
        }

        if (FindObjectOfType<MensajePuente>().Num1Con() && FindObjectOfType<MensajePuente>().Num2Con())
        {
            player.PlayOneShot(victoria);
            Win = true;
            FindObjectOfType<PuenteDesactivado>().Ganar();

        }
        if (!Win)
        {
            if (FindObjectOfType<PuenteDesactivado>().IntentosDentro == 0)
            {
                player.PlayOneShot(derrota);
                FindObjectOfType<PuenteDesactivado>().IntentosDentro = 8;
                Num_1 = 0;
                this.transform.Find("BG_Numero1").Find("Numero_1").GetComponent<TextMeshProUGUI>().text = Num_1.ToString();
                Num_2 = 0;
                this.transform.Find("BG_Numero2").Find("Numero_2").GetComponent<TextMeshProUGUI>().text = Num_1.ToString();
                this.transform.Find("BG_Numero1").Find("Aumentar_1").gameObject.SetActive(true);
                this.transform.Find("BG_Numero1").Find("Decrementar_1").gameObject.SetActive(true);
                this.transform.Find("BG_Numero2").Find("Aumentar_2").gameObject.SetActive(true);
                this.transform.Find("BG_Numero2").Find("Decrementar_2").gameObject.SetActive(true);
                FindObjectOfType<PuenteDesactivado>().Perder();
            }
        }
        
    }
}