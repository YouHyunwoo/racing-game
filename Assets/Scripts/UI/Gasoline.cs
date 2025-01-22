namespace RacingGame.UI
{
    using TMPro;
    using UnityEngine;

    public class Gasoline : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI gasolineText;

        public void RefreshGasoline(float gasoline)
        {
            gasolineText.text = $"Gasoline: {gasoline}";
        }
    }
}