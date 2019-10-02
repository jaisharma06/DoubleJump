using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [HideInInspector]
    public bool isInputActive = false;
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
        if (!isInputActive)
        {
            return;
        }
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
            var isTouchOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(touch.fingerId);
            if (isTouchOverUI)
            {
                return;
            }
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (gameManager.state == GameState.PAUSED)
                    {
                        gameManager.OnGamePaused(false);
                    }
                    if (gameManager.state != GameState.OVER && gameManager.state != GameState.PAUSED)
                        playerController.Jump();
                    break;
                case TouchPhase.Ended:
                    if (gameManager.state == GameState.START)
                        gameManager.StartGame();
                    else if (gameManager.state == GameState.OVER)
                    {
                        gameManager.ShowStartGameUI();
                    }
                    break;
                default: break;
            }
        }
    }

    private void HandleInput()
    {
        var isTouchOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
        if (!gameManager.detectTouch || isTouchOverUI)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (gameManager.state != GameState.OVER && gameManager.state != GameState.PAUSED)
                playerController.Jump();
            if (gameManager.state == GameState.PAUSED)
            {
                gameManager.OnGamePaused(false);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (gameManager.state == GameState.START)
                gameManager.StartGame();
            else if (gameManager.state == GameState.OVER)
            {
                gameManager.ShowStartGameUI();
            }
        }
    }
}