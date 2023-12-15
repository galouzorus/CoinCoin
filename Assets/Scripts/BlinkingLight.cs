using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    private Light _light;
    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        if ( _light.enabled)
            _light.enabled = false;
        else _light.enabled = true;
        yield return new WaitForSeconds(0.25f);

        StartCoroutine(Blink());
    }
    
}
