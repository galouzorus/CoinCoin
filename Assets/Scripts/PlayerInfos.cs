using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfos : MonoBehaviour
{
    public static PlayerInfos pi;
    public int playerHealth = 3;
    //public int nbCoins = 0;
    public Image[] hearts;
    public TMP_Text coinsTxt;
    private void Awake()
    {
        //variable = class
        pi = this;   
    }

    private void Start()
    {
        GameManager._instance.nbCoin = 0;
       // GameManager._instance.nbMonster = 0; 
    }
    public void SetHealth(int val)
    {
        playerHealth += val;
        
        SetHealthBar();
    }

    //COIN SCORE
    public void GetCoin(int value)
    {   
        GameManager._instance.nbCoin += value;
        coinsTxt.text = GameManager._instance.nbCoin.ToString();
    }
    //MONSTER SCORE
    /*public void KillMonster(int nb)
    {
        GameManager._instance.nbMonster += nb;
        coinsTxt.text = GameManager._instance.nbMonster.ToString();
    }*/



    public void SetHealthBar()
    {   //lifebar decrease
        foreach (Image img in hearts) 
        {
            img.enabled = false;
        }
        //heart number
        for(int i = 0; i<(playerHealth); i++) 
        {
            hearts[i].enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
