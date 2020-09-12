using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera playerCam;
    [SerializeField] private Camera victoryCam;

    private float inputX;
    private float sensity = 4f;

    private Rigidbody playerRgb;
    private Animator playerAnim;
    private GroundChecker groundChecker;

    //private float multiPlieValue = 50;

    private const float OLD_JUMPFORCE_VALUE = 6f;
   [SerializeField] private float speed =6f;
    private float jumpForce = 6f;

   

    private float trajectorySpeed = 10f;
       
    private bool isFirstTime = true;
    private bool wasDown = false;
    private bool attackFlag = false;

    [SerializeField] private TrajectoryRenderer trajectoryRenderer;

    public delegate void MethodContainer();
    public static event MethodContainer OnAttack;
    public static event MethodContainer OnGround;


    private void Start()
    {
        
        playerRgb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        groundChecker = GetComponent<GroundChecker>();
        Finish.OnVictory += Victory;
       
    }

    
    private void Update()
    {
        playerRgb.velocity = new Vector3(transform.forward.x * speed, playerRgb.velocity.y, transform.forward.z * speed);
  
        if (Input.GetMouseButton(0))
        {
            if (groundChecker.IsGrounded && wasDown)
            {
                    DrawTrajectory();
                jumpForce += Time.deltaTime * trajectorySpeed;                                          
            }
            inputX = Input.GetAxis("Mouse X");
                 
        }
        if(groundChecker.IsGrounded)
        {
            playerAnim.SetBool("isRun",true);
            if (attackFlag == false)
            {
                OnGround();
                playerAnim.SetBool("isAttack", true);
                attackFlag = true;
            }
     
            isFirstTime = true;
            if (Input.GetMouseButtonDown(0) && wasDown==false)
            {
                playerAnim.SetBool("isJump", true);
                OnAttack();
                wasDown = true;
               
            }
            if(Input.GetMouseButtonUp(0) && wasDown)
            {
                playerAnim.SetBool("isJump", false);
                DisableTrajectory();
                Jump();
                wasDown = false;
            }
        }
        else
        {
            if (attackFlag)
            {
                playerAnim.SetBool("isAttack", false);
                attackFlag = false;
            }

         
            playerAnim.SetBool("isRun", false);
            if (isFirstTime)
            {        
                playerAnim.SetTrigger("isFalling");
                isFirstTime = false;
            }        
        }
    }

 
    private void Jump()
    {
        playerRgb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
        jumpForce = OLD_JUMPFORCE_VALUE;
       // speed = jumpForce;
    }
    private void DrawTrajectory()
    {
        trajectoryRenderer.ShowTrajectory(transform.position, playerRgb.velocity+Vector3.up * jumpForce);
      
    }
    private void DisableTrajectory()
    {
        trajectoryRenderer.DisableShowTrajectory();
      
    }
    private void Victory()
    {
        playerAnim.SetTrigger("isVictory");
        speed = 0;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles+new Vector3(0,180f,0));
        playerCam.gameObject.SetActive(false);
        victoryCam.gameObject.SetActive(true);
        Debug.Log("Kek");
    }

    

   
}
