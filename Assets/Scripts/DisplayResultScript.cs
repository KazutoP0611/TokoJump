using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DisplayResultScript : MonoBehaviour
{
    [Header("Background Settings")]
    [SerializeField] Image canvasBackground;
    [SerializeField] float fadeBackGroundInSecs = 1.2f;

    [Header("Score Settings")]
    [SerializeField] TextMeshProUGUI shellScoreText;

    [Header("Button Settings")]
    [SerializeField] Transform buttonGroup;
    [SerializeField] Button RestartBtn;
    [SerializeField] Button NextLevelBtn;

    [Header("Select Script Settings")]
    public SelectScript selectScript;

    private UI_Fade uiFadeController;
    private Coroutine changeSceneCoroutine;

    float t = 0.8f;
    bool timeCount = false;
    int currentSceneIndex;

    private void Awake()
    {
        uiFadeController = FindFirstObjectByType<UI_Fade>();
    }

    private void Start()
    {
        timeCount = true;
        
        currentSceneIndex = Gameplay.currentScene;
        int nextLevelIndex = currentSceneIndex + 1;
        if (nextLevelIndex >= SceneManager.sceneCountInBuildSettings)
        {
            NextLevelBtn.gameObject.SetActive(false);
            RestartBtn.gameObject.SetActive(true);

            //selectScript.SetStartButton(0);
            selectScript.Init(0);
        }
        else
        {
            RestartBtn.gameObject.SetActive(false);

            //selectScript.SetStartButton(1);
            selectScript.Init(2);
        }

        selectScript.SetStartButton(1);

        int shellScore = Gameplay.shellCount;
        shellScoreText.text = shellScore.ToString();
    }

    public void GotoNextLevel()
    {
        Gameplay.shellCount = 0;
        ChangeScene(currentSceneIndex + 1);
    }

    public void BackToMainMenu()
    {
        Gameplay.shellCount = 0;
        ChangeScene(0);
    }

    public void RestartGame()
    {
        Gameplay.shellCount = 0;
        ChangeScene(2);
    }

    private void ChangeScene(int loadLevel)
    {
        if (changeSceneCoroutine != null)
            StopCoroutine(changeSceneCoroutine);
        changeSceneCoroutine = StartCoroutine(ChangeSceneCoroutine(loadLevel));
    }

    private IEnumerator ChangeSceneCoroutine(int loadLevel)
    {
        uiFadeController.FadeOut();
        yield return uiFadeController.fadeCoroutine;
        SceneManager.LoadScene(loadLevel);
    }
}
