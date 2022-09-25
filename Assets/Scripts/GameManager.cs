using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Animator Animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

        Animator.SetTrigger("Confirmation");

    }

    public void OnConfirmationTrue()
    {

        PlayerPrefs.SetInt("index", 1);
        Application.Quit();

    }
    
    public void OnConfirmationFalse()
    {

        Animator.SetTrigger("Confirmation");

    }

    private void OnLoadScene(int _index)
    {

        SceneManager.LoadScene(_index);

    }

    public void OnBack()
    {

        int _index = SceneManager.GetActiveScene().buildIndex;
        if (_index != 3)
        {

            OnLoadScene(1);

        }
        else
        {

            OnLoadScene(2);

        }

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

        OnLoadCareer();

    }

}
