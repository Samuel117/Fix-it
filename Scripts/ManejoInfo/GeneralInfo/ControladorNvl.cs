using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorNvl : MonoBehaviour
{

   protected string[] data = new string[3];
    [SerializeField] GameObject[] prefabsPersonajes = new GameObject[5];
    [SerializeField] GameObject menuPausa;
    [SerializeField] protected GameObject puntoAparicion;
    [SerializeField] GameObject pantallaCompletar;
    [SerializeField] GameObject pantallaDerrota;
    [SerializeField] GameObject Enemigos;
    [SerializeField] GameObject Obstaculos;
    [SerializeField] GameObject engObtenidos;
    [SerializeField] int engranajesNivelPrimer;
    [SerializeField] int engranajesNivelSegundo;

    [SerializeField] protected AudioSource MusicPlayer;
    [SerializeField] protected AudioClip Victoria;
    [SerializeField] AudioClip GameOver;
    [SerializeField] AudioClip Derrota;
    [SerializeField] protected AudioClip LevelTheme;
    [SerializeField] GameObject transicion;

    //Engranajes.
    public int engranajesGanados = 0;
    protected int engranjes = 0;
    protected bool nivelCompletado = false;

    // Start is called before the first frame update
    void Start()
    {
        //Tocar musica 
        MusicPlayer.loop = true;
        MusicPlayer.clip = LevelTheme;
       
        MusicPlayer.Play();
     
        data = GeneralPlayerData.CargarInfo();

      
            CargarPersonaje();
        
        engranjes = int.Parse(data[1]);
        Debug.Log("Engranajes actuales: " + engranjes);
    }

    // Update is called once per frame
    void Update()
    {
        FinNivel();
        Pausa();
        DerrotaNvl();

        if (FindObjectOfType<Jugador>() != null && !FindObjectOfType<Jugador>().MinijuegoAbierto && !Cursor.visible)
        {
            Cursor.visible = true;
        }
    }

    protected void CargarPersonaje()
    {
        Debug.Log("CNT NVL: " + data[2]);

        for (int x = 0; x < prefabsPersonajes.Length; x++)
        {
            if (data[2] == prefabsPersonajes[x].name)
            {
                Instantiate(this.prefabsPersonajes[x], puntoAparicion.transform.position, this.transform.rotation);
                break;
            }
        }
    }

    public void SumarEngranajes(int engranajesRecibidos)
    {
        engranajesGanados += engranajesRecibidos;
        Debug.Log("Engranajes acumulados: " + engranajesGanados);
    }

    private void ActualizarEngranajesArchivo()
    {
        engranjes += engranajesGanados;
        
        if(engranjes > 10000)
        {
            engranjes = 10000;
            data[1] = engranjes.ToString();
        }
        else
        {
            data[1] = engranjes.ToString();
        }

        GeneralPlayerData.GuardarInfo(data);
    }

    protected void FinNivel()
    {
        if (nivelCompletado)
        {
            if (!pantallaCompletar.activeSelf)
            {
                MusicPlayer.Stop();
                MusicPlayer.PlayOneShot(Victoria);

                if(int.Parse(data[0]) == int.Parse(data[3]))
                {
                    engranajesGanados += engranajesNivelPrimer;
                    data[0] = (int.Parse(data[0]) + 1).ToString();
                }
                else
                {
                    engranajesGanados += engranajesNivelSegundo;
                }

                ActualizarEngranajesArchivo();
                Debug.Log("Engranajes totales: " + data[1]);
                pantallaCompletar.SetActive(true);
                engObtenidos.gameObject.GetComponent<TextMeshProUGUI>().text = "Engranajes obtenidos: " + engranajesGanados;
                FindObjectOfType<Jugador>().MinijuegoAbierto = true;
            }
          
            if(pantallaCompletar.activeSelf && Input.GetKeyUp(KeyCode.Return))
            {
                nivelCompletado = false;
                FindObjectOfType<Jugador>().MinijuegoAbierto = false;
              
                if(data[3] == 10.ToString())
                {
                    transicion.SetActive(true);
                    StartCoroutine(CargarEscena(15));
                    //SceneManager.LoadScene(15);
                }
                else
                {
                    transicion.SetActive(true);
                    StartCoroutine(CargarEscena(12));
                    //SceneManager.LoadScene(12);
                }
            }
        }
    }

    IEnumerator CargarEscena(int index)
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(index);
    }

    public void nivelCompletadoSetter()
    {
        nivelCompletado = true;
    }

    protected void Pausa()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && FindObjectOfType<Jugador>() != null && !FindObjectOfType<Jugador>().MinijuegoAbierto)
        {
            if (!menuPausa.activeSelf)
            {
                MusicPlayer.Pause();
                menuPausa.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    protected void DerrotaNvl()
    {
        if(FindObjectOfType<Jugador>() == null && !pantallaDerrota.activeSelf)
        {
            MusicPlayer.Stop();
            MusicPlayer.PlayOneShot(GameOver);

            Enemigos.gameObject.SetActive(false);
            Obstaculos.gameObject.SetActive(false);
            pantallaDerrota.SetActive(true);
        }

        if (!MusicPlayer.isPlaying && pantallaDerrota.activeSelf)
        {
            MusicPlayer.PlayOneShot(Derrota);
        }
    }
}
