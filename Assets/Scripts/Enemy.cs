using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f;      // Tốc độ di chuyển
    [SerializeField] private float distance = 5f;   // Khoảng cách tuần tra

    private Vector3 startPos;   // Vị trí bắt đầu
    private bool movingRight = true; // Hướng di chuyển (true = phải, false = trái)

    void Start()
    {
        startPos = transform.position; // Ghi nhớ vị trí ban đầu
    }

    void Update()
    {
        // Giới hạn trái/phải
        float leftBound = startPos.x - distance;
        float rightBound = startPos.x + distance;

        // Xác định hướng di chuyển
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;

        // Di chuyển enemy
        transform.Translate(direction * speed * Time.deltaTime);

        // Đảo hướng khi chạm biên
        if (movingRight && transform.position.x >= rightBound)
        {
            movingRight = false;
            Flip();
        }
        else if (!movingRight && transform.position.x <= leftBound)
        {
            movingRight = true;
            Flip();
        }
    }

    // Đảo ngược hướng nhìn (flip sprite)
    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;              // Đổi dấu trục X
        transform.localScale = scale;
    }
}
