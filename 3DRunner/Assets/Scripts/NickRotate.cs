using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NickRotate : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private void Start()
    {
        
    }

  
    private void Update()
    {
        transform.LookAt(target.transform);
    }
}
