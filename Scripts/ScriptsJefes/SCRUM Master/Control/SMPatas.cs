using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SMPatas : MonoBehaviour
{
    private float SaludJPatasMax = 0f;

    // Start is called before the first frame update
    void Start()
    {
        SaludJPatasMax = FindObjectOfType<SCRUM_Master>().bateriaPatas;
        this.GetComponent<Image>().color = Color.Lerp(Color.red, new Color32(35, 159, 30, 255), FindObjectOfType<Jefes>().bateria / SaludJPatasMax);
        ActualizarPatas();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActualizarPatas()
    {
        this.GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, FindObjectOfType<SCRUM_Master>().bateriaPatas / SaludJPatasMax);
    }
}
