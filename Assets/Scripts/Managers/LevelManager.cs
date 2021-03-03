using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance
    {
        get
        {
            if(_instance == null)
                Debug.LogError("Level Manager is null.");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void LevelOne()
    {
        GameManager.Instance.LoadLevel(0);
        UIManager.Instance.LoadLevelSelectionScene();
    }

    public void LevelTwo()
    {
        GameManager.Instance.LoadLevel(1);
        UIManager.Instance.LoadLevelSelectionScene();
    }

    public void LevelThree()
    {
        GameManager.Instance.LoadLevel(2);
        UIManager.Instance.LoadLevelSelectionScene();
    }

    public void LevelFour()
    {
        GameManager.Instance.LoadLevel(3);
        UIManager.Instance.LoadLevelSelectionScene();
    }

    public void LevelFive()
    {
        GameManager.Instance.LoadLevel(4);
        UIManager.Instance.LoadLevelSelectionScene();
    }

}
