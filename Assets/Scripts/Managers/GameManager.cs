using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Game Manager is null.");
            return _instance;
        }
    }


    [SerializeField] GameObject _player;

    [Header("Levels")]
    [SerializeField] Transform levelSpawnPos;
    [SerializeField] List<GameObject> levelList = new List<GameObject>();
    [SerializeField] GameObject activeLevel;

    int activeLevelIndex;

    [SerializeField] Transform _mainMenuPos;


    Vector3 checkPointPos;




    private void Awake()
    {
        _instance = this;
    }

    public void LoadLevel(int levelIndex)
    {
        if(activeLevel != null)
            DestroyLevel();

        activeLevelIndex = levelIndex;
        activeLevel = Instantiate(levelList[levelIndex], levelSpawnPos.position, Quaternion.identity);
        SpawnPlayerAtLevel();
        if(levelIndex < 1)
            _player.GetComponent<Player>().SetSKillCondition(false);
        else _player.GetComponent<Player>().SetSKillCondition(true);
    }

    public void DestroyLevel()
    {
        _player.SetActive(false);
        Destroy(activeLevel);

    }

    public void LoadNextLevel()
    {
        DestroyLevel();
        if(activeLevelIndex + 1 > levelList.Count - 1)
        {
            _player.SetActive(false);
            LoadLevelSelection();
            return;
        }
        else
        {
            _player.GetComponent<Player>().SetSKillCondition(true);
            activeLevelIndex = activeLevelIndex + 1;
            activeLevel = Instantiate(levelList[activeLevelIndex], levelSpawnPos.position, Quaternion.identity);
            SpawnPlayerAtLevel();
        }

    }

    public void RestartLevel()
    {
        DestroyLevel();
        activeLevel = Instantiate(levelList[activeLevelIndex], levelSpawnPos.position, Quaternion.identity);
        SpawnPlayerAtLevel();
    }

    public void GameOver()
    {
        DestroyLevel();
        //show game over scene__?????
        LoadLevelSelection();
    }



    public void LoadLevelSelection()
    {
        UIManager.Instance.LoadLevelSelectionScene();
    }


    public void SpawnPlayerInCheckpoint()
    {
        checkPointPos = _player.GetComponent<Player>().CheckPointPos;
        _player.transform.position = checkPointPos;

    }

    public Vector3 FindLevelSpawnPos()
    {
        Vector3 spawnPos = activeLevel.transform.Find("Spawn Pos").transform.position;
        return spawnPos;
    }

    public void SpawnPlayerAtLevel()
    {
        _player.SetActive(true);
        _player.transform.position = FindLevelSpawnPos();
    }

    public void LoadMainMenu()
    {
        DestroyLevel();
        UIManager.Instance.LoadMainMenu();
        UIManager.Instance.LoadLevelSelectionScene();
    }

}
