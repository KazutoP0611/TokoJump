using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] string gotoLevelString;

    private UI_Fade uiFadeController;
    private Coroutine changeLevelCoroutine;

    private void Awake()
    {
        uiFadeController = FindFirstObjectByType<UI_Fade>();
    }

    public void GotoLevel()
    {
        if (changeLevelCoroutine != null)
            StopCoroutine(changeLevelCoroutine);

        changeLevelCoroutine = StartCoroutine(GoToGame());
    }

    private IEnumerator GoToGame()
    {
        uiFadeController.FadeOut();
        yield return uiFadeController.fadeCoroutine;
        SceneManager.LoadScene(gotoLevelString);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
