using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField]
    private Coins[] coins; 
    // Start is called before the first frame update
    void Start()
    {
        var coin = Instantiate(SelectCoins(),transform.position,Quaternion.identity);
    }

    private Coins SelectCoins()

    {
        int rnd = Random.Range(0, coins.Length);
        return coins[rnd];
    }
}
