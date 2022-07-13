using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private new Collider collider;
    [SerializeField] private float speedIncreasePerSecond = .03f;
    [SerializeField] private float initialVelocityX = 2f;
    public int RemainingLives { get; private set; } = 3;
    [SerializeField] private GameController gameController;
    public float CurrentSpeed => rb.velocity.x;
    private float _currentVelocityX;
    private bool _isGrounded;
    private bool _jump;
    private bool _fishingOut;
    private Vector3 _fishingOutResetPosition;

    #region Unity life-cycle methods
        private void Start()
        {
            _currentVelocityX = initialVelocityX;
        }

        private void Update()
        {
            CheckEndGame();
        }

        private void FixedUpdate()
        {
            if (_fishingOut)
            {
                PlayFishingOut();
                return;
            }

            _currentVelocityX += speedIncreasePerSecond * Time.fixedDeltaTime;

            rb.velocity = new Vector3(_currentVelocityX, rb.velocity.y, 0f);

            _isGrounded = Physics.Raycast(transform.position, Vector3.down, .6f);

            if (_jump)
            {
                rb.AddForce(Vector3.up * 6f, ForceMode.Impulse);
                _jump = false;
            }
        }
    #endregion

    /// <summary>
    /// Check if the ball is falling, if so put the ball in FishOut state or switch to game over scene
    /// </summary>
    private void CheckEndGame()
    {
        if (_fishingOut || GameController.MinY < transform.position.y) return;

        if (RemainingLives > 0)
            FishOut();
        else
            SceneManager.LoadScene("Scenes/GameOverScene");
    }

    /// <summary>
    /// Calculate the FishOut reset position and put the ball in the FishOut state
    /// </summary>
    private void FishOut()
    {
        // Calculate the reset position
        var lastBrick = gameController.BlocksList.Last(block => block.transform.position.x < transform.position.x);
        _fishingOutResetPosition = lastBrick.transform.position + Vector3.up * 1.5f;

        // Decrease life counter and disable Collider and Rigidbody
        RemainingLives--;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        collider.enabled = false;

        // Put the ball in FishOut state
        _fishingOut = true;
    }

    /// <summary>
    /// Play the FishOut cinematic
    /// </summary>
    private void PlayFishingOut()
    {
        // Go Up
        if (_fishingOutResetPosition.y > transform.position.y)
            rb.velocity = Vector3.up;
        // Go Left
        else if (_fishingOutResetPosition.x -.5f < transform.position.x)
            rb.velocity = Vector3.left;
        // Release
        else
        {
            _fishingOut = false;
            rb.useGravity = true;
            collider.enabled = true;
            rb.velocity = Vector3.right * initialVelocityX;
        }
    }

    #region Inputs
    /// <summary>
    /// Bound on the InputManager Jump action, activate the jump performed in FixedUpdate
    /// </summary>
    /// <param name="context"></param>
    private void Jump(InputAction.CallbackContext context)
    {
        if (_isGrounded && context.performed)
            _jump = true;
    }
    #endregion
}
