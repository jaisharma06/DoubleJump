using System.Collections;
using UnityEngine;

public class SplashManager : MonoBehaviour
{
    [SerializeField]
    private float fadeDelay = 3;
    [SerializeField]
    private float fadeoutTime = 2;
    [SerializeField]
    private float fadeSpeed = 1;
    [SerializeField]
    private SpriteRenderer bg;
    [SerializeField]
    private GameObject uiCanvas;

    [SerializeField]
    private InputHandler InputHandler;

    GameObject bgO;
    // Start is called before the first frame update
    void Start()
    {
        bgO = bg.gameObject;
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        InputHandler.isInputActive = false;
        bgO.SetActive(true);
        uiCanvas.SetActive(false);
        yield return new WaitForSeconds(fadeDelay);
        while (bg.color.a != 0)
        {
            var color = bg.color;
            color.a = Mathf.MoveTowards(color.a, 0, Time.deltaTime * fadeSpeed);
            bg.color = color;
            yield return new WaitForEndOfFrame();
        }

        bgO.SetActive(false);
        uiCanvas.SetActive(true);
        InputHandler.isInputActive = true;
    }
}
