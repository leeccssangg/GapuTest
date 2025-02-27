using System.Collections;
using System.Collections.Generic;
using TW.Utility.DesignPattern;
using UnityEngine;

public class BallLevelManager : Singleton<BallLevelManager>
{
    public void SetCurLevel(int level)
    {
        InputManager.Instance.SetActive(true);
    }
}
