using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AparicionColeccion : MonoBehaviour
{
    [SerializeField] int Coleccion;
   
    // Start is called before the first frame update
    void Start()
    {
        if(int.Parse(GeneralPlayerData.CargarInfo()[0]) == 10 && !Coleccionables.CargarColeccion()[Coleccion])
        {
            ActivarRobot(Random.Range(1, 4));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ActivarRobot(int dato)
    {
        switch (dato)
        {
            case 1:
                this.transform.Find("Robot_Coleccion").transform.gameObject.SetActive(true);
                break;
            case 2:
                this.transform.Find("Robot_Coleccion_1").transform.gameObject.SetActive(true);
                break;
            case 3:
                this.transform.Find("Robot_Coleccion_2").transform.gameObject.SetActive(true);
                break;
        }
    }
}
