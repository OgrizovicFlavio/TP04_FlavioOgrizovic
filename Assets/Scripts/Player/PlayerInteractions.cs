using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [Header("Layer Masks")]
    [SerializeField] private LayerMask obstacleLayerMask;
    [SerializeField] private LayerMask powerUpJumpLayerMask;
    [Header("Trail Effects")]
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private RainbowTrail rainbowTrail;
    [SerializeField] private GameManager gameManager;

    private PlayerController playerJump;
    private PauseManager pauseManager;
    private Animator playerAnimator;
    private float powerUpTimer;
    private float playerUpForce;
    private float jumpBoostDuration = 5f;
    private int powerUpScorePoints = 100;
    private bool isPowerUpActive = false;

    private void Awake()
    {
        playerJump = GetComponent<PlayerController>();
        playerAnimator = GetComponent<Animator>();
        trailRenderer = GetComponent<TrailRenderer>();
        SetTrailColorWhite();
    }

    private void Update()
    {
        if (powerUpTimer > 0)
        {
            powerUpTimer -= Time.deltaTime;
            if (powerUpTimer <= 0)
            {
                playerJump.SetUpForce(playerJump.GetInitialUpForce());
                isPowerUpActive = false;
                SetTrailColorWhite();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Utilites.CheckLayerInMask(powerUpJumpLayerMask, other.gameObject.layer))
        {
            if (!isPowerUpActive) 
            {
                Debug.Log("¡Power Up de Salto activado por 5 segundos!");
                Debug.Log("¡Obtienes 100 puntos extra!");
                playerJump.SetUpForce(playerJump.GetUpForce() * 1.5f);
                rainbowTrail.ApplayRainbowEffect();
                gameManager.AddScore(powerUpScorePoints);
                powerUpTimer = jumpBoostDuration;
                isPowerUpActive = true;
            }

            other.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (Utilites.CheckLayerInMask(obstacleLayerMask, other.gameObject.layer))
        {
            Debug.Log("¡Chocaste con un obstáculo! Reiniciando el juego...");
            playerAnimator.SetTrigger("Die");
            gameManager.ResetGame();
            playerAnimator.SetTrigger("Idle");
        }
    }

    private void SetTrailColorWhite()
    {
        Gradient whiteGradient = new Gradient();
        whiteGradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(0.4f, 0.0f), new GradientAlphaKey(0.4f, 1.0f) }
        );

        trailRenderer.colorGradient = whiteGradient;
    }
}
