using UnityEngine;
using TMPro;

public class BottomNavigationState : MonoBehaviour
{

    private TextMeshProUGUI bottomNavigationStateUIText;

    // Start is called before the first frame update
    void Start()
    {

        bottomNavigationStateUIText = GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBottomNavigationState(string _bottomNavigationState)
    {

        bottomNavigationStateUIText.text = _bottomNavigationState;

    }

}
