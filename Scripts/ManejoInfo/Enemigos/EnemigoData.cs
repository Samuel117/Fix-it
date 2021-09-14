using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class EnemigoData
{
    //crea los archivos default del juego de los enemigos
    public static void GuardarEnemigos()
    {
        EnemyData[] enemigo = new EnemyData[8];
        enemigo = Enemigos();
        for(int x = 0; x<enemigo.Length; x++)
        {
            //Se crea un formato binario
            BinaryFormatter bf = new BinaryFormatter();
            //Se pone la ruta especifica y se crea el archivo segun el robot<s
            FileStream stream = new FileStream(Application.persistentDataPath + "/GameData/" + enemigo[x].Nombre + ".FixIt", FileMode.Create);
            //se crea el archivo
            bf.Serialize(stream, enemigo[x]);
            Debug.Log("Guardado con exito enemigo: " + enemigo[x].Nombre);
            //Se cierra el archivo
            stream.Close();
        }
        
    }

    //carga los datos de los enemigos en un arreglo
    public static EnemyData[] CargarDatosEnemigos()
    {
        EnemyData[] datos = new EnemyData[8];
        //Verifica si el archivo existe
        if (File.Exists(Application.persistentDataPath + "/GameData" + "/Peonv1.FixIt"))
        {
            datos[0] = CargarEnemigo("PeonV1");
            Debug.Log("Se cargo con exito: " + datos[0].Nombre);
        }
        else
        {
            Debug.Log("No se cargo con exito");
            datos[0] = null;
        }
        if (File.Exists(Application.persistentDataPath + "/GameData" + "/Peonv2.FixIt"))
        {
            datos[4] = CargarEnemigo("PeonV2");
            Debug.Log("Se cargo con exito: " + datos[4].Nombre);
        }
        else
        {
            Debug.Log("No se cargo con exito");
            datos[4] = null;
        }
        if (File.Exists(Application.persistentDataPath + "/GameData" + "/Peonv3.FixIt"))
        {
            datos[6] = CargarEnemigo("PeonV3");
            Debug.Log("Se cargo con exito: " + datos[6].Nombre);
        }
        else
        {
            Debug.Log("No se cargo con exito");
            datos[6] = null;
        }
        if (File.Exists(Application.persistentDataPath + "/GameData" + "/Wind.FixIt"))
        {
            datos[2] = CargarEnemigo("Wind");
            Debug.Log("Se cargo con exito: " + datos[2].Nombre);
        }
        else
        {
            Debug.Log("No se cargo con exito");
            datos[2] = null;
        }
        if (File.Exists(Application.persistentDataPath + "/GameData" + "/DVDV1.FixIt"))
        {
            datos[3] = CargarEnemigo("DVDV1");
            Debug.Log("Se cargo con exito: " + datos[3].Nombre);
        }
        else
        {
            Debug.Log("No se cargo con exito");
            datos[3] = null;
        }
        if (File.Exists(Application.persistentDataPath + "/GameData" + "/DVDV2.FixIt"))
        {
            datos[7] = CargarEnemigo("DVDV2");
            Debug.Log("Se cargo con exito: " + datos[7].Nombre);
        }
        else
        {
            Debug.Log("No se cargo con exito");
            datos[7] = null;
        }
        if (File.Exists(Application.persistentDataPath + "/GameData" + "/Rotom.FixIt"))
        {
            datos[1] = CargarEnemigo("Rotom");
            Debug.Log("Se cargo con exito: " + datos[1].Nombre);
        }
        else
        {
            Debug.Log("No se cargo con exito");
            datos[1] = null;
        }
        if (File.Exists(Application.persistentDataPath + "/GameData" + "/SCRUM.FixIt"))
        {
            datos[5] = CargarEnemigo("SCRUM");
            Debug.Log("Se cargo con exito: " + datos[5].Nombre);
        }
        else
        {
            Debug.Log("No se cargo con exito");
            datos[5] = null;
        }

        return datos;
    }

    //Carga los datos de un enemigo
    public static EnemyData CargarEnemigo(string Nombre)
    {   
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/GameData/" + Nombre + ".FixIt", FileMode.Open);
        EnemyData data = bf.Deserialize(stream) as EnemyData;

        stream.Close();
        return data;
    }

    public  static EnemyData[] Enemigos()
    {
        //VALORES: Ataque, Bateria, Velocidad de ataque, Velocidad Movimiento, Alcance ataque.
        EnemyData[] enemigos = new EnemyData[8];
        //Peon V1  nivel:1
        enemigos[0] = new EnemyData(valores(10, 40, 6, 7, 7), descripcion("Modelo común de robot reparador defectuoso, ahora solo le importa destruir edificios.", "No esta equipado con habilidad especial."), "PeonV1");
        //Peon V2   nivel:4
        enemigos[4] = new EnemyData(valores(20, 60, 6, 7, 7), descripcion("Versión mejorada del PeonV1, tiene mayor ataque y energia.", "No esta equipado con habilidad especial."), "PeonV2");
        //Peon V3    nivel:6
        enemigos[6] = new EnemyData(valores(20, 70, 10, 7, 7), descripcion("Versión mejorada del PeonV2, tiene mayor ataque y energia.", "No esta equipado con habilidad especial."), "PeonV3");
        //Wind  Nivel:3
        enemigos[2] = new EnemyData(valores(0, 50, 14, 0, 10), descripcion("Robot ventilador cargado con energia negativa, convierte la tranquila brisa en aterradora.", "En lugar de atacar con energia, ataca con aire haciendo retroseder a quien se cruce en su camino."), "Wind");
        //DVD V1   nivel: 4
        enemigos[3] = new EnemyData(valores(30, 80, 6, 7, 7), descripcion("Un reproductor de DVD cargado con energia negativa, convierte los discos en amenazas.", "Aumenta su velocidad al maximo y libera una potente onda de energía al ser desactivado o tocar a su adversario."), "DVDV1");
        //DVD V2   nivel: 9
        enemigos[7] = new EnemyData(valores(35, 80, 6, 7, 7), descripcion("Versión mejorada de DVDv1, cuanta con mayor energía para liberar.", "Aumenta su velocidad al maximo y libera una potente onda de energía al ser desactivado o tocar a su adversario, ademas de dejar un rastro de energia que causa daño constante."), "DVDV2");
        //Rotom  Nivel:2
        enemigos[1] = new EnemyData(valores(5, 50, 14, 5, 4), descripcion("Una imitación de un robot de Pandora que ataca con rayos de energia negativa.", "Su ataque causa un ligero corto en los sistemas de su oponente disminuyendo su velocidad."), "Rotom");
        //SCRUM   Nivel:5
        enemigos[5] = new EnemyData(valores(40, 100, 10, 7, 7), descripcion("Un robot en forma de araña, altamente peligroso.", "Usa sus habilidades robo-aracnidas para crear una telaraña artificial, la cual atrapa y drena la energía de otros robots."), "SCRUM");

        return enemigos;
    }
    //Inicializa los valores de los enemigos
    public static float[] valores(float dano, float bateria, float velocidadAtaque, float velocidadmovimiento, float alcance)
    {
        float[] valores = new float[5];
        int x = 0;
        valores[x] = dano;
        x++;
        valores[x] = bateria;
        x++;
        valores[x] = velocidadAtaque;
        x++;
        valores[x] = velocidadmovimiento;
        x++;
        valores[x] = alcance;

        return valores;
    }

    //inicializa las descripciones de los enemigos
    private static string[] descripcion(string descripcionRobot, string descripcionHabilidad)
    {
        string[] descripcion = new string[2];
        int x = 0;
        descripcion[x] = descripcionRobot;
        x++;
        descripcion[x] = descripcionHabilidad;
        return descripcion;
    }

    [Serializable]
    public class EnemyData
    {
        public float[] Valores;
        public string[] Descripciones;
        public string Nombre;

        //constructor principal
        public EnemyData(float[] _Valores, string[] _Descripciones, string _Nombre)
        {
            Valores = new float[5];
            Descripciones = new string[2];
            for(int x = 0; x< Valores.Length; x++)
            {
                Valores[x] = _Valores[x];
                if (x < Descripciones.Length)
                {
                    Descripciones[x] = _Descripciones[x];
                }
            }
            Nombre = _Nombre;
        }

        //constructor vacio
        public EnemyData()
        {

        }
    }

}