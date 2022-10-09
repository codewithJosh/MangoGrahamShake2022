using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Animator animator;

    private enum startMenuStates { idle, newCareer, options, help, about, exit };
    private startMenuStates startMenuState = startMenuStates.idle;

    public void OnAnimateFromStartMenu(int _startMenuState)
    {

        startMenuState = GetStartMenuState(_startMenuState);
        animator.SetInteger("startMenuState", (int) startMenuState);

    }

    private startMenuStates GetStartMenuState(int _startMenuState)
    {
       
        switch (_startMenuState)
        {

            case 1:
                return startMenuStates.newCareer;

            case 2:
                return startMenuStates.options;

            case 3:
                return startMenuStates.help;

            case 4:
                return startMenuStates.about;

            case 5:
                return startMenuStates.exit;

        }

        return startMenuStates.idle;

    }

    public void OnExitAffirmative()
    {

        OnAnimateFromStartMenu(0);
        PlayerPrefs.SetInt("index", 1);
        Application.Quit();

    }

    public void OnBack()
    {

        OnAnimateFromStartMenu(0);

    }

    public void OnAnimateFromNewCareer(string _trigger)
    {

        animator.SetTrigger(_trigger);

    }

    public void OnNewCareer()
    {

        int countdown = 3;
        StartCoroutine(NewCareerToStart(countdown));

    }

    IEnumerator NewCareerToStart(int _countdown)
    {

        FindObjectOfType<GameManager>().OnAnimateFromStartMenu(0);

        while (_countdown > 0)
        {

            if (_countdown == 3)
            {

                OnAnimateFromNewCareer("startMenu");

            }

            yield return new WaitForSeconds(1f);

            _countdown--;

        }

        PlayerPrefs.SetInt("index", 2);
        SceneManager.LoadScene(0);

    }

    public void OnLoadCareer()
    {

        int countdown = 3;
        StartCoroutine(LoadCareerToStart(countdown));

    }

    IEnumerator LoadCareerToStart(int _countdown)
    {

        OnAnimateFromNewCareer("startMenu");

        while (_countdown > 0)
        {

            yield return new WaitForSeconds(1f);

            _countdown--;

        }

        PlayerPrefs.SetInt("index", 2);
        SceneManager.LoadScene(0);

    }

    public void OnWarningOverwriteTrue()
    {

        //OnNewPlayer();

    }

    public void OnWarningOverwriteFalse()
    {

        animator.SetTrigger("WarningOverwrite");

    }

    public void OnStartDay()
    {



    }

    public void OnMainMenu()
    {

        animator.SetTrigger("ConfirmationMainMenu");

    }

    public void OnWarningSaveGame()
    {

        animator.SetTrigger("WarningSaveGame");

    }

    public void OnConfirmationMainMenuTrue()
    {

        OnMainMenu();
        OnWarningSaveGame();

    }

    public void OnWarningSaveGameTrue()
    {

        FindObjectOfType<Player>().SavePlayer();
        OnWarningSaveGameFalse();

    }

    public void OnWarningSaveGameFalse()
    {

        OnWarningSaveGame();
        PlayerPrefs.SetInt("index", 1);
        //OnLoadScene(0);

    }

}
