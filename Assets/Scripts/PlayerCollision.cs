using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public GameObject player;
    public Transform spawnPoint;
    public GameObject pickupEffect;
    public GameObject killEffect;
    bool canInstantiate = true;
    bool isInvincible = false;
    public AudioClip hitSound;
    public AudioClip coinSound;
    AudioSource audioSource;
    public SkinnedMeshRenderer rend;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="Coins")
        {
            audioSource.PlayOneShot(coinSound);
            GameObject go = Instantiate(pickupEffect,other.transform.position,Quaternion.identity);
            Destroy(go, 0.5f);
            Coins coins = other.gameObject.GetComponent<Coins>();
            PlayerInfos.pi.GetCoin(coins.value);
            Destroy(other.gameObject);
        }
            //if Kill enemy
        if (other.gameObject.tag == "Kill" && canInstantiate)
        {
            canInstantiate = false;
            audioSource.PlayOneShot(hitSound);
            other.transform.parent.gameObject.GetComponent<Collider>().enabled = false;
            iTween.PunchScale(other.transform.parent.gameObject, new Vector3(10f, 10f, 10f), 0.6f);
            //Kill enemy
            GameObject go = Instantiate(killEffect, other.transform.position, Quaternion.identity);
            Destroy(go, 0.6f);
            GameManager._instance.nbMonster++;
            print("t'es mort");
            Destroy(other.gameObject.transform.parent.gameObject, 0.5f);
            StartCoroutine("ResetInstantiate");
        }
        //WaterDeath
        if (other.gameObject.tag == "Death")
        {
            //Spawn to start point
            player.transform.position = spawnPoint.position;

            PlayerInfos.pi.SetHealth(-1);
        }
        //Portal
        if (other.gameObject.tag == "Portal")
        {
            GameManager._instance.SetFinalTime();
            SceneManager.LoadScene("End");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {   //Monster Damage
        if (collision.gameObject.tag == "Hurt" && !isInvincible)
        {
            print ("ca fait mal");
            isInvincible = true;
            PlayerInfos.pi.SetHealth(-1);
            iTween.PunchPosition(gameObject, Vector3.back * 5, 0.5f);
            StartCoroutine("ResetInvincible");
        }
    }

    //reset Instance bool after 0.5f
    IEnumerator ResetInstantiate()
    {
        yield return new WaitForSeconds(0.5f);
        canInstantiate = true;
    }
    //reset after hurt
    IEnumerator ResetInvincible()
    {
        for (int i=0; i < 10; i++)
        {   //flash mesh
            yield return new WaitForSeconds(0.2f);
            rend.enabled = !rend.enabled;
        }
        yield return new WaitForSeconds(0.2f);
        rend.enabled = true;
        isInvincible = false;
    }

    

}
