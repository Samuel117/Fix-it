using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reparado : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.one * 2.5f;   
    }
}
