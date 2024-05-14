using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerController))]
public class PlayerInput : MonoBehaviour
{
    public KeyCode forward = KeyCode.W;
    public KeyCode turnLeftNew = KeyCode.A;
    public KeyCode turnRightNew = KeyCode.D;
    private GameObject teleporter;

    private PlayerController controller;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(forward)) controller.MoveForward();
        if (Input.GetKeyDown(turnRightNew)) controller.RotateRight();
        if (Input.GetKeyDown(turnLeftNew)) controller.RotateLeft();
        if (Input.GetKeyDown(KeyCode.R))
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
