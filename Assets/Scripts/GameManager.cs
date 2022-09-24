using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    

    
    }

    public void OnHelp()
    {
    

    
    }

    public void OnAbout()
    {
    

    
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

}
