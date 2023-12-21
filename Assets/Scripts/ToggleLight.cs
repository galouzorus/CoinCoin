using System.Collections;
using UnityEngine;

public class ToggleLight : MonoBehaviour
{
    public GameObject directionalLight;
    public GameObject grid;
    // Start is called before the first frame update
    void Start()
    {
        directionalLight.GetComponent<Light>().enabled = true;
        grid.SetActive(false); 
    }

    private void OnTriggerEnter(Collider other)
    {
        //StartCoroutine(FadeLight(directionalLight.GetComponent<Light>(), 1, 0, 5));
        grid.SetActive(true);
        directionalLight.GetComponent<Light>().enabled = false;

    }
    private void OnTriggerExit(Collider other)
    {
        grid.SetActive(false);
        directionalLight.GetComponent<Light>().enabled = true;
        //StartCoroutine(FadeLight(directionalLight.GetComponent<Light>(), 0, 2, 3));


    }
    /*private IEnumerator FadeLight(Light directionalLight, float startValue, float endValue, float fadeTime)
    {
        while (fadeTime > 0) 
        {
            fadeTime -= Time.deltaTime;
            directionalLight.intensity = Mathf.Lerp(startValue, endValue, Time.deltaTime*10);
        }
        yield return null;
    }*/
}
