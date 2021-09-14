using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Discos : MonoBehaviour
{
    [SerializeField] private GameObject[] discos = new GameObject[5];
    [SerializeField] private GameObject tiempo;
    [SerializeField] private GameObject discosListos;
    [SerializeField] AudioSource player;
    [SerializeField] AudioClip victoria;
    [SerializeField] AudioClip derrota;
    [SerializeField] AudioClip seleccionar;

    int DiscoSeleccionado = 0;
    int discosAlineados = 0;
    float duracion;
    int segundosRest= 15;
    float espera = 1f;

    private bool debeReiniciar = true;
    private Quaternion[] rotacionesInicio = new Quaternion[5];
    private Quaternion[] rotacionesRandom = new Quaternion[5];
    private float posicionRandom;

    [SerializeField] Sprite Apagado;
    [SerializeField] Sprite Encendido;

    List<Quaternion> posiciones = new List<Quaternion>();

    // Start is called before the first frame update
    void Start()
    {
        //Aparecer en rotaciones aleatorias no repetidas

        duracion = Time.time + 15f;

        for(int x = 0; x < discos.Length; x++)
        {
            rotacionesInicio[x] = discos[x].transform.rotation;
        }
        DiscoSeleccionado--;
    }

    // Update is called once per frame
    void Update()
    {
        if (debeReiniciar)
        {
            duracion = Time.time + 15f;
            espera = Time.time + 1f;
            debeReiniciar = false;
            tiempo.GetComponent<TextMeshProUGUI>().text = "Segundos: " + segundosRest;
        }

        if(Time.time > duracion)
        {
            Reinicio();
            player.PlayOneShot(derrota);
            FindObjectOfType<CampoDeEnergia>().Derrota();
        }

        if (MenosTimepo(espera))
        {
            segundosRest--;
            tiempo.GetComponent<TextMeshProUGUI>().text = "Segundos: " + segundosRest;
            
            espera = Time.time + 1f;
        }
        
        SeleccionDisco();
        
        ComprobarVictoria();
    }

    private void FixedUpdate()
    {
        RotarDisco();
    }

    private void SeleccionDisco()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            player.PlayOneShot(seleccionar);
            DiscoSeleccionado++;

            if (DiscoSeleccionado > 0)
            {
                discos[DiscoSeleccionado - 1].gameObject.GetComponent<Image>().sprite = Apagado;
            }

            if (DiscoSeleccionado > 4)
            {
                DiscoSeleccionado = 0;
            }

            discos[DiscoSeleccionado].gameObject.GetComponent<Image>().sprite = Encendido;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            player.PlayOneShot(seleccionar);
            DiscoSeleccionado--;
            
            if(DiscoSeleccionado < 4)
            {
                discos[DiscoSeleccionado + 1].gameObject.GetComponent<Image>().sprite = Apagado;
            }

            if (DiscoSeleccionado < 0)
            {
                DiscoSeleccionado = 4;
            }

            discos[DiscoSeleccionado].gameObject.GetComponent<Image>().sprite = Encendido;
        }
    }

    private void RotarDisco()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            discos[DiscoSeleccionado].transform.Rotate(Vector3.forward * -2);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            discos[DiscoSeleccionado].transform.Rotate(Vector3.forward * 2);
        }
    }

    private void ComprobarVictoria()
    {
        int escala = 40;
        for (int x = 0; x < discos.Length; x++)
        {
            if (discos[x].transform.rotation.z * escala >= -5 && discos[x].transform.rotation.z * escala <= 5)
            {
                discosAlineados++;
            }
            escala += 30;
        }

        if (discosAlineados >= 5)
        {
            player.PlayOneShot(victoria);
            FindObjectOfType<CampoDeEnergia>().Victoria();
        }
        else
        {
            discosListos.GetComponent<TextMeshProUGUI>().text = "Discos: " + discosAlineados + "/5";
            discosAlineados = 0;
        }
    }

    private bool MenosTimepo(float espera)
    {
        
        return Time.time > espera;
    }

    private void Reinicio()
    {
        int x = 0;

        discosAlineados = 0;
        DiscoSeleccionado = 0;
        segundosRest = 15;
        debeReiniciar = true;
        DiscoSeleccionado--;

        while(x < 5)
        {
            posicionRandom = Random.Range(-190, 190);
           
            rotacionesRandom[x] = rotacionesInicio[x];
            rotacionesRandom[x].z = rotacionesInicio[x].z + (posicionRandom/100);

            if (!posiciones.Contains(rotacionesRandom[x]))
            {
                posiciones.Add(rotacionesRandom[x]);
                discos[x].transform.rotation = rotacionesRandom[x];
                x++;
            }
        }

        x = 0;
        posiciones.Clear();
    }
}
