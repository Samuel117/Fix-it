using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
public class Coleccionables
{
    public static void InicializarColeccion()
    {
        bool[] Objet = new bool[3];
        for(int x = 0; x < 3; x++)
        {
            Objet[x] = false;
        }
        Colleccion Objeto = new Colleccion(Objet);
        BinaryFormatter bf = new BinaryFormatter();
        //se guarda en Default/Data
        FileStream stream = new FileStream(Application.persistentDataPath + "/GameData" + "/Coleccionables.FixIt", FileMode.Create);
        Debug.Log("Aqui ando");
        bf.Serialize(stream, Objeto);
        Debug.Log("Guardar Iestado coleccionables.");
        stream.Close();
    }

    public static void GuardarColeccion(bool[] Objet)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/GameData" + "/Coleccionables.FixIt", FileMode.Create);
        Colleccion Objeto = new Colleccion(Objet);

        bf.Serialize(stream, Objeto);

        Debug.Log("Guardar estado coleccionables.");

        stream.Close();
    }

    public static bool[] CargarColeccion()
    {
        if (File.Exists(Application.persistentDataPath + "/GameData" + "/Coleccionables.FixIt"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/GameData" + "/Coleccionables.FixIt", FileMode.Open);

            Colleccion data = bf.Deserialize(stream) as Colleccion;

            stream.Close();

            return data.ObjetosColec;
        }
        else
        {
            Debug.Log("Archivo " + "/GameData" + "/Coleccionables.FixIt" + " inexistente.");
            return new bool[0];
        }
    }

    [Serializable]
    public class Colleccion
    {
        public bool[] ObjetosColec = new bool[3];

        public Colleccion(bool[] _ObjetosColec)
        {
            ObjetosColec[0] = _ObjetosColec[0];//Cristal de datos
            ObjetosColec[1] = _ObjetosColec[1];//Reactor helping
            ObjetosColec[2] = _ObjetosColec[2];//Radar digital

        }
    }
}