using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{

    public Image fill;

    private void Start()
    {

        int index = PlayerPrefs.GetInt("index", 1);
        LoadScene(index);

    }

    public void LoadScene(int sceneIndex)
    {

        StartCoroutine(LoadAsynchronously(sceneIndex));

    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {

            float progress = Mathf.Clamp01(operation.progress / .9f);

            fill.fillAmount = progress;

            yield return null;

        }

    }

}
