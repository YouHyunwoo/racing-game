using System.Collections;
using System.Collections.Generic;
using RacingGame.Model;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] DataController dataController;
    [SerializeField] UIController uiController;
    [SerializeField] RoadController roadController;
    [SerializeField] CarController carController;
    [SerializeField] ItemController itemController;

    void Awake()
    {
        roadController.Initialize();
        carController.Initialize();
        itemController.Initialize();
    }

    #region Event Handler

    public void OnTitleStartButtonClick()
    {
        var car = new Car() {
            Gasoline = dataController.carGasoline,
            HorizontalMovementSpeed = dataController.carHorizontalMovementSpeed,
            VerticalMovementSpeed = dataController.carVerticalMovementSpeed * dataController.ProgressSpeedMultiplier,
            GasolineEfficiency = dataController.carGasolineEfficiency,
        };

        roadController.Play(car);
        carController.Play(car);
        itemController.Play(car);

        uiController.TransitTitleToGame();
    }

    public void OnGameLeftSideActive(bool active)
    {
        uiController.game.SetLeftSideActive(active);
    }

    public void OnGameRightSideActive(bool active)
    {
        uiController.game.SetRightSideActive(active);
    }

    public void OnGameGasolineConsume(float gasoline)
    {
        uiController.game.RefreshGasoline(gasoline);
    }

    public void OnGameGasolineEmpty()
    {
        roadController.Stop();
        carController.Stop();
        itemController.Stop();

        roadController.Clear();
        carController.Clear();
        itemController.Clear();

        uiController.TransitGameToGameOver();
    }

    public void OnGameOverBackButtonClick()
    {
        uiController.TransitGameOverToTitle();
    }

    #endregion
}
