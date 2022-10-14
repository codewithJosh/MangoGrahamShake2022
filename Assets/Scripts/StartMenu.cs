using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class StartMenu : MonoBehaviour
{

    [SerializeField] private Button loadCareerUIButton;
    [SerializeField] private Button startUIButton;
    [SerializeField] private TextMeshProUGUI playerNameUIText;
    [SerializeField] private TMP_InputField playerNameHUD;
    private enum StartMenuStates { idle, newCareer, options, help, about, exit };
    private StartMenuStates startMenuState = StartMenuStates.idle;

    void Awake()
    {

        OnAnimateFromNewCareer("startMenu");

    }

    void Update()
    {

        string playerName = playerNameHUD.GetComponent<TMP_InputField>().text;
        PlayerModel player = Database.LoadPlayer();

        if (player == null)
        {

            CheckCurrentPlayerState("NO SAVED GAME", false);
            
        }
        else
        {

            CheckCurrentPlayerState(player.playerName, true);

        }

        if (playerName.Equals(""))
        {

            startUIButton.interactable = false;

        }
        else
        {

            startUIButton.interactable = true;

        }

        if (SimpleInput.GetButtonDown("OnStartFromNewCareer"))
        {

            if (startUIButton.interactable != false)
            {

                if (player != null)
                {

                    OnAnimateFromNewCareer("warningOverwrite");

                }
                else
                {

                    OnStartFromNewCareer();

                }

            }
            

        }

        if (SimpleInput.GetButtonDown("OnNewCareer"))
        {

            OnAnimateFromStartMenu(1);

        }

        if (SimpleInput.GetButtonDown("OnBackFromNewCareer"))
        {

            OnBackFromNewCareer();

        }

        if (SimpleInput.GetButtonDown("OnBack"))
        {

            OnBack();

        }

        if (SimpleInput.GetButtonDown("OnLoadCareer"))
        {

            if (loadCareerUIButton.interactable != false)
            {

                OnLoadCareer();

            }

        }

        if (SimpleInput.GetButtonDown("OnExit"))
        {

            OnAnimateFromStartMenu(5);

        }

        if (SimpleInput.GetButtonDown("OnExitAffirmative"))
        {

            OnExitAffirmative();

        }

        if (SimpleInput.GetButtonDown("OnWarningOverwrite"))
        {

            OnAnimateFromNewCareer("warningOverwrite");

        }

        if (SimpleInput.GetButtonDown("OnWarningOverwriteAffirmative"))
        {

            OnAnimateFromNewCareer("warningOverwrite");

        }

    }

    private void CheckCurrentPlayerState(string _playerName, bool _isInteractable)
    {

        playerNameUIText.text = _playerName;
        loadCareerUIButton.interactable = _isInteractable;

    }

    public void OnAnimateFromStartMenu(int _startMenuState)
    {

        startMenuState = GetStartMenuState(_startMenuState);
        FindObjectOfType<GameManager>().GetAnimator.SetInteger("startMenuState", (int) startMenuState);

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

        OnBack();

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

        FindObjectOfType<GameManager>().GetAnimator.SetTrigger(_trigger);

    }

    public void OnNewCareer()
    {

        int countdown = 3;
        StartCoroutine(NewCareerToStart(countdown));

    }

    IEnumerator NewCareerToStart(int _countdown)
    {

        OnBack();

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

        FindObjectOfType<Player>().NewPlayer(playerNameHUD.GetComponent<TMP_InputField>().text.ToString());
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

    public void OnBackFromNewCareer()
    {

        int countdown = 1;
        StartCoroutine(AnimateToStart(countdown));

    }

    IEnumerator AnimateToStart(int _countdown)
    {

        OnBack();

        while (_countdown > 0)
        {

            yield return new WaitForSeconds(1f);

            _countdown--;

        }

        playerNameHUD.GetComponent<TMP_InputField>().text = "";

    }

    public void OnStartFromNewCareer()
    {

        FindObjectOfType<Player>().NewPlayer(playerNameHUD.GetComponent<TMP_InputField>().text.ToString());
        OnNewCareer();

    }

}
