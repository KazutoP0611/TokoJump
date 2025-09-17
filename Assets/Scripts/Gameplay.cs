using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameplay : MonoBehaviour
{
    [SerializeField] bool startPlayerAtPoint = false;
    [SerializeField] Respawn respawnComponent;
    [SerializeField] GameObject Player;

    [SerializeField] GameObject GameoverPanel;
    [SerializeField] string titleSceneString;
    [SerializeField] string resultSceneString;
    [SerializeField] TMP_Text shellCountText;
    [SerializeField] AudioSource collectSound;

    public static int shellCount = 0;
    public static int currentScene;

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
        SceneManager.LoadScene(currentScene);
    }

    public void NextLevel()
    {
        int nextLevel = currentScene + 1;

        if (nextLevel >= SceneManager.sceneCountInBuildSettings) return;
        SceneManager.LoadScene(nextLevel);
    }

    public void Quit()
    {
        shellCount = 0;
        SceneManager.LoadScene(0);
    }

    public void LoadResultScene()
    {
        SceneManager.LoadScene(resultSceneString);
    }

    public void UpdateShellCount(int amount)
    {
        shellCount += amount;
        shellCountText.text = shellCount.ToString();
        collectSound.Play(0);
    }
}
