using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Generador_Gravitatorio : Minijuegos
{
    [SerializeField] GameObject reparado;
    public int LetrasAtinadas = 0;
    public int FigurasCorrectas = 0;

    //Etapa 2 
    [SerializeField] GameObject[] Cristales = new GameObject[6];

    private Sprite[][] sprites = new Sprite[3][];
    [SerializeField] Sprite[] spritesCristales1 = new Sprite[6];
    [SerializeField] Sprite[] spritesCristales2 = new Sprite[6];
    [SerializeField] Sprite[] spritesCristales3 = new Sprite[6];

    List<int> RandomNumbers = new List<int>();

    private int Num1 = 0;
    private int Num2 = 0;
    private int[] nums;

    //Etapa 3 
    public float TimepoE3;
    private bool E3 = false;
    int contador = 20;
    float cambiarTiempo = 0f;

    // Start is called before the first frame update
    void Start()
    {
        engranajes = 100;
        MinijuegoAbierto = true;
        IntentosDentro = 5;

        //Etapa2
        sprites[0] = spritesCristales1;
        sprites[1] = spritesCristales2;
        sprites[2] = spritesCristales3;

        ConfigurarEtapa2();
    }

    // Update is called once per frame
    void Update()
    {
        if (LetrasAtinadas == 5)//cambiar a 2
        {
            this.transform.Find("Canvas").Find("Etapa1").gameObject.SetActive(false);
            this.transform.Find("Canvas").Find("Etapa2").gameObject.SetActive(true);
            LetrasAtinadas++;
        }
        if (IntentosDentro <= 0)
        {
            LetrasAtinadas = 0;
            IntentosDentro = 5;

            Sansion();
        }
        if(FigurasCorrectas < 3)
        {

            switch (FigurasCorrectas)
            {
                case 1:
                    if(this.transform.Find("Canvas").Find("Etapa3").Find("Hexagono2").Find("Vertice1").GetComponent<BoxCollider2D>().enabled == false)
                    {
                        for(int x = 1; x < 7; x++)
                        {
                            this.transform.Find("Canvas").Find("Etapa3").Find("Hexagono1").Find("Vertice" + x).GetComponent<BoxCollider2D>().enabled = false;
                        }
                        for (int x = 1; x < 7; x++)
                        {
                            this.transform.Find("Canvas").Find("Etapa3").Find("Hexagono2").Find("Vertice" + x).GetComponent<BoxCollider2D>().enabled = true;
                        }
                    }
                    break;
                case 2:
                    if (this.transform.Find("Canvas").Find("Etapa3").Find("Hexagono3").Find("Vertice1").GetComponent<BoxCollider2D>().enabled == false)
                    {
                        for (int x = 1; x < 7; x++)
                        {
                            this.transform.Find("Canvas").Find("Etapa3").Find("Hexagono2").Find("Vertice" + x).GetComponent<BoxCollider2D>().enabled = false;
                        }
                        for (int x = 1; x < 7; x++)
                        {
                            this.transform.Find("Canvas").Find("Etapa3").Find("Hexagono3").Find("Vertice" + x).GetComponent<BoxCollider2D>().enabled = true;
                        }
                    }
                    break;

            }
        }
        if (FigurasCorrectas == 3)
        {
            Recompensa();
        }

        if (E3 && Time.time > TimepoE3)
        {
            Sansion();
        }

        if (E3 && Temporizador(cambiarTiempo))
        {
            //this.transform.Find("Canvas").Find("Etapa3").Find("Tiempo").gameObject.GetComponent<TextMeshProUGUI>().text = contador.ToString();
            contador--;
            cambiarTiempo = Time.time + 1f;
        }
    }

    protected override void Recompensa()
    {
        Instantiate(reparado, this.transform.position, this.transform.rotation);
        FindObjectOfType<fondo>().Reparado = true;
        CerrarMinijuegoVictoria();
    }

    protected override void Sansion()
    {
        int bateria = FindObjectOfType<Jugador>().BateriaGetter();
        FindObjectOfType<Jugador>().RecibirDano(bateria);
        CerrarMinijueogDerrota();
    }

    private void ConfigurarEtapa2()
    {
        Num1 = Random.Range(0, 3);

        for (int x = 0; x < Cristales.Length; x++)
        {
            Num2 = Random.Range(0, 6);

            if (!RandomNumbers.Contains(Num2))
            {
                RandomNumbers.Add(Num2);
                Cristales[x].GetComponent<Image>().sprite = sprites[Num1][Num2];
                Cristales[x].gameObject.GetComponent<Cristal>().codigo = Num2;
                Debug.Log(Num2);
            }
            else
            {
                x--;
            }
        }
    }

    public void LlamarEtapa3()
    {
        this.transform.Find("Canvas").Find("Etapa2").gameObject.SetActive(false);
        this.transform.Find("Canvas").Find("Etapa3").gameObject.SetActive(true);

        TimepoE3 = Time.time + 20f;
        E3 = true;
    }

    private bool Temporizador(float CambiarTiempo)
    {
        return Time.time > CambiarTiempo;
    }

}
