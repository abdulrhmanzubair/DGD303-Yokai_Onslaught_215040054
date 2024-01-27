using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public int Trial;
   public GameObject final;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void  OpenScene()
    {
        SceneManager.LoadScene("Trial " + Trial.ToString());
        
      

        Debug.Log("level loaded");



    }

    private void Update()
    {
      
    }
}
