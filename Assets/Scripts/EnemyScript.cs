using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    public float speed = 5f;
    public LayerMask mask;
    private Vector3 ground;
    public bool isFacingRight; // Se está olhando para a direita
    public bool isDead; // Se está olhando para a direita
    private AudioManager audioManager;
    private Animator animator;

    // Start is called before the first frame update
    void Start () {
        isDead = false;
        isFacingRight = true;

        audioManager = FindObjectOfType<AudioManager> ();
        animator = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update () {
        // ground = gameObject.transform.position - Vector3.right * 0.25f;
        if (!isDead) {

            gameObject.transform.Translate (Vector3.right * speed * Time.deltaTime);
            RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.down, 0.75f, mask);

            if (hit.collider == null) {

                if (isFacingRight) {
                    gameObject.transform.eulerAngles = new Vector3 (0, -180, 0);
                    isFacingRight = false;
                } else {
                    gameObject.transform.eulerAngles = new Vector3 (0, 0, 0);
                    isFacingRight = true;
                }
                gameObject.transform.Translate (Vector3.right * 10 * speed * Time.deltaTime);

            }
        }

        animator.SetBool ("isDead", isDead);
    }

    // void OnTriggerEnter2D (Collider2D other) {

    //     Debug.Log (other.name);
    //     foreach (ContactPoint2D contact in Collider2D.GetContacts (other)) {

    //         Debug.Log (contact.normal.y);
    //         if (contact.normal.y < 0) { }
    //     }
    // }

    void OnTriggerEnter2D (Collider2D other) {

        if (other.gameObject.layer != LayerMask.NameToLayer ("Player") || isDead) {
            return;
        }

        if (other.gameObject.transform.position.y > (transform.position.y + 0.7)) {
            audioManager.Play ("stomp");
            isDead = true;
        } else {
            other.gameObject.SendMessage ("applyDamage");
        }
        // Collider2D.Distance (other);
        // if (Physics2D.Raycast (transform.position, Vector3.up, out hit, Mathf.Infinity, mask)) {
        //     Debug.Log ("Point of contact: " + hit.point);
        // }
    }
}