﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {
    // Start is called before the first frame update
    public int speedX;
    public int speedY;
    public float maxX;
    public float maxY;

    private Vector3 initialPosition;
    void Start () {
        // O menor incremento de tempo
        initialPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update () {
        float dt = Time.deltaTime;

        if (Mathf.Abs (gameObject.transform.position.x - initialPosition.x) > (maxX / 2)) {
            speedX = -speedX;
        }
        if (Mathf.Abs (gameObject.transform.position.y - initialPosition.y) > (maxY / 2)) {
            speedY = -speedY;
        }
        gameObject.transform.position += Vector3.right * dt * speedX;
        gameObject.transform.position += Vector3.up * dt * speedY;

    }
}