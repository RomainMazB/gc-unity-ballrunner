using UnityEngine;
using UnityEngine.UI;

public class Speed : MonoBehaviour
{
    [SerializeField] private BallController ballController;

    [SerializeField] private Text speedSentence;

    private void Update()
    {
        speedSentence.text = "Speed: " + ballController.CurrentSpeed;
    }
}
