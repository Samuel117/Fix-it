using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroshokBase : MonoBehaviour
{
    private float lifeSpam = 15f;

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
        Invoke("Desactivar", lifeSpam);
    }

    private void Desactivar()
    {
        this.gameObject.SetActive(false);
    }
}
