using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public int nbCoin;
    public GameObject breakMenu;
    public GameObject looseText;
    public GameObject pauseText;
    public float currentTime = 0f;
    public float startingTime = 300f;
    bool isPaused = false;
    public int nbMonster;
    public float finalTime;

    
    [SerializeField]
    Button restartButton;
    [SerializeField]
    Button quit;
    

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Init()
    {   
        isPaused = false;
        nbCoin = 0;
        nbMonster = 0;
        breakMenu.SetActive(false);
        pauseText.SetActive(false);
        looseText.SetActive(false);
        restartButton.onClick.AddListener(() => {Restart(); });
        quit.onClick.AddListener(() => {Quit(); });
        Time.timeScale = 1;
        currentTime = startingTime;
    }

    private void Update()
    {
        GameOver();


        // PAUSE ACTIVE
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                isPaused = false;
                breakMenu.SetActive(isPaused);
                pauseText.SetActive(isPaused);
                Time.timeScale = 1f;
            }
            else
            {
                isPaused = true;
                breakMenu.SetActive(isPaused);
                pauseText.SetActive(isPaused);
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

            }

        }
        //LOOSE COUNTDOWN
        currentTime -= Time.deltaTime;
        

        if (currentTime <= 0)
        {
            currentTime = 0;
            breakMenu.SetActive(true);
            looseText.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

    public void SetFinalTime()
    {
        finalTime = startingTime - currentTime;
    }

    //LOOSE DEATH
    public void GameOver()
    {
        if (PlayerInfos.pi.playerHealth <= 0)
        {
            breakMenu.SetActive(true);
            looseText.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    //RESTART
    public void Restart()
    {
        Debug.Log("test");
        SceneManager.LoadScene(1);

    }
    //QUIT
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
