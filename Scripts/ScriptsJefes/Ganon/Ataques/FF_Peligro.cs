using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FF_Peligro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<Ganon>().gameObject.transform.Find("GeneradorFF").gameObject.activeSelf == false)
        {
            this.gameObject.SetActive(false);
        }
    }
}
