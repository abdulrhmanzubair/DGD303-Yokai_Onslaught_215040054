using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemCollector : MonoBehaviour
{
    public static ItemCollector Counter;
    
    [SerializeField] private Text FAVORESofTheGods;
    
    public GameObject FavoresGODS;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            
            
          
            
            
            

            FavoresGODS.SetActive(false);
            Debug.Log("FAVORES COLLECTED");
            
        }
        
        
    }
    private void Update()
    {
        
    }
}