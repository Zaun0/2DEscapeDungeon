using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int score = 0;                                   // Điểm số hiện tại
    [SerializeField] private TextMeshProUGUI scoreText;      // Text hiển thị điểm
    [SerializeField] private GameObject gameOverUI;          // UI Game Over
    [SerializeField] private GameObject gameWinUI;           // UI Game Win
    [SerializeField] private GameObject pauseUI;             // UI Pause

    private bool isGameOver = false;                         // Trạng thái thua
    private bool isGameWin = false;                          // Trạng thái thắng

    void Start()
    {
        UpdateScore();                                       // Cập nhật điểm ban đầu
        gameOverUI.SetActive(false);                         // Ẩn UI thua
        gameWinUI.SetActive(false);                          // Ẩn UI thắng
    }

    // Thêm điểm
    public void AddScore(int points)
    {
        if (!isGameOver && !isGameWin)                       // Chỉ cộng điểm khi đang chơi
        {
            score += points;
            UpdateScore();
        }
    }

    // Cập nhật text điểm
    private void UpdateScore() => scoreText.text = score.ToString();

    // Tạm dừng game
    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0;
    }

    // Tiếp tục game
    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    // Xử lý khi thua
    public void GameOver()
    {
        isGameOver = true;
        score = 0;                                           // Reset điểm
        Time.timeScale = 0;                                  // Dừng game
        gameOverUI.SetActive(true);
    }

    // Xử lý khi thắng
    public void GameWin()
    {
        isGameWin = true;
        Time.timeScale = 0;
        gameWinUI.SetActive(true);
    }

    // Chơi lại màn hiện tại
    public void RestartGame()
    {
        isGameOver = false;
        score = 0;
        UpdateScore();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Về menu chính
    public void GotoMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    // Qua màn tiếp theo
    public void NextLevel()
    {
        isGameWin = false;
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Load scene bất kỳ theo tên
    public void LoadScene(string sceneName) => SceneManager.LoadSceneAsync(sceneName);

    // Kiểm tra trạng thái game
    public bool IsGameOver() => isGameOver;
    public bool IsGameWin() => isGameWin;
}
