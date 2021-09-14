using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalJefe : MonoBehaviour
{
    private bool transportado = false;
    public int engranajesActuales;
    // Start is called before the first frame update

    [SerializeField] string nombreEscena;
    [SerializeField] GameObject transicion;
    void Start()
    {
        

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //if(FindObjectOfType<Jugador>() != null && !savedPlayer)
        //{
        //    DontDestroyOnLoad(FindObjectOfType<Jugador>());
        //    savedPlayer = true;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Jugador>()!= null && !transportado)
        {
            engranajesActuales = FindObjectOfType<ControladorNvl>().engranajesGanados;
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(0,0,0,0);
            transportado = true;

            transicion.SetActive(true);
            StartCoroutine(CargarEscena(nombreEscena));
            //SceneManager.LoadScene(nombreEscena);
        }
    }

    IEnumerator CargarEscena(string index)
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(index);
    }
}
