using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Navigation : MonoBehaviour
{

    private ToggleGroup navigationPanel;

    void Start()
    {

        navigationPanel = GetComponent<ToggleGroup>();

    }

    public string GetNavigation
    {

        get 
        {
            
            Toggle navigation = navigationPanel.ActiveToggles().FirstOrDefault();
            return navigation.name.ToString(); 

        }

    }

}
