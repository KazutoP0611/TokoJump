using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameplay : MonoBehaviour
{
    [SerializeField] bool startPlayerAtPoint = false;
    [SerializeField] Respawn respawnComponent;
    [SerializeField] GameObject Player;

    [SerializeField] GameObject GameoverPanel;
    [SerializeField] TMP_Text shellCountText;
    [SerializeField] AudioSource collectSound;

    private UI_Fade uiFadeController;
    private Coroutine changeSceneCoroutine;

    public static int shellCount = 0;
    public static int currentScene;

    private void Awake()
    {
        uiFadeController = FindFirstObjectByType<UI_Fade>();
    }

    private void Start()
    {
        GameoverPanel.SetActive(false);
        currentScene = SceneManager.GetActiveScene().buildIndex;

        if (startPlayerAtPoint)
            respawnComponent.SetStartObj(Player);
    }

    public void ShowGameOverPanel()
    {
        GameoverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        shellCount = 0;
        shellCountText.text = shellCount.ToString();

        ChangeScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        int nextLevel = currentScene + 1;
        if (nextLevel >= SceneManager.sceneCountInBuildSettings)
            return;
        
        ChangeScene(nextLevel);
    }

    public void LoadResultScene() => ChangeScene(1);

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

    public void Quit()
    {
        shellCount = 0;
        ChangeScene(0);
    }

    public void UpdateShellCount(int amount)
    {
        shellCount += amount;
        shellCountText.text = shellCount.ToString();
        collectSound.Play(0);
    }
}
