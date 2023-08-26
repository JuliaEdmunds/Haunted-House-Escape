using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SplashController : MonoBehaviour
{
    private const string INTRO_SCENE_NAME = "Intro";

    void Start()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(INTRO_SCENE_NAME);
    }
}
