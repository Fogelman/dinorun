using System.Collections;
using System.Collections.Generic;
using Prime31;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public CharacterController2D.CharacterCollisionState2D flags;
    public float walkSpeed = 4.0f; // Depois de incluido, alterar no Unity Editor
    public float jumpSpeed = 8.0f; // Depois de incluido, alterar no Unity Editor
    public float doubleJumpSpeed = 6.0f; //Depois de incluido, alterar no Editor
    public float gravity = 9.8f; // Depois de incluido, alterar no Unity Editor
    public bool doubleJumped; // informa se foi feito um pulo duplo

    public bool isDucking;
    public bool isGrounded; // Se está no chão
    public bool isJumping; // Se está pulando
    public bool isFacingRight; // Se está olhando para a direita
    public bool isFalling; // Se estiver caindo 
    public bool isDead; // Se estiver caindo 

    public LayerMask mask;

    private Vector3 moveDirection = Vector3.zero; // direção que o personagem se move
    private CharacterController2D characterController; //Componente do Char. Controller

    private BoxCollider2D boxCollider;
    private float colliderSizeY;
    private float colliderOffsetY;
    private Animator animator;
    private AudioManager audioManager;

    void Start () {
        characterController = GetComponent<CharacterController2D> (); //identif. o componente
        animator = GetComponent<Animator> ();
        boxCollider = GetComponent<BoxCollider2D> ();
        audioManager = FindObjectOfType<AudioManager> ();

        transform.position = GameState.checkpoint;
        colliderSizeY = boxCollider.size.y;
        colliderOffsetY = boxCollider.offset.y;

        isDead = false;
    }

    void Update () {
        moveDirection.x = Input.GetAxis ("Horizontal"); // recupera valor dos controles
        moveDirection.x *= walkSpeed;

        if (!isDead) {
            if (moveDirection.x < 0) {
                transform.eulerAngles = new Vector3 (0, 180, 0);
                isFacingRight = false;

            } else if (moveDirection.x > 0) {
                transform.eulerAngles = new Vector3 (0, 0, 0);
                isFacingRight = true;

            }

            if (isGrounded) { // caso esteja no chão
                moveDirection.y = 0.0f;
                isJumping = false;
                doubleJumped = false; // se voltou ao chão pode faz pulo duplo

                if (Input.GetButton ("Jump")) {
                    audioManager.Play ("jump");
                    moveDirection.y = jumpSpeed;
                    isJumping = true;
                }

            } else { // caso esteja pulando 
                if (Input.GetButtonUp ("Jump") && moveDirection.y > 0) // Soltando botão diminui pulo
                    moveDirection.y *= 0.5f;

                if (Input.GetButtonDown ("Jump") && !doubleJumped) // Segundo clique faz pulo duplo
                {
                    audioManager.Play ("jump");
                    moveDirection.y = doubleJumpSpeed;
                    doubleJumped = true;
                }
            }

            RaycastHit2D hit = Physics2D.Raycast (transform.position, -Vector2.up, 4f, mask);
            if (hit.collider != null && isGrounded) {
                transform.SetParent (hit.transform);
                if (Input.GetAxis ("Vertical") < 0 && Input.GetButtonDown ("Jump")) {
                    moveDirection.y = -jumpSpeed;
                    StartCoroutine (PassPlatform (hit.transform.gameObject));
                }
            } else {
                transform.SetParent (null);
            }

            if (moveDirection.y < 0) {

                isFalling = true;
            } else {
                isFalling = false;

            }

            moveDirection.y -= gravity * Time.deltaTime; // aplica a gravidade
            characterController.move (moveDirection * Time.deltaTime); // move personagem    

            flags = characterController.collisionState; // recupera flags
            isGrounded = flags.below; // define flag de chão
        }

        // Atualizando Animator com estados do jogo
        animator.SetFloat ("movementX", Mathf.Abs (moveDirection.x / walkSpeed)); // +Normalizado
        animator.SetFloat ("movementY", moveDirection.y);
        animator.SetBool ("isGrounded", isGrounded);
        animator.SetBool ("isJumping", isJumping);
        animator.SetBool ("isFalling", isFalling);
        animator.SetBool ("isDead", isDead);

    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer ("Coins")) {
            GameState.score += 5;
            audioManager.Play ("coin");
            Destroy (other.gameObject);
        } else if (other.gameObject.layer == LayerMask.NameToLayer ("Damage")) {
            if (!isDead) {
                StartCoroutine ("DeathSequence");
            }
        }
    }

    private IEnumerator DeathSequence () {
        isDead = true;
        float waitFor = 2.7f;
        if (GameState.score > GameState.maxScore) {
            GameState.maxScore = GameState.score;
        }

        if (GameState.lives <= 1) {
            waitFor = 4f;
            audioManager.Stop (waitFor);
            audioManager.Play ("gameover");
        } else {
            audioManager.Stop (waitFor);
            audioManager.Play ("die");
        }
        yield return new WaitForSeconds (waitFor);
        GameState.lives -= 1;
        GameState.score = 0;
        if (GameState.lives <= 0) {
            SceneManager.LoadScene ("GameOver");

        } else {
            SceneManager.LoadScene ("Main");
        }

    }

    private IEnumerator PassPlatform (GameObject platform) {
        platform.GetComponent<EdgeCollider2D> ().enabled = false;
        yield return new WaitForSeconds (1.0f);
        platform.GetComponent<EdgeCollider2D> ().enabled = true;
    }
}