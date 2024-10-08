using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Transform groundCheck;

    private Rigidbody2D playerRb2D;
    private Animator playerAnimator;
    private float jumpForce;
    private float radius = 0.1f;
    private Vector3 initialPosition;

    private void Awake()
    {
        playerRb2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        jumpForce = playerData.initialJumpForce;
        initialPosition = transform.position;
    }
    private void Update()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, playerData.groundLayerMask);
                                                                                                            
        if (playerAnimator.GetBool("IsGrounded") != isGrounded)
        {
            playerAnimator.SetBool("IsGrounded", isGrounded);
        }

        if (Input.GetKeyDown(playerData.jumpKey))
        {
            if (isGrounded)
            {
                Jump();
            }
        }
    }

    private void Jump()
    {
        playerRb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

    }

    public void ResetPlayerPosition()
    {
        transform.position = initialPosition;
        playerRb2D.velocity = Vector2.zero;
    } 

    public void SetUpForce(float newUpForce)
    {
        jumpForce = newUpForce;
    }

    public float GetUpForce()
    {
        return jumpForce;
    } 

    public float GetInitialUpForce()
    {
        return playerData.initialJumpForce;
    }
}
