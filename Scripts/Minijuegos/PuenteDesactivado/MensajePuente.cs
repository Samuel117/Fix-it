using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MensajePuente : MonoBehaviour
{
    private int ContraNum1, ContraNum2;
    private bool Num1Correcto, Num2Correcto;
    private string Num1, Num2;
    // Start is called before the first frame update
    void Start()
    {
        ContraNum1 = Random.Range(0, 10);
        Num1Correcto = false;
        Num2Correcto = false;
        ContraNum2 = Random.Range(0, 10);
        Debug.Log("Num1: " + ContraNum1 + " y Num2: " + ContraNum2);
        this.transform.Find("Mensaje").GetComponent<TextMeshProUGUI>().text = "ingresa la contraseña correcta";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void verificar(int num1, int num2)
    {
        if(num1 != ContraNum1 && num2 != ContraNum2)
        {
            FindObjectOfType<PuenteDesactivado>().IntentosDentro--;
            if (num1 > ContraNum1)
            {
                Num1 = "Numero de la izquierda es mayor";
            }
            else
            {
                Num1 = "Numero de la izquierda es menor";
            }

            if (num2 > ContraNum2)
            {
                Num2 = "Numero de la derecha es mayor";
            }
            else
            {
                Num2 = "Numero de la derecha es menor";
            }
            this.transform.Find("Mensaje").GetComponent<TextMeshProUGUI>().text = "Tu " + Num1 + " y el " + Num2;
        }
        else if(num1 != ContraNum1 && num2 == ContraNum2)
        {
            FindObjectOfType<PuenteDesactivado>().IntentosDentro--;
            Num2Correcto = true;
            if(num1 > ContraNum1)
            {
                this.transform.Find("Mensaje").GetComponent<TextMeshProUGUI>().text = "Tu numero de la derecha es correcto, pero el de la izquierda es mayor al de la contraseña";
            }
            else
            {
                this.transform.Find("Mensaje").GetComponent<TextMeshProUGUI>().text = "Tu numero de la derecha es correcto, pero el de la izquierda es menor al de la contraseña";
            }
        }else if (num1 == ContraNum1 && num2 != ContraNum2)
        {
            FindObjectOfType<PuenteDesactivado>().IntentosDentro--;
            Num1Correcto = true;
            if (num2 > ContraNum2)
            {
                this.transform.Find("Mensaje").GetComponent<TextMeshProUGUI>().text = "Tu numero de la izquierda es correcto, pero el de la derecha es mayor al de la contraseña";
            }
            else
            {
                this.transform.Find("Mensaje").GetComponent<TextMeshProUGUI>().text = "Tu numero de la izquierda es correcto, pero el de la derecha es menor al de la contraseña";
            }
        }
        else
        {
            Num1Correcto = true;
            Num2Correcto = true;
        }
        if (FindObjectOfType<PuenteDesactivado>().IntentosDentro == 0)
        {
            ContraNum1 = Random.Range(0, 10);
            Num1Correcto = false;
            Num2Correcto = false;
            ContraNum2 = Random.Range(0, 10);
            Debug.Log("Num1: " + ContraNum1 + " y Num2: " + ContraNum2);
            this.transform.Find("Mensaje").GetComponent<TextMeshProUGUI>().text = "ingresa la contraseña correcta";
        }
    }

    public bool Num1Con()
    {
        return Num1Correcto;
    }

    public bool Num2Con()
    {
        return Num2Correcto;
    }
}