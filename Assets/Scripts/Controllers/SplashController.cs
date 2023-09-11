using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SplashController : MonoBehaviour
{
    private const string INTRO_SCENE_NAME = "Intro";

    IEnumerator Start()
    {
        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadScene(INTRO_SCENE_NAME);
    }
}
