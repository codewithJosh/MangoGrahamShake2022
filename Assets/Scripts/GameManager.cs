using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Animator animator;

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

        animator.SetTrigger("Confirmation");

    }

    public void OnConfirmationTrue()
    {

        PlayerPrefs.SetInt("index", 1);
        Application.Quit();

    }

    public void OnConfirmationFalse()
    {

        animator.SetTrigger("Confirmation");

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

        PlayerModel player = Database.LoadPlayer();

        if (player != null)
        {

            animator.SetTrigger("Warning");

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

    public void OnRequiredOK()
    {

        animator.SetTrigger("Required");

    }

    public void OnWarningTrue()
    {

        OnNewPlayer();

    }

    public void OnWarningFalse()
    {

        animator.SetTrigger("Warning");

    }

}
