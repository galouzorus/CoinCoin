using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Final : MonoBehaviour
{
    public GameObject chest;
    private bool open = false;
    public GameObject particles;
    public TMP_Text coinsTxt;
    public TMP_Text timeTxt;
    public TMP_Text monsterTxt;

    // Start is called before the first frame update
    void Start()
    {
        coinsTxt.text = GameManager._instance.nbCoin.ToString();
        timeTxt.text = GameManager._instance.finalTime.ToString("0");
        monsterTxt.text = GameManager._instance.nbMonster.ToString();

    }
    //SCORE
    public void ScoreCoin()
    {   
        
        Debug.Log(coinsTxt.text);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!open)
        {
            chest.GetComponent<Animation>()["ChestAnim"].speed = 6.0f;
            open = true;
            particles.SetActive (true);

        }
        /*else
        {
            chest.GetComponent<Animation>()["ChestAnim"].time = chest.GetComponent<Animation>()["ChestAnim"].length;
            chest.GetComponent<Animation>()["ChestAnim"].speed = -1.0f;
            open = false;
        }*/
        chest.GetComponent<Animation>().Play("ChestAnim"); 
    }
   
}