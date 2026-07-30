using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform pointA; // Điểm A (vị trí giới hạn 1 của platform)
    [SerializeField] private Transform pointB; // Điểm B (vị trí giới hạn 2 của platform)
    [SerializeField] private float speed = 2f; // Tốc độ di chuyển
    private Vector3 target; // Vị trí đích hiện tại

    void Start()
    {
        target = pointB.position; // Ban đầu cho platform đi về phía B
    }

    void Update()
    {
        // Di chuyển platform từ vị trí hiện tại về target với tốc độ speed
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Nếu platform đã đến target thì đổi hướng sang điểm còn lại
        if (transform.position == target)
        {
            target = (target == pointA.position) ? pointB.position : pointA.position;
        }
    }

    // Khi Player chạm vào platform → gắn Player làm con (child) của platform
    // Như vậy Player sẽ di chuyển theo platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.transform.SetParent(transform);
    }

    // Khi Player rời khỏi platform → bỏ quan hệ cha-con
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.transform.SetParent(null);
    }
}
