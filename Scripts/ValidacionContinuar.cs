using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class ValidacionContinuar : MonoBehaviour
{
    private string NombreCarpeta;
    // Start is called before the first frame update
    void Start()
    {
        NombreCarpeta = "/GameData";
        
        if(!Directory.Exists(Application.persistentDataPath + NombreCarpeta)){
            this.transform.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
