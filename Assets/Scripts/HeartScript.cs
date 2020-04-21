using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour {
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        int children = gameObject.transform.childCount;
        for (int i = 0; i < children; i++) {
            SpriteRenderer renderer = gameObject.transform.GetChild (i).gameObject.GetComponent<SpriteRenderer> ();
            if (i < GameState.lives) {
                renderer.color = Color.red;
            } else {
                renderer.color = Color.grey;

            }
        }
    }
}