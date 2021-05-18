using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SunBehavior : MonoBehaviour
{
    [SerializeField] private float minLight, maxLight, sunSpeed;
    private Light2D light;
    private bool sunRise;

    // Start is called before the first frame update
    void Start()
    {
        light = this.GetComponent<Light2D>();
        sunRise = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (sunRise)
        {
            light.intensity += sunSpeed;
            if (light.intensity >= maxLight)
                sunRise = false;
        }
        else
        {
            light.intensity -= sunSpeed;
            if (light.intensity <= minLight)
                sunRise = true;
        }
    }
}
