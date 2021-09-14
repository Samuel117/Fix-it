using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaDerrota : MonoBehaviour
{
    private string[] Data = new string[3];
    [SerializeField] GameObject transicion;

    // Start is called before the first frame update
    void Start()
    {
        Data = GeneralPlayerData.CargarInfo();
    }

    // Update is called once per frame
    void Update()
    {
        ContinuarBtn();
    }

    public void Salir()
    {
        transicion.SetActive(true);
        StartCoroutine(CargarEscena(12));
        //SceneManager.LoadScene(12);
    }

    public virtual void Continuar()
    {
        transicion.SetActive(true);
        StartCoroutine(CargarEscena(int.Parse(Data[3])));
        //SceneManager.LoadScene(int.Parse(Data[3]));
    }

    public void ContinuarBtn()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            transicion.SetActive(true);
            StartCoroutine(CargarEscena(int.Parse(Data[3])));
        }
    }

    IEnumerator CargarEscena(int index)
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(index);
    }
}
