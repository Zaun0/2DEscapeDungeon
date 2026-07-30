using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private GameManager gameManager;   // Quản lý trạng thái game (thắng, thua, điểm số, UI)
    private AudioManager audioManager; // Quản lý âm thanh

    private void Awake()
    {
        // Tìm script GameManager & AudioManager có trong scene
        gameManager = FindAnyObjectByType<GameManager>();
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Xử lý va chạm dựa trên tag của object
        switch (collision.tag)
        {
            case "Soul": // Ăn linh hồn → tăng điểm
                CollectSoul(collision.gameObject);
                break;

            case "Trap": // Va chạm bẫy → thua game
            case "Enemy": // Va chạm quái → thua game
                gameManager.GameOver();
                break;

            case "Key": // Lấy chìa khóa → thắng level + mở khóa level mới
                CollectKey(collision.gameObject);
                break;
        }
    }

    // ------------------- HÀM XỬ LÝ -------------------

    // Khi ăn Soul
    private void CollectSoul(GameObject soul)
    {
        Destroy(soul);                     // Xóa object Soul khỏi scene
        audioManager.PlaySoulSound();      // Phát âm thanh ăn Soul
        gameManager.AddScore(1);           // Cộng điểm
    }

    // Khi lấy Key
    private void CollectKey(GameObject key)
    {
        Destroy(key);                      // Xóa chìa khóa khỏi scene
        UnlockNewLevel();                  // Mở khóa level tiếp theo
        gameManager.GameWin();             // Gọi UI thắng
    }

    // ------------------- MỞ KHÓA LEVEL -------------------
    private void UnlockNewLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;   // Level hiện tại
        int reachedIndex = PlayerPrefs.GetInt("ReachedIndex", 0);      // Level cao nhất đã đạt (lưu trong PlayerPrefs)

        // Nếu level hiện tại >= level cao nhất đã đạt → cập nhật tiến độ
        if (currentIndex >= reachedIndex)
        {
            PlayerPrefs.SetInt("ReachedIndex", currentIndex + 1);      // Lưu index của level tiếp theo
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1); // Tăng số level đã mở
            PlayerPrefs.Save();                                        // Lưu thay đổi
        }
    }
}
