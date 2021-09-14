using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FF : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(FindObjectOfType<Jugador>() != null)
        {
            this.transform.position = new Vector2(FindObjectOfType<Jugador>().transform.position.x, this.transform.position.y);
        }
    }

    private void OnEnable()
    {
        Invoke("Desactivar", 2);
    }

    private void Desactivar()
    {
        this.gameObject.SetActive(false);
    }
}
