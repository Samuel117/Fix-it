using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class MinijuegosData
{
    public static void GuardarInfo()
    {
        string fileName = "Minijuegos.FixIt";
        string[] Descripciones = new string[18] {
        "Hay un extenso fuego que impide el paso, deberá ser apagado para despejar el camino.",//1
        "Una tubería rota que expulsa agua a alta presión que impide el paso, si intentas pasar el agua te empujara!.",//2
        "Escombro caído desde arriba del edificio, es muy grande por lo que debe ser reducirlo a polvo para poder continuar.",//3
        "Escalera exterior que permite subir a la siguiente sección del edificio, deben repararse ya que no existe otro camino.",//4
        "Puente desactivado por los robots desactivados.",//5
        "Elevador que permite continuar a la siguiente sección, es él único camino por lo que debe ser reparado.",//6
        "La estructura central está comprometida y si no se repara es posible que colapse todo el edificio.",//7
        "sistema averiado que genera humo, se debe reparar para despejar el camino.",//8
        "Un generador de alto voltaje que da energía a la estructura por lo que no se puede subir hasta que sea reparado.",//9
        "Sistema de protección del edificio que ha sido hackeado por los robots defectuosos para impedir tu avance, debes repararlo de forma cautelosa para evitar llamar la atención.",//10
        "Único camino para subir que se encuentra sellado por los robots defectuosos para frenar tu avance.",//11
        "Un colega que ha sido desactivado y necesita de tu ayuda para volver a andar. Es opcional si deseas ayudarlo.",//12
       "Generador que permite que grandes estructuras floten en el aire, sin él la estructura se desploma, es prioritario que sea reparado.",//10
        "Generador que permite que grandes estructuras floten en el aire, sin él la estructura se desploma, es prioritario que sea reparado.",//11
        "Generador que permite que grandes estructuras floten en el aire, sin él la estructura se desploma, es prioritario que sea reparado.",//12
        "Campo de seguridad que impide que objetos entren o salgan, debes desactivarlo para poder salir y continuar tu camino.",//16
        "Lámpara que ilumina el camino, sin ella no se puede seguir subiendo porque es demasiado oscuro.",//17
         "Helicóptero no tripulado dañado por los robots defectuosos, se repara para despejar el camino.",//18
        };
        string[] Reparar = new string[18] {
        "Se debe detener la flecha 3 veces en la sección blanca usando la tecla espaciadora.",//1
        "Supera la fuerza del agua presioando multiples veces las teclas que se indiquen en pantalla.",//2
        "Guia el taladro usando las flechas para dirigir el taladro y la tecla espaciadora para acelerar, llegar al nucleo del escombro evitando todos los obstaculos.",//3
        "Presta atenión y memoriza las letras en pantalla, replica la secuencia cuando se te indique.",//4
        "Encontrar la contraseña de 2 digitos, usa las pistas para saber si tu número es mayor o menor a la contraseña correcta.",//5
        "Usa las flechas para rotar el camino y guiar la energía por el camino correcto."//6
       ,"Usa el click izquierdo para reparar las gritas manteniendo presioando sobre las secciones dañadas."//7
       ,"Presiona los focos del mismo color al foco de control, el cual se encuntra en la parte superior."//8
       ,"De acuerdo a los dobleces de los cables, selecciona el cable que lleve la energía mas rapido a la parte inferior del generador."//9
       ,"Arrastra la esfera manteniendo el click izquierdo sobre ella, sin salir de los bordes del camino."//10
       ,"Presina las letas resaltadas cuando lleguen a la sección blanca."//11
       ,"Replica la secuencia de luces que se muestra en pantalla."//12
       ,"Se mostrará una serie de teclas que deben ser presionadas en el momento indicado que es cuando alcanzan el punto mostrado en pantalla."//10
       ,"Arrastra las piezas del cristal fracturado usando el click izquierdo en el orden correcto al centro de la pantalla."//11
       ,"Une los vertices y crea la figura que se encuetra debajo de cada hexagono."//12
       ,"Alinea las secciones resaltadas de los discos con la sección inferior utilizando las flechas para seleccioanar y rotar cada disco."//16
       ,"Usa las flechas para girar y apoyar a las lamapras contra la energía negativa, desactiva todos los generadores negativos para ganar."//17
       ,"Usa la mira con el click izquierdo para atacar y  desactivar a las robomoscas que pasan volando."//18
        };
        string[] Nombre = new string[18] {
        "Incendio",//1
            "Tubería Dañada",//2
            "Escombro bloqueando el camino",//3
            "Escalera rota",//4
            "Puente Desactivado",//5
            "Elevador exterior descompuesto",//6
            "Muro fracturado",//7
            "Sistema de control de clima",//8
            "Generador eléctrico averiado",//9
            "Sistema de seguridad Hackeado",//10
            "Puerta sellada",//11
            "Robot reparador desactivado",//12
            "Generador anti-gravitatorio (Etapa 1)",//10
            "Generador anti-gravitatorio (Etapa 2)",//11
            "Generador anti-gravitatorio (Etapa 3)",//12
            "Campo de energía",//16
            "Lámpara desactivada",//17
            "Helicóptero estrellado no tripulado",//18
        };

        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/GameData/" + fileName, FileMode.Create);

        MinijugosInfo data = new MinijugosInfo(Nombre, Descripciones, Reparar);

        bf.Serialize(stream, data);

        Debug.Log("Guardar Minijuegos.");

        stream.Close();
    }

    public static MinijugosInfo CargarInfo()
    {
        string fileName = "Minijuegos.FixIt";

        if (File.Exists(Application.persistentDataPath + "/GameData/" + fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/GameData/" + fileName, FileMode.Open);

            MinijugosInfo data = bf.Deserialize(stream) as MinijugosInfo;

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
    public class MinijugosInfo
    {
        public string[] nombre = new string[18];
        public string[] descripciones = new string[18];
        public  string[] reparar = new string[18];

        public MinijugosInfo(string[] _nombre, string[] _descripcion, string[] _reparar)
        {
            nombre = _nombre;
            descripciones = _descripcion;
            reparar = _reparar;
        }
    }
}
