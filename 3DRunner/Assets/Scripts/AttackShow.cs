using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackShow : MonoBehaviour
{
    private float radius = 3;

    private SkinnedMeshRenderer meshRenderer;
    private PlayerController playerController;

    private Rigidbody playerRgb;

   [SerializeField] private ParticleSystem spark;
   [SerializeField] private ParticleSystem explosive;

    [SerializeField] private GameObject head;

    private void Start()
    {
        playerRgb = GetComponent<Rigidbody>();
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        playerController = GetComponent<PlayerController>();
        PlayerController.OnAttack += UseAttack;
        PlayerController.OnGround += UseGrounded;
    
    }

    
    private void Update()
    {
      if(this.gameObject.tag=="Bot")
        {
            PlayerController.OnAttack -= UseAttack;
            PlayerController.OnGround -= UseGrounded;
        }
        
    }

    private void UseAttack()
    {
        ChangeRedColor();
        spark.Play();
        head.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
    private void UseGrounded()
    {
        ChangeBlueColor();
        Bang();
        explosive.Play();
        spark.Stop();
        head.transform.localScale = new Vector3(1, 1, 1);
    }

    private void ChangeRedColor()
    {
        meshRenderer.material.color =Color.yellow+Color.red;
    }
    private void ChangeBlueColor()
    {
        meshRenderer.material.color = Color.yellow;
    }
    private void Bang()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, radius);
       for(int i=0;i<colls.Length;i++)
        {

            if(colls[i].tag=="Obstacle")
            {
                Rigidbody obstacleRgb = colls[i].gameObject.GetComponent<Rigidbody>();
                Vector3 randomPart = new Vector3(UnityEngine.Random.Range(-5,5), UnityEngine.Random.Range(-5, 5), UnityEngine.Random.Range(-5, 5));
                Vector3 kickDirection = (transform.forward + Vector3.up + randomPart) * 10;
                obstacleRgb.AddForce(kickDirection, ForceMode.Impulse);
            }


            BotEnemy bot=colls[i].gameObject.GetComponent<BotEnemy>();
            if(bot!=null)
            {
                Rigidbody botRgb = colls[i].gameObject.GetComponent<Rigidbody>();
                Vector3 randomPart = new Vector3(UnityEngine.Random.Range(-2, 2), UnityEngine.Random.Range(-2, 2), UnityEngine.Random.Range(-2, 2));
                botRgb.AddForce(Vector3.up*3+ randomPart, ForceMode.Impulse);
                bot.Death();
            }
        }
    }

}
