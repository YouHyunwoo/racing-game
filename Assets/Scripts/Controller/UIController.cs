using System.Collections;
using System.Collections.Generic;
using RacingGame.UI;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public Title title;
    public Game game;
    public GameOver gameOver;

    public void TransitTitleToGame()
    {
        title.gameObject.SetActive(false);
        game.gameObject.SetActive(true);
    }

    public void TransitGameToGameOver()
    {
        game.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(true);
    }

    public void TransitGameOverToTitle()
    {
        gameOver.gameObject.SetActive(false);
        title.gameObject.SetActive(true);
    }
}
