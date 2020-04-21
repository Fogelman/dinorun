using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScoreScript : MonoBehaviour {
    void Update () {
        gameObject.GetComponent<Text> ().text = GameState.maxScore.ToString ();
    }
}