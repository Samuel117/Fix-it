using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Columnas : MonoBehaviour
{  
    private int[] codigo = new int[5];
    private int[] Letra = new int[5];
    private int[] cambio = new int[5];
    private int columnaAvance = 0;
    private bool[] DetenerMovimiento = new bool[5];
    private float[] es = new float[5];
    private float[] Velocidad = new float[5];
    private string Palabra;
    private string[,] LetrasColumnaFila = new string[5, 7];
    private string[] PalabrasClave = new string[10] { "APOLO", "HANZO", "GENJI", "SCRUM", "KEYES", "FLOOD", "BRUTE", "SPARK", "BROLY", "GINYU" };
    private bool inicioJuego;
    [SerializeField] private GameObject[] columnas = new GameObject[5];

    [SerializeField] AudioSource player;
    [SerializeField] AudioClip correcto;
    [SerializeField] AudioClip incorrecto;

    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0; x < 5; x++)
        {
            es[x] = -1f;
            Letra[x] = 0;
            cambio[x] = 7;
            DetenerMovimiento[x] = false;
        }
        inicioJuego = true;
        for(int x = 0; x < 5; x++)
        {
            Velocidad[x] = AsignarVel(Random.Range(0, 4));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inicioJuego)
        {
            TomarPalabra();
            inicioJuego = false;
        }
        for (int x = 0; x < 5; x++)
        {
            Velocidades(x);
        }
    }

    private void Velocidades(int x)
    {
        if (!DetenerMovimiento[x])
        {
            if (PuedeGenerar(es[x]))
            {
                if (cambio[x] <= 0)
                {
                    cambio[x] = 7;
                }
                cambio[x]--;
                Letra[x] = cambio[x];
                codigo[x]++;
                es[x] = Time.time + Velocidad[x];
            }
            for (int y = 1; y < 8; y++)
            {
                if (Letra[x] > 6)
                {
                    Letra[x] = 0;
                }
                if (codigo[x] > 6)
                {
                    codigo[x] = 0;
                }
                columnas[x].transform.Find("Letra" + y).Find("Letra" + y).GetComponent<TextMeshProUGUI>().text = LetrasColumnaFila[x, Letra[x]];
                if (y - 1 == codigo[x])
                {
                    columnas[x].transform.Find("Letra" + y).GetComponent<Image>().color = new Color32(10,82,9,255);
                    columnas[x].transform.Find("Letra" + VerificarNulo(y)).GetComponent<Image>().color = Color.black;
                }
                Letra[x]++;
            }
        }
    }

    private bool PuedeGenerar(float espera)
    {
        return Time.time > espera;
    }

    private void TomarPalabra()
    {
        Palabra = PalabrasClave[Random.Range(0, 10)];
        //Debug.Log(Palabra);
        ConfColumnas();
    }

    private void ConfColumnas()
    {
        int control = 1;
        char letraRandom;
        for(int x = 0; x < 5; x++)
        {
            codigo[x] = Random.Range(0, 7);
            //Debug.Log(codigo[x]);
            LetrasColumnaFila[x, codigo[x]] = Palabra.Substring(x, 1);
            //Debug.Log(LetrasColumnaFila[x, codigo]);
            columnas[x].transform.Find("Letra" + (codigo[x]+1)).GetComponent<Image>().color = Color.red;
            columnas[x].transform.Find("Letra" + (codigo[x]+1)).Find("Letra" + (codigo[x]+1)).GetComponent<TextMeshProUGUI>().text = LetrasColumnaFila[x,codigo[x]];
            for (int y = 0; y < 7; y++)
            {
                //Debug.Log(y);
                if (y != codigo[x])
                {
                    //Debug.Log(y);
                    letraRandom = (char)('A' + Random.Range(0, 26));
                    LetrasColumnaFila[x, y] = letraRandom.ToString();
                    columnas[x].transform.Find("Letra" + control).GetComponent<Image>().color = Color.black;
                    columnas[x].transform.Find("Letra" + control).Find("Letra" + control).GetComponent<TextMeshProUGUI>().text = LetrasColumnaFila[x, y];
                }
                control++;
            }
            control = 1;
        }
    }

    private int VerificarNulo(int lugar)
    {
        if(lugar-1== 0)
        {
            return 7;
        }
        else
        {
            return lugar - 1;
        }
    }

    public int ComprobarLetra()
    {
        if(columnas[columnaAvance].transform.Find("Letra4").GetComponent<Image>().color == new Color32(10, 82, 9, 255))
        {
            player.PlayOneShot(correcto);

            es[columnaAvance] = Time.time - 5;
            DetenerMovimiento[columnaAvance] = true;
            columnaAvance++;
        }
        else
        {
            //incrementador de error
            player.PlayOneShot(incorrecto);

            FindObjectOfType<PuertaSellada>().IntentosDentro--;
            Debug.Log("Fallaste");
        }
        return columnaAvance;
    }

    public void ReiniciarJuego()
    {
        for (int x = 0; x < 5; x++)
        {
            es[x] = -1f;
            Letra[x] = 0;
            cambio[x] = 7;
            DetenerMovimiento[x] = false;
        }
        columnaAvance = 0;
        FindObjectOfType<BarraBlanca>().columna = 0;
        inicioJuego = true;
    }

    private float AsignarVel(int Ver)
    {
        switch (Ver)
        {
            case 1:
                return 0.2f;
            case 2:
                return 0.3f;
            case 3:
                return 0.5f;
            default:
                return 0.5f;
        }
    }
}