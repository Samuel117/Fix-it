using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Letra_Alea : MonoBehaviour
{
    private string LetraRandom;
    // Start is called before the first frame update
    void Start()
    {
        GenerarLetra();
    }

    // Update is called once per frame
    void Update()
    {
        MoverLetra();
        Destruir();
    }

    private void GenerarLetra()
    {
        LetraRandom = ((char)('A' + Random.Range(0, 4))).ToString();
        this.transform.GetComponent<TextMeshProUGUI>().text = LetraRandom;
    }

    private void MoverLetra()
    {
        this.transform.GetComponent<Rigidbody2D>().velocity = new Vector2(this.transform.GetComponent<Rigidbody2D>().velocity.x, -300f);

    }

    private void Destruir()
    {
        if(this.transform.position.y <= 95)
        {
            Destroy(this.gameObject);
        }
    }
}
