using UnityEngine;
using UnityEngine.UI;

public class ResultsUINavButton : MonoBehaviour
{

    private Toggle resultsUINavButton;

    void Start()
    {

        resultsUINavButton = GetComponent<Toggle>();

    }

    public void OnToggleTrue()
    {

        resultsUINavButton.isOn = true;

    }

}
