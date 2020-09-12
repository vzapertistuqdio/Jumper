using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;


public class BotController : MonoBehaviour
{
  

    private float inputX;
    private float sensity = 4f;

    private Rigidbody playerRgb;
    private Animator playerAnim;
    private GroundChecker groundChecker;

    //private float multiPlieValue = 50;

    private const float OLD_JUMPFORCE_VALUE = 6f;
    private float speed = 12f;
    private float jumpForce = 6f;

    [SerializeField] private ParticleSystem bloodParticle;

    [SerializeField] private ParticleSystem bloodSpotParticle;

    private float trajectorySpeed = 10f;

    private bool isFirstTime = true;
    private bool wasDown = false;
    private bool attackFlag = false;

    private bool isAlive = true;


    public delegate void MethodContainer();
    public static event MethodContainer OnAttack;
    public static event MethodContainer OnGround;


    private void Start()
    {

        playerRgb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        groundChecker = GetComponent<GroundChecker>();
        Finish.OnVictory += Victory;
        StartCoroutine(DeathAfterTime(5.4f));

    }


    private void Update()
    {
        if (isAlive)
        {
            playerRgb.velocity = new Vector3(transform.forward.x * speed, playerRgb.velocity.y, transform.forward.z * speed);

            if (Input.GetMouseButton(0))
            {
                if (groundChecker.IsGrounded && wasDown)
                {

                    jumpForce += Time.deltaTime * trajectorySpeed;
                }
                inputX = Input.GetAxis("Mouse X");

            }
            if (groundChecker.IsGrounded)
            {
                playerAnim.SetBool("isRun", true);
                if (attackFlag == false)
                {
                    //OnGround();
                    playerAnim.SetBool("isAttack", true);
                    attackFlag = true;
                }

                isFirstTime = true;
                if (Input.GetMouseButtonDown(0) && wasDown == false)
                {
                    playerAnim.SetBool("isJump", true);
                    //OnAttack();
                    wasDown = true;

                }
                if (Input.GetMouseButtonUp(0) && wasDown)
                {
                    playerAnim.SetBool("isJump", false);

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
        else
        {
            //Destroy(this.gameObject);
            //this.gameObject.SetActive(false);
            playerRgb.useGravity = false;
            this.gameObject.tag = "Bot";
            playerAnim.SetTrigger("isFalling");
        }
    }


    private void Jump()
    {
        playerRgb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        jumpForce = OLD_JUMPFORCE_VALUE;
        // speed = jumpForce;
    }

    private void Victory()
    {
        playerAnim.SetTrigger("isVictory");
        speed = 0;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 180f, 0));
    
        Debug.Log("Kek");
    }

    private IEnumerator DeathAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        bloodParticle.Play();
        bloodSpotParticle.Play();
        isAlive = false;
    }




}
