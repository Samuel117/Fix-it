using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagono : MonoBehaviour
{
    [SerializeField] GameObject Barra;
    [SerializeField] GameObject Padre;
    private int BarrasCorrectas = 0;
    public Vector2 origen, destino;

    [SerializeField] private int victoria;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(BarrasCorrectas == victoria)
        {
            FindObjectOfType<Puntero>().Origen = true;
            FindObjectOfType<Puntero>().Destino = false;
            BarrasCorrectas++;
            FindObjectOfType<Generador_Gravitatorio>().FigurasCorrectas++;
        }
    }

    public void GenerarBarra()
    {
        float largo = origen.x - destino.x;
        //Vector2 Tamaño = new Vector2(Mathf.Abs(largo), 17);
        Vector2 Tamaño = new Vector2(Vector2.Distance(origen,destino), 17);
        Barra.GetComponent<RectTransform>().sizeDelta = Tamaño;
        Instantiate(this.Barra, origen, Quaternion.Euler(0, 0, AngleBetweenVector2(origen, destino)), Padre.gameObject.transform);
    }

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }

    public bool V2Equal(Vector2 a, Vector2 b)
    {
        return Vector2.SqrMagnitude(a - b) < 0.00000001;
    }

    public void Origen(Vector2 posicion)
    {
        origen = posicion;
        //Debug.Log(origen);
    }

    public void Destino(Vector2 posicion)
    {
        destino = posicion;
        //Debug.Log(destino);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<BarraBarra>() != null)
        {
            BarrasCorrectas++;
        }
    }
}
