using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float upForce;
    private Rigidbody2D playerRb2D;

    private void Awake()
    {
        playerRb2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        playerRb2D.AddForce(Vector2.up * upForce, ForceMode2D.Impulse);

    }
}
