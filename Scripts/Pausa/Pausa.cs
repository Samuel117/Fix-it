using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour
{
    [SerializeField] AudioSource GeneralMusicPlayer;
    [SerializeField] AudioSource PausePlayer;
    [SerializeField] AudioClip PauseTheme;
    [SerializeField] AudioClip PauseStart;
    [SerializeField] AudioClip PauseEnd;
    [SerializeField] GameObject transicion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        PausePlayer.PlayOneShot(PauseStart);
        PausePlayer.PlayOneShot(PauseTheme);    
    }

    public void Continuar()
    {
        PausePlayer.Stop();
        PausePlayer.PlayOneShot(PauseEnd);
        GeneralMusicPlayer.UnPause();
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void Salir()
    {
        //Mandar al mapa
        Time.timeScale = 1;

        transicion.SetActive(true);
        StartCoroutine(CargarEscena(12));
        //SceneManager.LoadScene(12);
    }

    IEnumerator CargarEscena(int index)
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(index);
    }
}
