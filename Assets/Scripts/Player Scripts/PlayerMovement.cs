using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    #region var definitions

    [Header("Movement")]
    [SerializeField] private float maxMoveSpeed = 4.5f;
    //[SerializeField] private float minMoveSpeed = 0.5f;
    [SerializeField] private float gravity;
    public float direction;
    float moveSpeed = 5.5f;

    [Header("Jumping")]
    [SerializeField] private float maxJumpHeight = 2.5f;
    //[SerializeField] private float minJumpHeight = 4f;
    [SerializeField] private float timeToJumpApex = .6f;
    [SerializeField] private int maxJumps = 1;
    float jumpForce;
    int jumpCounter;
    int layerMask;


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
        layerMask = LayerMask.GetMask(Globals.GROUND_TAG);
        Physics2D.IgnoreLayerCollision(8, 8);

    }


    void Update()
    {
        moveSpeed = Mathf.MoveTowards(moveSpeed, maxMoveSpeed, Time.deltaTime);
        GetInput();
        WallCheck();
        ApplyMovement();
        GroundedCheck();
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) StaticLevelSelector.GoToLevelX(0,this);
    }

    private void WallCheck()
    {
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position,Vector2.right,0.365f,layerMask);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.365f, layerMask);
        if ((hitRight.collider != null && direction > 0) || (hitLeft.collider != null && direction < 0))
        {
            direction = 0;
        }
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
        transform.Translate(Vector3.right * direction * Time.deltaTime * moveSpeed);

        if (Input.GetKeyDown(KeyCode.Space) && jumpCounter < maxJumps)
        {
            jumpCounter++;
            body.AddForce(Vector2.up * body.gravityScale * jumpForce, ForceMode2D.Impulse);
        }
    }
}
