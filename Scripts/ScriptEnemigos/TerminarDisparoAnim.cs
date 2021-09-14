using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminarDisparoAnim : MonoBehaviour
{
    public void TerminarDisparo()
    {
        transform.parent.gameObject.GetComponent<Enemigo>().disparando = false;
    }
}
