using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float moveSpeed = 5f;          // Tốc độ di chuyển ngang
    [SerializeField] private float jumpForce = 15f;         // Lực nhảy
    [SerializeField] private LayerMask groundLayer;         // Layer mặt đất
    [SerializeField] private Transform groundCheck;         // Điểm check để biết Player có đang đứng đất không

    private Animator animator;      // Quản lý animation
    private Rigidbody2D rb;         // Điều khiển vật lý
    private GameManager gameManager;// Quản lý trạng thái game
    private AudioManager audioManager;// Quản lý âm thanh

    private bool isGrounded;        // Kiểm tra Player có đang chạm đất không

    private void Awake()
    {
        // Lấy component và tham chiếu các Manager
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindAnyObjectByType<GameManager>();
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    private void Update()
    {
        // Nếu game đã thắng hoặc thua → dừng điều khiển
        if (gameManager.IsGameOver() || gameManager.IsGameWin()) return;

        HandleMovement();   // Xử lý di chuyển ngang
        HandleJump();       // Xử lý nhảy
        UpdateAnimation();  // Cập nhật animation
    }

    // ------------------- XỬ LÝ DI CHUYỂN -------------------
    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal"); // Lấy input A/D hoặc ←/→
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Lật hướng nhân vật theo input
        if (moveInput != 0)
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
    }

    // ------------------- XỬ LÝ NHẢY -------------------
    private void HandleJump()
    {
        // Chỉ nhảy khi nhấn phím Jump và đang đứng trên mặt đất
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            audioManager.PlayJumpSound();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Check va chạm mặt đất bằng OverlapCircle
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    // ------------------- ANIMATION -------------------
    private void UpdateAnimation()
    {
        animator.SetBool("isWalking", Mathf.Abs(rb.linearVelocity.x) > 0.1f); // Đi bộ khi có tốc độ ngang
        animator.SetBool("isJumping", !isGrounded);                          // Nhảy khi không chạm đất
    }
}
