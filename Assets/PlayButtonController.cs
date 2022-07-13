using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonController : MonoBehaviour
{
    public void Play() => SceneManager.LoadScene("Scenes/SampleScene");
}
