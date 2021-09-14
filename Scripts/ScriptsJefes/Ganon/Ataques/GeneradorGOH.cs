using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorGOH : MonoBehaviour
{
    [SerializeField] GameObject CampoGravitatorio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<Ganon>().transform.Find("CampoGravitatorio").gameObject.activeSelf)
        {
           if(FindObjectOfType<Jugador>() != null && FindObjectOfType<Jugador>().gameObject.transform.position.x > this.transform.position.x)
            {
                FindObjectOfType<Ganon>().transform.Find("CampoGravitatorio").gameObject.GetComponent<AreaEffector2D>().forceMagnitude = -200;
            }
            else
            {
                FindObjectOfType<Ganon>().transform.Find("CampoGravitatorio").gameObject.GetComponent<AreaEffector2D>().forceMagnitude = 200;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.GetComponent<Jugador>()!= null)
        {
            FindObjectOfType<Ganon>().GOH_Controller();
        }
    }

    private void Desactivar()
    {
        this.gameObject.SetActive(false);
    }

    private void Activar()
    {
        FindObjectOfType<Ganon>().transform.Find("CampoGravitatorio").gameObject.SetActive(true);
    }
}
