using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorFF : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Desactivar()
    {
        FindObjectOfType<Ganon>().transform.Find("GeneradorFF").gameObject.SetActive(false);
    }
}
