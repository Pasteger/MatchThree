using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

public class UserDataManager
{
    private List<UserData> _users;

    private static readonly UserDataManager Instance = new();

    private UserDataManager()
    {
    }

    public static UserDataManager GetInstance()
    {
        return Instance;
    }

    public List<UserData> GetUsers()
    {
        ReloadUsers();
        return _users;
    }

    public UserData AddUser(UserData newUser)
    {
        _users.Add(newUser);

        SaveUsers();

        ReloadUsers();

        return newUser;
    }

    public bool DeleteUser(UserData removedUser)
    {
        UserData user = null;

        foreach (var userData in _users.Where(
                     userData => userData.username == removedUser.username &&
                                 userData.levelCount == removedUser.levelCount &&
                                 userData.avatarName == removedUser.avatarName))
        {
            user = userData;
        }

        if (user == null) return false;
        
        var isRemoved = _users.Remove(user);

        SaveUsers();
        ReloadUsers();

        return isRemoved;
    }

    private void ReloadUsers()
    {
        if (File.Exists(GetSavePath()))
        {
            var formatter = new BinaryFormatter();
            var file = File.Open(GetSavePath(), FileMode.Open);
            _users = (List<UserData>)formatter.Deserialize(file);
            file.Close();
        }
        else
        {
            _users = new List<UserData>();
        }
    }

    public void SaveUsers()
    {
        var formatter = new BinaryFormatter();
        var file = File.Create(GetSavePath());
        formatter.Serialize(file, _users);
        file.Close();
    }

    private static string GetSavePath()
    {
        return Application.persistentDataPath + "/users.dat";
    }
}