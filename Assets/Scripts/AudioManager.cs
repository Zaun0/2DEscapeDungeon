using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Nguồn phát nhạc nền
    [SerializeField] private AudioSource backgroundAudioSource;
    // Nguồn phát hiệu ứng âm thanh
    [SerializeField] private AudioSource effectAudioSource;

    // Clip nhạc nền
    [SerializeField] private AudioClip backGroundClip;
    // Clip âm thanh khi nhảy
    [SerializeField] private AudioClip jumpClip;
    // Clip âm thanh khi nhặt Soul
    [SerializeField] private AudioClip soulClip;

    void Start()
    {
        PlayBackGroundMusic(); // Tự động phát nhạc nền khi game bắt đầu
    }

    // Phát nhạc nền (loop liên tục)
    public void PlayBackGroundMusic()
    {
        backgroundAudioSource.clip = backGroundClip;
        backgroundAudioSource.Play();
    }

    // Phát âm thanh Soul (mỗi lần gọi sẽ phát 1 lần)
    public void PlaySoulSound()
    {
        effectAudioSource.PlayOneShot(soulClip);
    }

    // Phát âm thanh khi nhảy
    public void PlayJumpSound()
    {
        effectAudioSource.PlayOneShot(jumpClip);
    }
}
