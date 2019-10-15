using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { NullState, MainMenu, Game }

public class GameManager : MonoBehaviour
{

    static GameManager _instance = null;
    int _Health;

    GameState _gm = GameState.NullState;

    public GameObject playerPrefab;

    // Use this for initialization
    void Awake()
    {
        if (instance)
            Destroy(gameObject);
        else
        {
            instance = this;

            DontDestroyOnLoad(this);
        }

        Health = 3;

        gm = GameState.MainMenu;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        gm = GameState.Game;
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void SpawnPlayer(Transform spawnLocation)
    {
        if (playerPrefab && spawnLocation)
        {
            Instantiate(playerPrefab, spawnLocation.position,
                spawnLocation.rotation);
        }
    }

    public static GameManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    public int Health
    {
        get { return _Health; }
        set
        {
            _Health = value;
            if (gm == GameState.Game)
            {
                if (_Health > 0)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                else
                    Debug.Log("Player Dead");
            }
        }
    }

    public GameState gm
    {
        get { return _gm; }
        set
        {
            _gm = value;
            Debug.Log("Current State: " + _gm);
        }
    }
}