using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform feetTransform;
    [SerializeField] private float radius;
    private bool isGrounded;
    public bool IsGrounded { get => isGrounded; }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(feetTransform.position, radius, whatIsGround);
        if (colliders.Length > 0)
            isGrounded = true;
        else isGrounded = false;
    }
   
}
