using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class ActivateCutscene : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playableDirector.Play();
            GetComponent<BoxCollider2D>().enabled = false;
            playableDirector.stopped += OnCutsceneEnd; // Prijavite se na događaj završetka cutscene-a
        }
    }

    private void OnCutsceneEnd(PlayableDirector director)
    {
        SceneManager.LoadScene("FinalLevel"); // Učitava scenu naziva "FinalLevel"
    }
}
