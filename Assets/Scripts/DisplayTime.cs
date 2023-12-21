
using TMPro;
using UnityEngine;

public class DisplayTime : MonoBehaviour
{
    [SerializeField]
    TMP_Text countDown;

    // Update is called once per frame
    void Update()
    {
        countDown.text = GameManager._instance.currentTime.ToString("0");
         
    }
}
