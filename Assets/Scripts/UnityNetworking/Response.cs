using System;

[Serializable]
public struct Response
{
    public UsersLeaderboard[] UsersData;

    public string token;

    public string login;

    public int score;
}
