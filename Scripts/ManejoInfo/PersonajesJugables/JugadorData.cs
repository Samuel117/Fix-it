 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class JugadorData
{
    public static void GuardarJugador(PlayerData Juga)
    {
        //Lo guarda en default
        BinaryFormatter bf = new BinaryFormatter();
        Directory.CreateDirectory(Application.persistentDataPath + "/GameData/");
        FileStream stream = new FileStream(Application.persistentDataPath + "/GameData/" + Juga.Nombre + ".FixIt", FileMode.Create);
        bf.Serialize(stream, Juga);
        Debug.Log("Guardar Jugador");
        stream.Close();
    }

    //CARGA VALORES DE PERSONAJES JUGABLES.
    public static float[] CargarJugador(string fileName)
    {
        if(File.Exists(Application.persistentDataPath + "/GameData" + fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/GameData" + fileName, FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;

            stream.Close();

            return data.valores;
        }
        else
        {
            Debug.Log("Archivo " + fileName + " inexistente.");
            return new float[0];
        }
    }

    //ACTUALIZA VALORES DE PERSONAJES JUGABLES.
    public static void ActualizarJugador(float[] jugador, string fileName)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/GameData" + fileName, FileMode.Create);

        PlayerData data = new PlayerData(jugador);

        bf.Serialize(stream, data);

        Debug.Log("Guardar Jugador");

        stream.Close();
    }


    //GUARDA INFORMACIÓN DE CONTROL POR DEFECTO.
    public static void GuardarDatosJugador()
    {
        string[] Descripciones = new string[5];
        int[] Precio = new int[5];
        string[] Condicion = new string[5];
        bool[] EstadoDesbloqueo = new bool[5];
        bool[] EstadoCondicion = new bool[5];

        //Skippy
        Descripciones[0] = "Modelo mas común y equilibrado de Robot reparador.";
        //Megabit
        Descripciones[1] = "Modelo aritmetico y veloz de Robot reparador.";
        //Cayde-6
        Descripciones[2] = "Modelo de segunda generación con cualidades autosustentables.";
        //Atom
        Descripciones[3] = "Modelo de tercera generación, mas resistente y mayor alcance.";
        //S-117 
        Descripciones[4] = "Modelo [?] de Robot reparador.";

        Precio[0] = 0;
        Precio[1] = 500;
        Precio[2] = 800;
        Precio[3] = 1000;
        Precio[4] = 1500;

        Condicion[0] = "ninguna es el personaje que te dan";
        Condicion[1] = "Supera el nivel 3";
        Condicion[2] = "Supera el nivel 5";
        Condicion[3] = "Supera el nivel 7";
        Condicion[4] = "Consigue el cristal de datos";

        EstadoDesbloqueo[0] = true;
        EstadoCondicion[0] = true;
        for(int x = 1; x < 5; x++)
        {
            EstadoDesbloqueo[x] = false;//false
            EstadoCondicion[x] = false; //false
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/GameData" + "/GeneralDataPersonajesJugables.FixIt", FileMode.Create);

        PlayerData data = new PlayerData(Descripciones, Precio, Condicion, EstadoDesbloqueo, EstadoCondicion);

        bf.Serialize(stream, data);

        stream.Close();
    }

    //ACTUALIZA LA INFORMACIÓN DE CONTROL.
    public static void ActualizarDatosJugador(PlayerData jugador)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/GameData" + "/GeneralDataPersonajesJugables.FixIt", FileMode.Create);

        bf.Serialize(stream, jugador);

        stream.Close();
    }

    //CARGA INFORMACIÓN DE CONTROL.
    public static PlayerData CargarDatosJugador()
    {
        if (File.Exists(Application.persistentDataPath + "/GameData" + "/GeneralDataPersonajesJugables.FixIt"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/GameData" + "/GeneralDataPersonajesJugables.FixIt", FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Archivo de descripciones inexistente.");
            return null;
        }
    }


    public static void InicializarArchivos()
    {
        Personajes();
        GuardarDatosJugador();
        GeneralPlayerData.InicializarInfo();
        EnemigoData.GuardarEnemigos();
        MinijuegosData.GuardarInfo();
        Coleccionables.InicializarColeccion();
    }

    public static void Personajes()
    {

        PlayerData[] Personajes = new PlayerData[5];
        int x = 0;
        Personajes[x] = new PlayerData(40f, 15f, 1f, 7f, 0.5f, "Skippy");
        x++;
        Personajes[x] = new PlayerData(50f, 10f, 1f, 7f, 0.5f, "Megabit");
        x++;
        Personajes[x] = new PlayerData(60f, 30f, 1f, 10f, 0.5f, "Cayde-6");
        x++;
        Personajes[x] = new PlayerData(70f, 20f, 1f, 5f, 0.5f, "Atom");
        x++;
        Personajes[x] = new PlayerData(30f, 15f, 1f, 10f, 0.5f, "S-117");
        x++;

        for(x = 0; x < 5; x++)
        {
            GuardarJugador(Personajes[x]);
            MejorasData.GuardarInfo(Personajes[x].Nombre + "Mejoras.FixIt");
        }

    }


    [Serializable]
    public class PlayerData{
        public string Nombre;
        public float[] valores;
        public string[] descripciones;
        public int[] precio;
        public string[] condicion;
        public bool[] estadoDesbloqueo;
        public bool[] estadoCondicion;

        //Uso de pruebas de archivos
        public PlayerData(Jugador jugador)
        {
            valores = new float[5];

            valores[0] = jugador.BateriaGetter();
            valores[1] = jugador.DanoGetter();
            valores[2] = jugador.NivelHEGetter();
            valores[3] = jugador.velocidadGetter();
            valores[4] = jugador.CadenciaDisparoGetter();
        }
        //Guarda las variables de los personajes
        public PlayerData(float _Bateria, float _Dano, float _NivelHE, float _Velocidad, float _Cadencia, string _Nombre)
        {
            valores = new float[5];
            valores[0] = _Bateria;
            valores[1] = _Dano;
            valores[2] = _NivelHE;
            valores[3] = _Velocidad;
            valores[4] = _Cadencia;
            Nombre = _Nombre;

        }
        //Guarda las condiciones de los personajes
        public PlayerData(string[] _descripciones, int[] _precio, string[] _condicion, bool[] _estadoDesbloqueo, bool[] _estadoCondicion)
        {
            descripciones = new string[5];
            precio = new int[5];
            condicion = new string[5];
            estadoDesbloqueo = new bool[5];
            estadoCondicion = new bool[5];

            for(int x=0; x < 5; x++)
            {
                descripciones[x] = _descripciones[x];
                precio[x] = _precio[x];
                condicion[x] = _condicion[x];
                estadoDesbloqueo[x] = _estadoDesbloqueo[x];
                estadoCondicion[x] = _estadoCondicion[x];
            }
        }
        //Guarda los valores del jugador
        public PlayerData(float[] _valores)
        {
            valores = new float[5];

            for (int x = 0; x < _valores.Length; x++)
            {
                valores[x] = _valores[x];
            }
        }
    }

}