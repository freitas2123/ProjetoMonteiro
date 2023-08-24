using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    public float velocidade = 3;
    public float corrida = 5;
    public KeyCode teclaDeCorrida = KeyCode.LeftShift;
    public Animator anim;
    public KeyCode teclaDePular = KeyCode.UpArrow;

    public Rigidbody rb;
    private float velocidadeAtual;
    public float jumpForce;

    public LayerMask Layermask;
    public bool IsGrounded;
    public float GroundCheckSize;
    public Vector3 GrounCheckPosition;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        velocidadeAtual = velocidade;
    }

    void FixedUpdate()
    {
        var inputX = Input.GetAxis("Horizontal");
        var inputZ = Input.GetAxis("Vertical");


        if(inputX != 0 || inputZ != 0)
        {
            transform.Translate(new Vector3(inputX, 0, inputZ) * Time.deltaTime * velocidadeAtual, Space.Self);

            if (Input.GetKey(teclaDeCorrida))
            {
                anim.SetBool("run", true);
                velocidadeAtual = corrida;
            }


        }
        else
        {
            anim.SetBool("run", false);
            velocidadeAtual = velocidade;
        }
        var groundcheck = Physics.OverlapSphere(transform.position + GrounCheckPosition, GroundCheckSize, Layermask);
        if (groundcheck.Length != 0)
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
        anim.SetBool("jump", !IsGrounded);
        if(IsGrounded == true && Input.GetButtonDown("jump"))
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + GrounCheckPosition, GroundCheckSize);
    }
}
