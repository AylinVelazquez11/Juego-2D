using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoveJoystick : MonoBehaviour
{
    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    public Joystick joystick;
    public float runSpeedHorizontal = 3;
    public float velocidad = 3;
    public float salto = 6;
    public int monedas = 0;
    bool tocandoSuelo = true;
    Rigidbody2D Player_rb;
    Transform Player_tr;
    Animator Player_anim;
    void Start()
    {
        Player_rb = GetComponent<Rigidbody2D>();
        Player_tr = GetComponent<Transform>();
        Player_anim = GetComponent<Animator>();
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        horizontalMove = joystick.Horizontal * runSpeedHorizontal;
        Player_tr.position += new Vector3(horizontalMove, 0, 0) * Time.deltaTime * velocidad;
    }


    void Update()
    {
        if (horizontalMove>0)//mini funcion que controla el movimineto del player 
        {
            Player_rb.velocity = new Vector2(velocidad, Player_rb.velocity.y);
            Player_tr.rotation = new Quaternion(0, 0, 0, 0);
            Player_anim.SetBool("caminando", true);
        }
        else if (horizontalMove<0)
        {
            Player_rb.velocity = new Vector2(-velocidad, Player_rb.velocity.y);
            Player_tr.rotation = new Quaternion(0, 180f, 0, 0);
            Player_anim.SetBool("caminando", true);

        }
        else
        {
            Player_anim.SetBool("caminando", false);
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
    }

    public void Jump()
    {
        Player_rb.velocity = new Vector2(Player_rb.velocity.x, salto);
        tocandoSuelo = false;
        Player_anim.SetBool("jump", true);
    }

}
