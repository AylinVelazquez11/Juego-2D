using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
 
    public float velocidad = 3;
    public float salto = 6;
    public int monedas = 0;
    public int vida = 3;
    bool tocandoSuelo = true;
    Rigidbody2D Player_rb;
    Transform Player_tr;
    Animator Player_anim;
    void Start()
    {

        Player_rb= GetComponent<Rigidbody2D>();
        Player_tr = GetComponent<Transform>();
        Player_anim = GetComponent<Animator>();

        Text contadorVidas = GameObject.FindWithTag("vida").GetComponent<Text>();
        contadorVidas.text = vida + "";


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))//mini funcion que controla el movimineto del player 
        {
            Player_rb.velocity = new Vector2(velocidad, Player_rb.velocity.y);
            Player_tr.rotation =new Quaternion(0,0,0,0);
            Player_anim.SetBool("caminando",true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Player_rb.velocity = new Vector2 (-velocidad, Player_rb.velocity.y);
            Player_tr.rotation = new Quaternion(0,180f,0,0);
            Player_anim.SetBool("caminando", true);

        }
        else
        {
            Player_anim.SetBool("caminando", false);
        }



        if (Input.GetKey(KeyCode.W) && tocandoSuelo) 
        {
            Player_rb.velocity = new Vector2(Player_rb.velocity.x,salto);
            tocandoSuelo = false;
            Player_anim.SetBool("jump", true);
        }
       
        
    }
     private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Piso")
        {
            tocandoSuelo = true;
            Player_anim.SetBool("jump", false);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Moneda")
        {
            monedas++; 
            Destroy(collision.gameObject);
            Text contadorMonedas = GameObject.FindWithTag("ContadorMonedas").GetComponent<Text>();
            contadorMonedas.text = monedas + "";
        }

        if (collision.gameObject.tag == "deadzone")
        {
            Destroy(GameObject.FindWithTag("Player"));

        }
        if (collision.gameObject.tag == "enemigo")
        {
            vida--;
            Text contadorVidas = GameObject.FindWithTag("vida").GetComponent<Text>();
            contadorVidas.text = vida + "";

            if(vida<=0)
            {
                Text opacidadHazMuerto= GameObject.FindWithTag("hasmuerto").GetComponent<Text>();
                Color colorOpacidad= opacidadHazMuerto.color;
                colorOpacidad.a = 1;
                opacidadHazMuerto.color = colorOpacidad;
                Destroy(GameObject.FindWithTag("Player"));
            }

        }
    }






}
