namespace RacingGame.UI
{
    using UnityEngine;

    public class Game : MonoBehaviour
    {
        [SerializeField] Gasoline gasoline;
        [SerializeField] GameObject leftSide;
        [SerializeField] GameObject rightSide;

        public void RefreshGasoline(float gasolineValue)
        {
            gasoline.RefreshGasoline(gasolineValue);
        }

        public void SetLeftSideActive(bool active)
        {
            leftSide.SetActive(active);
        }

        public void SetRightSideActive(bool active)
        {
            rightSide.SetActive(active);
        }
    }
}