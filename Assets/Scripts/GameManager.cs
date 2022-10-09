using System;
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

    public void OnPlay()
    {

        OnLoadScene(2);
    }

    public void OnHelp()
    {

        OnLoadScene(6);

    }

    public void OnAbout()
    {

        OnLoadScene(7);

    }

    public void OnExit()
    {

        animator.SetTrigger("ConfirmationExit");

    }

    public void OnConfirmationExitTrue()
    {

        

    }

    public void OnConfirmationExitFalse()
    {

        animator.SetTrigger("ConfirmationExit");

    }

    private void OnLoadScene(int _index)
    {

        SceneManager.LoadScene(_index);

    }

    public void OnNewCareer()
    {

        OnLoadScene(3);

    }

    public void OnLoadCareer()
    {

        PlayerPrefs.SetInt("index", 4);
        OnLoadScene(0);

    }

    public void OnStartNewCareer()
    {

        PlayerModel player = Database.LoadPlayer();

        if (player != null)
        {

            animator.SetTrigger("WarningOverwrite");

        }
        else
        {

            OnNewPlayer();

        }

    }

    private void OnNewPlayer()
    {

        FindObjectOfType<Player>().NewPlayer();
        OnLoadCareer();

    }

    public void OnRequiredPlayerNameOK()
    {

        animator.SetTrigger("RequiredPlayerName");

    }

    public void OnWarningOverwriteTrue()
    {

        OnNewPlayer();

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
        OnLoadScene(0);

    }

}
