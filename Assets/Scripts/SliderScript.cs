using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{

    [SerializeField] private Slider _slider;

    public void UpdateProgress(float thrust) 
    {
        _slider.value = thrust;
    }
}
