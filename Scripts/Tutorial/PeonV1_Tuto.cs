using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeonV1_Tuto : Enemigo
{
    // Start is called before the first frame update
    void Start()
    {
        Matdefault = sr.material;
        bateria = 40f;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Find("Enemigo_1").gameObject.GetComponent<Animator>().SetBool("Idle", true);
    }

    public override void RecibirDano(float danoRecibido)
    {
        this.bateria = this.bateria - danoRecibido;

        EfectoRecibirDano();
        Invoke("TerminarEfectoRecibirDano", 0.1f);

        if (this.bateria <= 0)
        {
            Instantiate(Desactivado, new Vector2(this.transform.position.x, this.transform.position.y + 1f), this.transform.Find("Enemigo_1").gameObject.transform.rotation);
            this.Destruir();
        }

        GirarAlRecibirDaño();
    }
}
