﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {
    void Update () {
        gameObject.GetComponent<Text> ().text = GameState.score.ToString ();
    }
}