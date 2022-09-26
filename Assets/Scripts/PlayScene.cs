using UnityEngine;
using UnityEngine.UI;

public class PlayScene : MonoBehaviour
{

    public Text playerName;
    public Button button;

    // Start is called before the first frame update
    void Start()
    {

        PlayerModel player = Database.LoadPlayer();

        if (player == null)
        {

            playerName.text = "NO SAVED GAME";
            button.interactable = false;

        }
        else
        {

            playerName.text = player.playerName;

        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
