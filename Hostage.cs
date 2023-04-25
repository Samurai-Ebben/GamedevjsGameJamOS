using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hostage : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D light;

    private void Start()
    {
        light = light.GetComponent<UnityEngine.Rendering.Universal.Light2D>();

    }
 
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //play effect and sound
            light.color = Color.HSVToRGB(185,100,100);
        }
    }

}
