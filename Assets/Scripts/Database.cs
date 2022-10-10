using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Database
{
    public static void SavePlayer(Player _player)
    {

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.mango";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerModel player = new PlayerModel(_player);
        formatter.Serialize(stream, player);
        stream.Close();

    }

    public static PlayerModel LoadPlayer()
    {

        string path = Application.persistentDataPath + "/player.mango";

        if (File.Exists(path))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerModel player = formatter.Deserialize(stream) as PlayerModel;
            stream.Close();

            return player;

        }
        else
        {

            Debug.Log("Savefile Not Found in " + path);
            return null;

        }

    }

}
