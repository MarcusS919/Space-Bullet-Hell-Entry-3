using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GaneOverManager : MonoBehaviour {

    public Button btnRestart;
    // Use this for initialization
    void Start()
    {

        if (GameManager.instance && btnRestart)
            btnRestart.onClick.AddListener(RestartGame);
    }

    void RestartGame()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
