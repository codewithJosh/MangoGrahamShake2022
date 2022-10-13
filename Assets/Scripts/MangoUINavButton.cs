using UnityEngine;
using UnityEngine.UI;

public class MangoUINavButton : MonoBehaviour
{

    private Toggle mangoUINavButton;

    void Start()
    {

        mangoUINavButton = GetComponent<Toggle>();

    }

    public void OnToggleTrue()
    {

        mangoUINavButton.isOn = true;

    }

}
