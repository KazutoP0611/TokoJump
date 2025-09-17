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

    float t = 0.8f;
    bool timeCount = false;
    int currentSceneIndex;

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

    //private void Update()
    //{
    //    if (timeCount)
    //    {
    //        t += Time.deltaTime;
    //        float imageAlpha = (t / fadeBackGroundInSecs);
    //        canvasBackground.color = new Color(0.4313726f, 0.6980392f, 0.4780907f, imageAlpha);

    //        if (t >= fadeBackGroundInSecs)
    //            timeCount = false;
    //    }
    //}

    public void GotoNextLevel()
    {
        Gameplay.shellCount = 0;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void BackToMainMenu()
    {
        Gameplay.shellCount = 0;
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        Gameplay.shellCount = 0;
        SceneManager.LoadScene(2);
    }
}
