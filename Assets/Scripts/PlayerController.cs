﻿using System.Collections;
using System.Collections.Generic;
using Prime31;
using UnityEngine;

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

    private Vector3 moveDirection = Vector3.zero; // direção que o personagem se move
    private CharacterController2D characterController; //Componente do Char. Controller

    private BoxCollider2D boxCollider;
    private float colliderSizeY;
    private float colliderOffsetY;
    private Animator animator;

    void Start () {
        characterController = GetComponent<CharacterController2D> (); //identif. o componente
        animator = GetComponent<Animator> ();
        boxCollider = GetComponent<BoxCollider2D> ();
        colliderSizeY = boxCollider.size.y;
        colliderOffsetY = boxCollider.offset.y;
    }

    void Update () {
        moveDirection.x = Input.GetAxis ("Horizontal"); // recupera valor dos controles
        moveDirection.x *= walkSpeed;

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
                moveDirection.y = jumpSpeed;
                isJumping = true;
            }

        } else { // caso esteja pulando 
            if (Input.GetButtonUp ("Jump") && moveDirection.y > 0) // Soltando botão diminui pulo
                moveDirection.y *= 0.5f;

            if (Input.GetButtonDown ("Jump") && !doubleJumped) // Segundo clique faz pulo duplo
            {
                moveDirection.y = doubleJumpSpeed;
                doubleJumped = true;
            }
        }

        if (moveDirection.y < 0)
            isFalling = true;
        else
            isFalling = false;

        moveDirection.y -= gravity * Time.deltaTime; // aplica a gravidade
        characterController.move (moveDirection * Time.deltaTime); // move personagem    

        flags = characterController.collisionState; // recupera flags
        isGrounded = flags.below; // define flag de chão

        if (Input.GetAxis ("Vertical") < 0 && moveDirection.x == 0) {
            if (!isDucking) {
                boxCollider.size = new Vector2 (boxCollider.size.x, 2 * colliderSizeY / 3);
                boxCollider.offset = new Vector2 (boxCollider.offset.x, colliderOffsetY - colliderSizeY / 6);
                characterController.recalculateDistanceBetweenRays ();
            }
            isDucking = true;
        } else {
            if (isDucking) {
                boxCollider.size = new Vector2 (boxCollider.size.x, colliderSizeY);
                boxCollider.offset = new Vector2 (boxCollider.offset.x, colliderOffsetY);
                characterController.recalculateDistanceBetweenRays ();
                isDucking = false;
            }
        }

        // Atualizando Animator com estados do jogo
        animator.SetFloat ("movementX", Mathf.Abs (moveDirection.x / walkSpeed)); // +Normalizado
        animator.SetFloat ("movementY", moveDirection.y);
        animator.SetBool ("isGrounded", isGrounded);
        animator.SetBool ("isJumping", isJumping);
        // animator.SetBool ("isDucking", isDucking);
        animator.SetBool ("isFalling", isFalling);
    }
}