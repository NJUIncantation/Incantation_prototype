using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public CharacterStats playerStats;

    List<IEndGameOberver> endGameObervers = new List<IEndGameOberver>();

    public void RigisterPlayer(CharacterStats player)
    {
        playerStats = player;
    }

    public void AddObserver(IEndGameOberver oberver)//¶©ÔÄ
    {
        endGameObervers.Add(oberver);
    }

    public void RemoveObserver(IEndGameOberver oberver)
    {
        endGameObervers.Remove(oberver);
    }

    public void NotifyObservers()//¹ã²¥
    {
        foreach (var observer in endGameObervers)
        {
            observer.EndNotify();
        }
    }
}
