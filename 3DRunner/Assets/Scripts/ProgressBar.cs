using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 0;
    }

   
   private void Update()
    {
        slider.value += Time.deltaTime / 15;
    }
}
