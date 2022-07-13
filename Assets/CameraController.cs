using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform characterTransform;
    private bool _reversing;
    private Vector3 _initialReversingRotation;
    private Vector3 _initialReversingPosition;
    private Vector3 _targetReversingRotation;
    private Vector3 _targetReversingPosition;
    private float _timerReversing;
    private const float ReversingTimerCap = 1;
    private readonly Vector3 UpPosition;
    private readonly Vector3 DownPosition;
    private readonly Vector3 UpRotation;
    private readonly Vector3 DownRotation;

    public CameraController()
    {
        UpPosition = new Vector3(0, 0, -5);
        UpRotation = new Vector3(180, 0, 0);
        DownPosition = new Vector3(0, 0, 5);
        DownRotation = Vector3.zero;
    }

    private void Update()
    {
        if (_reversing)
            PlayReversing();

        transform.position = new Vector3(characterTransform.position.x, transform.position.y, transform.position.z);
    }

    public void Reverse()
    {
        _reversing = true;
        _timerReversing = 0;

        _initialReversingRotation = transform.rotation.eulerAngles;
        _initialReversingPosition = transform.position;
        _targetReversingRotation = transform.rotation.x == 0 ? DownRotation : UpRotation;
        _targetReversingPosition = transform.position.z == 5 ? DownPosition : UpPosition;

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

    private void PlayReversing()
    {
        // if (_timerReversing >= ReversingTimerCap)
        //     _reversing = false;
        //
        // _timerReversing += Time.deltaTime;
        //
        // var lerpStep = Mathf.Clamp01(_timerReversing / ReversingTimerCap);
        // var diffBetweenAngles = _targetReversingRotation - _initialReversingRotation;
        // transform.position = RotatePointAroundPivot(
        //     new Vector3(transform.position.x, _initialReversingPosition.y, _initialReversingPosition.z),
        //     new Vector3(transform.position.x, 0, 0),
        //     diffBetweenAngles * lerpStep
        // );
        // transform.LookAt(new Vector3(transform.position.x, 0, 0));

        // if (transform.position.z == 5)
        // {
        //     transform.position = new Vector3(transform.position.x, transform.position.y, -5);
        //     transform.rotation = Quaternion.Euler(Vector3.zero);
        // }
        // else
        // {
        //     transform.position = new Vector3(transform.position.x, transform.position.y, 5);
        //     transform.rotation = Quaternion.Euler(new Vector3(180, 0, 0));
        // }
        //
        // var characterPosition = characterTransform.position;
        // var fromTo = Quaternion.Slerp(_initialReversingPosition, _targetReversingRotation, lerpStep);
        // transform.position = RotatePointAroundPivot(_initialReversingPosition, new Vector3(transform.position.x, 0, 0),  Vector3.right * lerpAngle);
        // transform.rotation = Quaternion.Slerp(quaternion.Euler(_initialReversingRotation), quaternion.Euler(_targetReversingRotation), lerpStep);
    }

    private static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        var dir =  point - pivot;
        dir = Quaternion.Euler(angles) * dir;
        point = dir + pivot;

        return point;
    }
}
