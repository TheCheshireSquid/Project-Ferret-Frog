using System.Collections;
using System.Collections.Generic;

using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.TextCore;

public class PlayerScript : MonoBehaviour
    //idamageable
{
    Controls controls;

    Controls.PlayerActions actions;
    Rigidbody2D rb;

    //private float boots()
    //{
    //    float boots = 2f;
    //    return boots;
    //}

    //health function?
    [SerializeField] int hp;
    public int hpp;
    public void TakeDamage(int Damage)
    {
        hp -= Damage;
        //updateHP(); 
    }
       



    [SerializeField] float speed;
    [SerializeField] float acceleration;
    [SerializeField] float decceleration;


    [SerializeField] float gravity;
    [SerializeField] float jumpForce;

    [SerializeField] LayerMask mask;

    [SerializeField] SpriteRenderer spr;

    [SerializeField] Animator animator;


    [SerializeField] float hitDistance;


    // private member variables
    [SerializeField] bool grouned;

    [SerializeField] public bool hasDash;


    [SerializeField] public int jumpNumber;

    Vector3 point;
    public Vector3 input;

    int facing, lastFace;

    [SerializeField] float groundAngle = 45;
    float groundDot;
    int groundCount;

    public bool Grounded => groundCount > 0;


    private void OnValidate()
    {
        groundDot = Mathf.Cos(groundAngle * Mathf.Deg2Rad);
    }



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new Controls();
        controls.Player.Enable();
        actions = controls.Player;



    }






    private void FixedUpdate()
    {

        PreMovement();


        Movement();

        ClearState();
    }


    void PreMovement()
    {
        input = actions.MoveAxis.ReadValue<Vector2>();

        if (input.x == 0)
        {
            animator.Play("Idle");
        }
        else
        {
            animator.Play("Walk");
        }

        facing = input.x > 0 ? 1 : input.x < 0 ? -1 : lastFace;


        spr.flipX = facing < 0 ? true : false;


        var r = Physics2D.Raycast(transform.position, Vector2.down, 1f, mask);

        if (r)
        {
            var b = r.distance < 0.1f;

            grouned = b ? true : grouned;


        }

        if (grouned)
        {
            jumpNumber= 0;
        }


        // attack


        if(controls.Player.Attack.WasPressedThisFrame())
        {




            Vector2 dir = new(facing, 0);

            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position + Vector3.up * 0.2f, 2, dir);

            foreach(var hit in hits)
            {
                if(hit.distance <= hitDistance)
                {

                    var objectHit = hit.collider.gameObject.GetComponent<IHittable>();

                    objectHit?.TakeHit(10f);





                }


            }


        }

    
    }

    void Movement()
    {
        var desVel = actions.MoveAxis.ReadValue<Vector2>() * speed;

        var vel = rb.velocity;


        var a = Mathf.Abs(vel.x) > Mathf.Abs(vel.x) ? acceleration : decceleration;

        var accel = a * Time.deltaTime;



        vel.x = Mathf.MoveTowards(vel.x, desVel.x, accel);


        //if (Grounded && actions.Jump.WasPressedThisFrame())
        //{
        //    vel.y = jumpForce;
        //    animator.Play("Jump");
        //}

        if (actions.Jump.WasPressedThisFrame() && (Grounded || jumpNumber < 2)) 
        {
            jumpNumber ++; 
            vel.y = jumpForce;
            animator.Play("Jump");
        }



        if (!Grounded)
        {
            vel.y += - gravity * Time.deltaTime;
        }


        rb.velocity = vel;


        //if (Player = Frog_item)
        //{
        //    Frog();
        //}

    }

    void ClearState()
    {
        lastFace = facing != 0 ? facing : lastFace;

        groundCount = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EvalCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EvalCollision(collision);
    }

    void EvalCollision(Collision2D collision)
    {
        for(int i = 0; i < collision.contactCount; i++)
        {
            Vector3 normal = collision.GetContact(i).normal;
            if(normal.y > groundDot)
            {
                groundCount++;
            }
        }
    }
   
    //This is Wolf's Frog Code {<----TOM}

    //void Player()
    //{
    //    double Player_Height = 0.3468;
    //    double Player_Width = 0.00674;
    //    float Player_Speed = 8f;
    //    float Player_Jump = 6f;
    //}
    //void Frog()
    //{
    //    Player();
    //    double Frog_height = Player_Height / 2;
    //    double Frog_width = Player_Width / 2;
    //    float Frog_speed = Player_Speed;
    //    float Frog_jump = Player_Jump;

    //}


}
