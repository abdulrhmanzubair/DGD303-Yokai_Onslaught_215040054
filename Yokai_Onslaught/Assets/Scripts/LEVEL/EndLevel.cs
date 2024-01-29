using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
   

    public GameObject ENDMenuUI;



    void Start()
    {


    }
    private void Awake()
    {

    }
    void Update()
    {



        
    }

    
    

    public void LoadMenu()

    {
        ENDMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameMenu2");
        ;
        Debug.Log("Menu LOADED");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
        Debug.Log("Quit");
    }
}