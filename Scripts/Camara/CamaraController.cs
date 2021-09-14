using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.GetComponent<CinemachineVirtualCamera>().Follow == null && FindObjectOfType<Jugador>() != null)
        {
            this.gameObject.GetComponent<CinemachineVirtualCamera>().Follow = FindObjectOfType<Jugador>().transform;
        }
    }
}
