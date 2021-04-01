/*
 --------------- Developer's Notes -------------------
 This script is attached to a prefab and operates level transitions
 In the start menu, Play and Quit Buttons can be interacted with
 When the player presses the Play button, it will call loadNextScene which will load the next scene which is the main level
 The Quit button will exit the game if it's run from a build
 
 The Game Over Scene only has one button, and if the player presses it, it will call loadStartScene which brings the player back to the start menu
 
 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void loadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // gets the current scene index
        SceneManager.LoadScene(currentSceneIndex + 1); // loading the next scene based on an addition to the current scene's index
    }

    public void loadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit(); // only works in a standalone build, not in the editor
    }
}
