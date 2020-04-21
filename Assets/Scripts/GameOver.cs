using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
    void Start () {

        if (GameState.lives > 0) {
            gameObject.GetComponent<Text> ().text = "YOU WON!";
        } else {
            gameObject.GetComponent<Text> ().text = "YOU LOST!";
        }
    }

    void Update () {
        if (Input.GetKeyDown ("space")) {
            PlayGame ();
        }
    }
    void PlayGame () {
        GameState.Restart ();
        SceneManager.LoadScene ("Main");

    }

}