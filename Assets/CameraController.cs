using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform characterTransform;

    private void Update()
    {
        transform.position = new Vector3(characterTransform.position.x, transform.position.y, transform.position.z);
    }

    public void Reverse()
    {
        if (transform.position.z == 5)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -5);
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 5);
            transform.rotation = Quaternion.Euler(new Vector3(180, 0, 0));
        }
    }
}
