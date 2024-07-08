using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class StartCutScene : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirectorHUB;

    private void OnTriggerEnter2D(Collider2D collision2)
    {
        if(collision2.CompareTag("Player"))
        {
            playableDirectorHUB.Play();
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }


}
