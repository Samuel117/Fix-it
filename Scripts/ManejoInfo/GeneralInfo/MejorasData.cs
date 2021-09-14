using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class MejorasData 
{
    public static void GuardarInfo( string fileName)
    {
        PlayerData data;

        string[] SkippyHB = new string[3] {
            "Lanza una bola de energía más grande que resta el doble de energía que su ataque básico, su tiempo de enfriamiento es de 30 segundos.",
            "Lanza una bola de energía más grande que resta el doble de energía que su ataque básico, su tiempo de enfriamiento es de 20 segundos.",
            "Lanza una bola de energía más grande que resta el doble de energía que su ataque básico y recuperara 15 puntos de su batería, su tiempo de enfriamiento es de 20 segundos."
        };
        
        string[] MegabitHB = new string[3] {
            "Sobrecarga que potencia las  bolas de energía, estas restarán 50 puntos de energía durante 10 segundos, esta habilidad se podrá usar 1 vez por partida.",
            "Sobrecarga que potencia las  bolas de energía, estas restarán 50 puntos de energía durante 15 segundos, esta habilidad se podrá usar 1 vez por partida.",
            "Sobrecarga que potencia las  bolas de energía, estas restarán 50 puntos de energía durante 15 segundos, esta habilidad se podrá usar 2 vez por partida."
        };
      
        string[] Cayde6HB = new string[3] {
            "Regenera batería constantemente siempre y cuando no reciba ataques y no se mueva, recuperará 5 puntos de batería por segundo.",
            "Regenera batería constantemente siempre y cuando no reciba ataques, ahora puede moverse, recuperará 5 puntos de batería por segundo.",
            "Regenera batería constantemente siempre y cuando no reciba ataques, ahora puede moverse, recuperará 5 puntos de batería por segundo, en caso de ser desactivado podra recargar 1 vez la mitad de la batería nuevamente."
        };
        string[] AtomHB = new string[3] {
            "Sobrecarga el sistema causando un aumento momentáneo a la velocidad de ataque durante 10 segundos, puede hacerlo cada 35 segundos.",
            "Sobrecarga el sistema causando un aumento momentáneo a la velocidad de ataque durante 15 segundos, puede hacerlo cada 35 segundos.",
            "Sobrecarga el sistema causando un aumento momentáneo a la velocidad de ataque durante 15 segundos, puede hacerlo cada 25 segundos."
        };
        string[] S117HB = new string[3] {
            "Avanzado escudo del futuro del año 2552 que se ve cómo una segunda batería con 25 puntos de energía que se regenera mientras S-117 no reciba ataques.",
            "Avanzado escudo del futuro del año 2552 que se ve cómo una segunda batería con 50 puntos de energía que se regenera mientras S-117 no reciba ataques.",
            "Avanzado escudo del futuro del año 2552 que se ve cómo una segunda batería con 75 puntos de energía que se regenera mientras S-117 no reciba ataques."
        };

        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/GameData/" + fileName, FileMode.Create);
        

        switch (fileName)
        {
            case "SkippyMejoras.FixIt":
                {
                    //Skippy limites.
                     data = new PlayerData(SkippyHB, 30, 60);
                }
                break;

            case "MegabitMejoras.FixIt":
                {
                    //Megabit limites.
                    data = new PlayerData(MegabitHB, 35, 70);
                }
                break;

            case "Cayde-6Mejoras.FixIt":
                {
                    //Cayde-6 limites.
                    data = new PlayerData(Cayde6HB, 45, 80);
                }
                break;

            case "AtomMejoras.FixIt":
                {
                    //Atom limites.
                     data = new PlayerData(AtomHB, 35, 90);
                }
                break;

            case "S-117Mejoras.FixIt":
                {
                    //S-117 limites.
                    data = new PlayerData(S117HB, 50, 100);
                }
                break;

            default:
                {
                    data = new PlayerData(SkippyHB, 0, 0);
                }
                break;
        }

        bf.Serialize(stream, data);

        Debug.Log("Guardar Infomración de Mejoras.");

        stream.Close();


    }

    public static PlayerData CargarInfo(string fileName)
    {
        if (File.Exists(Application.persistentDataPath + "/GameData" + fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/GameData" + fileName, FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Archivo " + fileName + " inexistente.");
            return null;
        }
    }

    [Serializable]
    public class PlayerData
    {
        public string[] descripcionesHE = new string[3];
        public int AtaqueMaximo, EnergiaMaxima;

        public PlayerData(string[] _descripcionHE, int _AtaqueMaximo, int _EnergiaMaxima)
        {
           for(int x = 0; x < _descripcionHE.Length; x++)
            {
                descripcionesHE[x] = _descripcionHE[x];
            }
            
            AtaqueMaximo = _AtaqueMaximo;
            EnergiaMaxima = _EnergiaMaxima;
        }

    }
}