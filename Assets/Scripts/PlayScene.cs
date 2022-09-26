using UnityEngine;
using UnityEngine.UI;

public class PlayScene : MonoBehaviour
{

    public GameObject playerName;
    public Button button;

    // Start is called before the first frame update
    void Start()
    {

        PlayerModel player = Database.LoadPlayer();

        if (player == null)
        {

            playerName.GetComponent<Text>().text = "NO SAVED GAME";
            button.interactable = false;

        }
        else
        {

            playerName.GetComponent<Text>().text = player.playerName;

        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
