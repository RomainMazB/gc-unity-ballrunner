using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReversingDoor : MonoBehaviour
{
    private Collider _ballCollider;
    private CameraController _cameraController;

    private void Start()
    {
        var rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();

        _ballCollider = rootGameObjects.First(o => o.name == "Sphere").GetComponent<Collider>();
        _cameraController = rootGameObjects.First(o => o.name == "Main Camera").GetComponent<CameraController>();
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider != _ballCollider) return;

        _cameraController.Reverse();
    }
}
