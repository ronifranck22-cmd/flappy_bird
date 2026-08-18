using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro; 

public class BirdController : MonoBehaviour
{
    public float jumpForce = 5f;
    public GameObject gameOverPanel;
    public GameObject startScreenPanel;
    public GameObject scoreTextToHide;
    public PipeManager pipeManager;

    [Header("Score UI")]
    public TextMeshProUGUI finalScoreText; 
    public TextMeshProUGUI bestScoreText;  

    public AudioClip jumpSound;
    public AudioClip scoreSound;
    public AudioClip deathSound;
    private AudioSource audioSource;

    private Rigidbody2D rb;
    private bool gameStarted = false;
    private bool isDead = false;
    private float startX; 
    private int score = 0; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>(); 
        startX = transform.position.x; 
        
        rb.gravityScale = 0f;
        if (pipeManager != null) pipeManager.enabled = false;
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (startScreenPanel != null) startScreenPanel.SetActive(true);

        if (scoreTextToHide != null) scoreTextToHide.SetActive(false);

    }

    void Update()
    {
        if (isDead) return;

        bool jumpPressed = (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame) ||
                           (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame);

        if (jumpPressed)
        {
            if (!gameStarted)
            {
                StartGame();
            }
            else
            {
                Jump();
            }
        }
    }

    void StartGame()
    {
        gameStarted = true;
        rb.gravityScale = 3f;
        if (startScreenPanel != null) startScreenPanel.SetActive(false);
        if (pipeManager != null) pipeManager.enabled = true;

        if (scoreTextToHide != null) scoreTextToHide.SetActive(true); 

        Jump();
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        if (audioSource != null && jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }

    void LateUpdate()
    {
        if (!isDead)
        {
            transform.position = new Vector3(startX, transform.position.y, transform.position.z);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead && (collision.CompareTag("Pipe") || collision.CompareTag("Boundary") || collision.gameObject.name == "BottomBoundary"))
        {
            StartCoroutine(HandleDeath());
        }
    }

    IEnumerator HandleDeath()
    {
        isDead = true;
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        enabled = false;
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 3f;
        }

        if (pipeManager != null)
        {
            pipeManager.enabled = false;
        }

        PipeMover[] movers = FindObjectsByType<PipeMover>(FindObjectsSortMode.None);
        foreach (PipeMover mover in movers)
        {
            mover.enabled = false;
        }

        if (scoreTextToHide != null)
        {
            scoreTextToHide.SetActive(false);
        }

        int currentScore = ScoreManager.score;
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        if (finalScoreText != null) finalScoreText.text = currentScore.ToString();
        if (bestScoreText != null) bestScoreText.text = highScore.ToString();
        // ---------------------------------------

        yield return new WaitForSeconds(1.2f);

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        Time.timeScale = 0f;
    }
}