using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    #region singleton
    //  Singleton Instance 선언
    private static SoundManager instance = null;

    // Singleton Instance에 접근하기 위한 프로퍼티
    public static SoundManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        // Scene에 이미 인스턴스가 존재 하는지 확인 후 처리
        if(instance)
        {
            Destroy(this.gameObject);
            return;
        }

        // instance를 유일 오브젝트로 만든다
        instance = this;

        // Scene 이동 시 삭제 되지 않도록 처리
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    
    [SerializeField] private AudioClip gameStartAudio;
    [SerializeField] private AudioClip badukAudio;

    private AudioSource audioSource;
    private GameManager gameManager;
    public bool isGameStart = false;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = GameManager.Instance;

        gameManager.onPlay += PlayGameStateSound;

    }
    private void PlayGameStateSound(GameState gameState)
    {
        switch (gameManager.gameState)
        {
            case GameState.Start:
                audioSource.PlayOneShot(gameStartAudio);
                StartCoroutine(WaitUntilSoundEnd());
                break;

        }

    }

    public void PlayBadukSound()
    {
        audioSource.PlayOneShot(badukAudio);

    }
    public IEnumerator WaitUntilSoundEnd()
    {
        while (true)
        {
            yield return null;
            if (audioSource.isPlaying == false)
            {
                gameManager.GamePlaying();
                break;
            }
        }
    }
}
