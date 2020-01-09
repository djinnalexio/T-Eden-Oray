using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script controls which passage becomes active when loading the Game Scene from the Start Scene
/// </summary>

public class StartScreenEntry : MonoBehaviour
{
    [Header("Starting States")]// get the possible states the Game Scene can start in.
    [SerializeField] Passage FirstPassage;
    [SerializeField] Passage Instructions;
    [SerializeField] Passage Credits;
    [SerializeField] Passage Secret;

    public static Passage EntryPoint;//variable to determine which passage the game scene will start with
    //The PassageController script will use it as the first passage

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {//Press B to set the EntryPoint to the first passage of the story and then load the game scene
            EntryPoint = FirstPassage;
            SceneManager.LoadScene("Game Screen");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {//Press I to set the EntryPoint to the instruction page and then load the game scene
            EntryPoint = Instructions;
            SceneManager.LoadScene("Game Screen");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {//Press C to set the EntryPoint to the credits page and then load the game scene
            EntryPoint = Credits;
            SceneManager.LoadScene("Game Screen");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {//Press K to set the EntryPoint to the secret 6 page and then load the game scene
            EntryPoint = Secret;
            SceneManager.LoadScene("Game Screen");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {//Press Q to stop the game
            Application.Quit();
        }
    }
}
