using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float upForce;
    [SerializeField] private float radius;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private KeyCode jump = KeyCode.Space;

    private Rigidbody2D playerRb2D;
    private Animator playerAnimator;

    private void Awake()
    {
        playerRb2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, groundLayerMask);//Detecta si hay un objeto dentro de un área circular en el mundo físico.
                                                                                                 //Verifica la colisión con el suelo. Si está en el suelo, puede saltar.
        if (playerAnimator.GetBool("IsGrounded") != isGrounded)
        {
            playerAnimator.SetBool("IsGrounded", isGrounded);
        }

        if (Input.GetKeyDown(jump))
        {
            if (isGrounded)
            {
                Jump();
            }
        }
    }

    private void Jump()
    {
        playerRb2D.AddForce(Vector2.up * upForce, ForceMode2D.Impulse);

    }
}
