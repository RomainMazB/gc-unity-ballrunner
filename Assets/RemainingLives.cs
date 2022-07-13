using UnityEngine;
using UnityEngine.UI;

public class RemainingLives : MonoBehaviour
{
    [SerializeField] private BallController ballController;

    [SerializeField] private Text remainingLifeSentence;

    private void Update()
    {
        remainingLifeSentence.text = "Remaining life: " + ballController.RemainingLives;
    }
}