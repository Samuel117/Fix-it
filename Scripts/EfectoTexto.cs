using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EfectoTexto : txtStart
{
    // Start is called before the first frame update
    void Start()
    {
        col = this.GetComponent<TextMeshProUGUI>().color;
        alpha = col.a;
    }

    // Update is called once per frame
    void Update()
    {
        if (esperarFadeColor(esperar))
        {
            FadeColor();
            esperar = Time.time + 0.01f;
        }
    }
}
