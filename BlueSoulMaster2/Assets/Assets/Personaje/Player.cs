using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Con esto identifico los objetos que estén en una layer especifica, ene ste caso las plataformas
    [SerializeField]
    private LayerMask platformLaterMask;

    //Referencias a componentes
    SpriteRenderer PlayerR;
    Animator AniR;
    Rigidbody2D player_Rigid;
    BoxCollider2D player_collider;

    //Variables de velocidad/salto
    public float jumpVelocity;
    float speed;

    //Estoy en modo dash
    bool dashing;

    //Variables para la velocidad/tiempo del dash
    public float dashSpeed;
    public float dashTime;
    public float startDashTime;
    private int dashDirection;

    void Start()
    {
        //inicializo variables
        //startDashTime es una variable de referencia para que dash time siempre
        //tenga a donde ir cuando llegué a 0
        dashTime = startDashTime;
        dashing = false;
        speed = 15f;
        PlayerR = GetComponent<SpriteRenderer>();
        AniR = GetComponent<Animator>();
        player_Rigid = GetComponent<Rigidbody2D>();
        player_collider = GetComponent<BoxCollider2D>();
    }

    private bool IsGrounded()
    {
        //Con esta función se cntrola que el jugador haya tocado el sueño antes de  querer saltar de nuevo
        //Casteando un rayo que va en dirección hacia abajo desde el centro del jugador
        RaycastHit2D ray = Physics2D.BoxCast(player_collider.bounds.center, player_collider.bounds.size, 0f, Vector2.down , .1f,platformLaterMask);
        return ray.collider != null;
    }

    void Movement()
    {
        //Movimiento con keys
        //EN este caso la velocidad del jugador ya esta basada dentro del rigidbody2d
        //que tiene el personaje en sus componentes, de está manera es más fácil
        //lidiar con interacciones futuras
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                PlayerR.flipX = true;
                player_Rigid.velocity = new Vector2(-speed, player_Rigid.velocity.y);
            }
            else
            {
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    PlayerR.flipX = false;
                    player_Rigid.velocity = new Vector2(+speed, player_Rigid.velocity.y);
                }
                else
                {
                    player_Rigid.velocity = new Vector2(0, player_Rigid.velocity.y);
                }
            }
    }

    void Animations()
    {
        //cambio de animaciones
        //Básicamente son las condiciones que estan en el animator
        if (IsGrounded())
        {
            if (player_Rigid.velocity.x == 0)
            {
                AniR.SetBool("Walk", false);
                AniR.SetBool("Jump", false);
                AniR.SetBool("Dash", false);
            }
            else
            {
                AniR.SetBool("Walk", true);
            }
        }
        else
        {
            AniR.SetBool("Jump", true);
        }
        if (player_Rigid.velocity.y == 0)
        {
            AniR.SetBool("Jump", false);
        }
        if (dashing)
        {
            AniR.SetBool("Dash", true);
        }else
        {
            AniR.SetBool("Dash", false);
        }
             
    }

    void Jump()
    {
        //checo si esta en el suelo y si presiono alguna tecla de salto
        if (IsGrounded() && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            player_Rigid.velocity = Vector2.up * jumpVelocity;
        }
    }
 
    void Dash()
    {
        //Checo que no esté dasheando y que se presiona space
        if(!dashing && Input.GetKeyDown(KeyCode.Space))
        {
            //al iniciar el dash, evito que el código vuelva a ingresar a esta parte
            //desactivo el collider, ya que queremos que el jugador pueda pasar
            //por paredes en esté estado, por lo que tambipen le quitamos que sea
            //afectado por la gravedad
            dashing = true;
            player_collider.enabled = false;
            player_Rigid.gravityScale = 0.0f;

            //checo en que dirección es el dash
            if (PlayerR.flipX)
            {
                //left
                player_Rigid.velocity = Vector2.left * dashSpeed;
            }
            else
            {
                //right
                player_Rigid.velocity = Vector2.right * dashSpeed;
            }
        }

        //Si estoy en modo dash, empieza mi tiempo para realizar esa acción
        //una vez llegado dashtime a 0, reinicializo los valores a su estado normal
        if(dashing)
        {
            if(dashTime<= 0)
            {
                dashing = false;
                player_collider.enabled = true;
                player_Rigid.gravityScale = 9.0f;
                dashTime = startDashTime;
                player_Rigid.velocity = Vector2.zero;
            }
            else {
            
                dashTime -= Time.deltaTime;
            }
        }
    }
    void Update()
    {
        Dash();

        if (!dashing)
        {
            Movement();
            Jump();
        }

        Animations();
       
       

      /*  //movimiento con fisicas
        if(Input.GetKey(KeyCode.D)) //al presionar D
        {
            if(PlayerR.flipX==true)//Verifica si flipeo esta en true
                PlayerR.flipX = false;//Si esta en True lo pasa a falso
            AniR.SetBool("Walk", true);//Activa la animacion de moviemiento de Caminar
            transform.Translate(0.05f, 0, 0);//Es cuanto se va a mover
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (PlayerR.flipX == false)
                PlayerR.flipX = true;
            AniR.SetBool("Walk", true);
            transform.Translate(-0.05f, 0, 0);
        }
        
        if(Input.GetKey(KeyCode.W))
        {
            AniR.SetBool("Jump", true);
            transform.Translate( 0, 0.06f, 0);
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.W))
        {
            AniR.SetBool("Walk", false);
            AniR.SetBool("Jump", false);
        }
        */
    }//update


    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "ground")
        {
            canJump = true;
        }
    }//oncolli*/
    

}//class
