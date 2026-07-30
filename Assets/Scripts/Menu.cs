using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Menu : MonoBehaviour
{
    [Header("Âm thanh")]
    public AudioMixer audioMixer;      // Điều khiển âm lượng
    public Slider musicSlider;         // Thanh chỉnh nhạc nền
    public Slider sfxSlider;           // Thanh chỉnh hiệu ứng âm thanh

    [Header("Level Buttons")]
    public Button[] buttons;           // Các nút chọn level

    private void Awake()
    {
        // Lấy level đã mở từ PlayerPrefs (mặc định là 1)
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        // Ban đầu disable hết nút
        foreach (Button btn in buttons)
            btn.interactable = false;

        // Chỉ mở các level đã được unlock
        for (int i = 0; i < unlockedLevel && i < buttons.Length; i++)
            buttons[i].interactable = true;
    }

    private void Start()
    {
        LoadVolume(); // Load âm lượng đã lưu
    }

    // Nút "Play" mặc định vào Level 1
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    // Mở level theo ID (1 = Game, 2 = GameLv2, 3 = GameLv3)
    public void OpenLevel(int levelId)
    {
        string levelName = levelId switch
        {
            1 => "Game",
            2 => "GameLv2",
            3 => "GameLv3",
            _ => "Game"
        };

        SceneManager.LoadScene(levelName);
    }

    // Thoát game
    public void QuitGame()
    {
        UnityEngine.Application.Quit(); // Thoát khi build game
    }

    // Cập nhật nhạc nền
    public void UpdateMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
        SaveVolume();
    }

    // Cập nhật hiệu ứng âm thanh
    public void UpdateSoundVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
        SaveVolume();
    }

    // Lưu âm lượng vào PlayerPrefs
    private void SaveVolume()
    {
        audioMixer.GetFloat("MusicVolume", out float musicVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);

        audioMixer.GetFloat("SFXVolume", out float sfxVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);

        PlayerPrefs.Save();
    }

    // Load âm lượng đã lưu (mặc định = 0 nếu chưa có)
    private void LoadVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0f);

        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;

        audioMixer.SetFloat("MusicVolume", musicVolume);
        audioMixer.SetFloat("SFXVolume", sfxVolume);
    }
}
