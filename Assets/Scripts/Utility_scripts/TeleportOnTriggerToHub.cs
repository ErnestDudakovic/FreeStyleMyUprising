using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnTriggerToHub : MonoBehaviour
{
      public LevelLoader levelLoader;
   
         private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
              StartCoroutine(levelLoader.LoadLevel(1)); // Load the shop scene
          
        }
    }
    

  
}
