using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
    // Start is called before the first frame update
    public LayerMask mask = 1 << 8;
    void Start () { }

    // Update is called once per frame
    void Update () {

        RaycastHit2D hit = Physics2D.Raycast (gameObject.transform.position, Vector3.up, Mathf.Infinity, mask);
        if (hit.collider != null) {

            Vector3 position = gameObject.transform.position + Vector3.right * 1.5f + Vector3.up * 0.5f;

            if (GameState.checkpoint.x < position.x) {
                GameState.checkpoint = position;
            }

        }
    }
}