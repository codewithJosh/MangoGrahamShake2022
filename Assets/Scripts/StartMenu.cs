using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartMenu : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI playerNameUIText;
    [SerializeField] private Button loadCareerUIButton;

    private void Start()
    {

        FindObjectOfType<GameManager>().OnAnimateFromNewCareer("startMenu");

        PlayerModel player = Database.LoadPlayer();

        if (player == null)
        {

            playerNameUIText.text = "NO SAVED GAME";
            loadCareerUIButton.interactable = false;

        }
        else
        {

            playerNameUIText.text = player.playerName;

        }

    }

}
