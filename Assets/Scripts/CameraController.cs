﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;

    public float cameraXOffset = 5.0f; // Define o deslocamento em X em relação ao personagem
    public float cameraYOffset = 1.0f; // Define o deslocamento em Y em relação ao personagem

    public float horizontalSpeed = 2.0f; // Velocidade que a câmera acompanhará na horizontal
    public float verticalSpeed = 2.0f; // Velocidade que a câmera acompanhará na vertical

    private Transform cameraTransform;
    private PlayerController playerController;

    void Start () {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerController = player.GetComponent<PlayerController> ();
        cameraTransform = Camera.main.transform;

        // Ja posiciona camera a frente do personagem
        cameraTransform.position = new Vector3 (
            player.transform.position.x + cameraXOffset,
            player.transform.position.y + cameraYOffset,
            cameraTransform.position.z
        );
    }

    void Update () {

        var x = Mathf.Lerp (cameraTransform.position.x,
            player.transform.position.x + (playerController.isFacingRight?cameraXOffset: -cameraXOffset),
            horizontalSpeed * Time.deltaTime);

        if (x < 0f) {
            x = 0f;
        } else if (x > 159f) {
            x = 159f;
        }
        cameraTransform.position = new Vector3 (x,
            0,
            cameraTransform.position.z
        );
    }
}