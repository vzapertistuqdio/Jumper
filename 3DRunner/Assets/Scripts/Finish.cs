using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public delegate void MethodContainer();
    public static event MethodContainer OnVictory;

    [SerializeField] private ParticleSystem[] confetisAfter;

    [SerializeField] private ParticleSystem[] confetis;

    [SerializeField] private GameObject victoryText;

    [SerializeField] private GameObject levelText;
    [SerializeField] private GameObject progressBar;

    [SerializeField] private GameObject[] fireworks;

    private void Start()
    {
        //for(int i=0;i<fireworks.Length;i++)
        //{
        //    fireworks[i].SetActive(false);
        //}
        victoryText.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="Player")
        {
            for(int i=0;i<confetisAfter.Length;i++)
            {
                confetisAfter[i].Play();
               
            }
            Time.timeScale = 0.5f;
            victoryText.SetActive(true);
            progressBar.SetActive(false);
            levelText.SetActive(false);
            OnVictory();
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < confetis.Length; i++)
            {
                confetis[i].Play();

            }
            for (int i = 0; i < fireworks.Length; i++)
            {
                Debug.Log("kek");
                fireworks[i].SetActive(true);
            }
        }
       }


}
