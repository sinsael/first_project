using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : DefualtUI
{
    public static GameOverUI Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.PauseUI = this.gameObject;
            if (UIManager.Instance.uidict != null)
            {
                UIManager.Instance.uidict[UIManager.UIType.GameOverUI] = this.gameObject;
            }
        }



    }
}