using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MensajeDerrota : MonoBehaviour
{
    private bool activado = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(FindObjectOfType<SCRUM_Master>() == null && !activado)
        {
            this.transform.Find("Mensaje").gameObject.SetActive(true);
            activado = true;
        }
    }
}
