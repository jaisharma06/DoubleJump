﻿using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private PlayerController playerController;
    private GameManager gameManager;
    // Use this for initialization
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        HandleInput();
#else
        HandleMobileInput();
#endif
    }

    private void HandleMobileInput()
    {
        if (Input.touchCount > 0 && gameManager.detectTouch)
        {
            var touch = Input.touches[0];
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (gameManager.isGamePaused)
                    {
                        gameManager.OnGamePaused(false);
                    }
                    if (!gameManager.isGameOver && !gameManager.isGamePaused)
                        playerController.Jump();
                    break;
                case TouchPhase.Ended:
                    if (gameManager.isGameOver)
                        gameManager.StartGame();
                    break;
                default: break;
            }
        }
    }

    private void HandleInput()
    {
        if (!gameManager.detectTouch)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (!gameManager.isGameOver)
                playerController.Jump();
            if (gameManager.isGamePaused)
            {
                gameManager.OnGamePaused(false);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (gameManager.isGameOver)
                gameManager.StartGame();
        }
    }
}
