using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Animator animator;

    private enum StartMenuStates { idle, newCareer, options, help, about, exit };
    private StartMenuStates startMenuState = StartMenuStates.idle;
    
    public void OnAnimateFromStartMenu(int _startMenuState)
    {

        startMenuState = GetStartMenuState(_startMenuState);
        animator.SetInteger("startMenuState", (int) startMenuState);

    }

    private StartMenuStates GetStartMenuState(int _startMenuState)
    {

        return _startMenuState switch
        {

            1 => StartMenuStates.newCareer,

            2 => StartMenuStates.options,

            3 => StartMenuStates.help,

            4 => StartMenuStates.about,

            5 => StartMenuStates.exit,

            _ => StartMenuStates.idle,

        };

    }

    public void OnExitAffirmative()
    {

        int countdown = 3;
        StartCoroutine(ExitToStart(countdown));

    }

    IEnumerator ExitToStart(int _countdown)
    {

        OnAnimateFromStartMenu(0);

        while (_countdown > 0)
        {

            if (_countdown == 3)
            {

                OnAnimateFromNewCareer("startMenu");

            }

            yield return new WaitForSeconds(1f);

            _countdown--;

        }

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

        OnAnimateFromStartMenu(0);

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

    public void OnWarningOverwriteAffirmative()
    {

        FindObjectOfType<Player>().NewPlayer();
        int countdown = 1;
        StartCoroutine(WarningOverwriteAffirmativeToStart(countdown));

    }

    IEnumerator WarningOverwriteAffirmativeToStart(int _countdown)
    {

        OnAnimateFromNewCareer("warningOverwrite");

        while (_countdown > 0)
        {

            yield return new WaitForSeconds(1f);

            _countdown--;

        }

        OnNewCareer();

    }

    public Animator GetAnimator
    {

        get { return animator; }

    }

}
