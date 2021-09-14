using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AparicionMinijuegos  : MonoBehaviour
{
    [SerializeField] private GameObject[] Minijuegos;
    [SerializeField] private int CantidadCol;
    [SerializeField] private int CantidadFila;
    private GameObject[,] ArregloMinijuegos;
    // Start is called before the first frame update
    void Start()
    {
        int Min = 0;
        ArregloMinijuegos = new GameObject[CantidadCol,CantidadFila];
        for(int x = 0; x < CantidadCol; x++)
        {
            for(int y = 0; y < CantidadFila; y++)
            {
                ArregloMinijuegos[x, y] = Minijuegos[Min];
                Min++;
            }
        }
        for(int x = 0; x < CantidadCol; x++)
        {
            Inicializar(Random.Range(1, 4), x);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Inicializar(int Variable, int Col)
    {
        switch (Variable)
        {
            case 1:
                ArregloMinijuegos[Col, 0].transform.gameObject.SetActive(true);
                break;
            case 2:
                ArregloMinijuegos[Col, 1].transform.gameObject.SetActive(true);
                break;
            case 3:
                ArregloMinijuegos[Col, 0].transform.gameObject.SetActive(true);
                ArregloMinijuegos[Col, 1].transform.gameObject.SetActive(true);
                break;
        }
    }
}
