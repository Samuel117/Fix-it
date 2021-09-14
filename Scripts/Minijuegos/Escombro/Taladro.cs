using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taladro : MonoBehaviour
{
    public int vidas = 3;
    int speed = 180;
    int rotationSpeed = 100;
    public Vector2 pos;

    public bool cancelarMov = false;
    public float esperar = 0f;

    [SerializeField] GameObject camino;
    [SerializeField] GameObject origen;

    [SerializeField] public AudioSource player;
    [SerializeField] AudioClip taladro;
    [SerializeField] AudioClip apagado;
    [SerializeField] public AudioClip victoria;
    [SerializeField] AudioClip derrota;
    [SerializeField] AudioClip dano;

    private float esperarCamino = 0f;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.rotation = Quaternion.Euler(0,0,180);
        pos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();

        //if (!player.isPlaying)
        //{
        //    player.PlayOneShot(taladro);
        //}

        if (cancelarMov && ReactivarMov(esperar))
        {
            FindObjectOfType<Taladro>().cancelarMov = false;
            FindObjectOfType<Taladro>().gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }


        if (vidas <= 0)
        {
            player.PlayOneShot(derrota);
            Debug.Log("Perder por vidas");
            FindObjectOfType<Escombro>().Derrota();
        }
    }

    public void RecibirDano()
    {
        player.PlayOneShot(dano);
        vidas--;
    }

    private void  Movimiento()
    {
        float deltaTime = Time.deltaTime;

        float horizontalInput = Input.GetAxis("Vertical");
        if (horizontalInput != 0f && Input.GetKey(KeyCode.Space) && !cancelarMov)
        {
            float deltaMovement = horizontalInput * speed * deltaTime;
            this.transform.Translate(new Vector3(deltaMovement, 0f, 0f), Space.Self);
            //this.transform.position += transform.right * horizontalInput * 2f;

            if (debeEsperarCamino(esperarCamino))
            {
                Instantiate(camino, this.transform.position, this.transform.rotation, origen.gameObject.transform);
                 esperarCamino = Time.time + 0.05f;
            }
        }

        bool isTurningLeft = Input.GetKey(KeyCode.RightArrow);
        bool isTurningRight = Input.GetKey(KeyCode.LeftArrow);

        if (isTurningLeft && !isTurningRight)
        {
            if (debeEsperarCamino(esperarCamino))
            {
                Instantiate(camino, this.transform.position, this.transform.rotation, origen.gameObject.transform);
                esperarCamino = Time.time + 0.05f;
            }

            this.transform.Rotate(Vector3.back * rotationSpeed * deltaTime, Space.World);
        }

        else if (!isTurningLeft && isTurningRight)
        {
            if (debeEsperarCamino(esperarCamino))
            {
                Instantiate(camino, this.transform.position, this.transform.rotation, origen.gameObject.transform);
                esperarCamino = Time.time + 0.05f;
            }

            this.transform.Rotate(Vector3.forward * rotationSpeed * deltaTime, Space.World);
        }
    }

    private bool debeEsperarCamino(float esperar)
    {
        return Time.time > esperar;
    }

    public bool ReactivarMov(float esperar)
    {
        return Time.time > esperar;
    }
}
