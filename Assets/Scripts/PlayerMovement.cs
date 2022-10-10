using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    #region var definitions

    [Header("Movement")]
    [SerializeField] private float maxMoveSpeed = 4.5f;
    //[SerializeField] private float minMoveSpeed = 0.5f;
    [SerializeField] private float gravity;
    float direction;
    float moveSpeed = 5.5f;

    [Header("Jumping")]
    [SerializeField] private float maxJumpHeight = 2.5f;
    //[SerializeField] private float minJumpHeight = 4f;
    [SerializeField] private float timeToJumpApex = .6f;
    [SerializeField] private int maxJumps = 1;
    float jumpForce;
    int jumpCounter;
    
    [Header("Player")]

    public static PlayerMovement instance;
    Rigidbody2D body;


    private void Awake()
    {
        instance = this;
    }

    #endregion


    private void Start()
    {
        jumpCounter = 0;
        body = GetComponent<Rigidbody2D>();

        gravity = -(2 * maxJumpHeight) / timeToJumpApex;
        jumpForce = (Mathf.Abs(gravity) * timeToJumpApex);

    }


    void Update()
    {
        moveSpeed = Mathf.MoveTowards(moveSpeed, maxMoveSpeed, Time.deltaTime);
        GetInput();




        ApplyMovement();
        GroundedCheck();
    }

    private void GroundedCheck()
    {
        jumpCounter = body.velocity.y == 0 ? 0 : 1;
    }

    private void GetInput()
    {

        direction = Input.GetAxis("Horizontal");

    }

    private void ApplyMovement()
    {
        //body.velocity = new Vector2(direction * moveSpeed, 0);//Vector2.right * direction * moveSpeed;
        transform.Translate(Vector3.right * direction * Time.deltaTime * moveSpeed);

        if (Input.GetKeyDown(KeyCode.Space) && jumpCounter < maxJumps)
        {
            jumpCounter++;
            body.AddForce(Vector2.up * body.gravityScale * jumpForce, ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        /*if (col.gameObject.CompareTag(Globals.GROUND_TAG))
        {
            Vector2 pointOfContact = col.contacts[0].normal;
            if ((pointOfContact == new Vector2(0, 1) && body.gravityScale > 0) || (pointOfContact == new Vector2(0, -1) && body.gravityScale < 0))
            { 
                jumpCounter = 0;
            }
            
        }*/
    }

}
