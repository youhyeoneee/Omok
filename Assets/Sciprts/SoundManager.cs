using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // 싱글톤
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

    // 오디오 클립들 
    [SerializeField] private AudioClip gameStartAudio; // 게임 시작 오디오 클립
    [SerializeField] private AudioClip badukAudio; // 바둑알 놓을 때 오디오 클립

    private AudioSource _audioSource;
    private GameManager _gameManager;
    
    void Start()
    {
        // 오디오 소스, 게임 매니저 초기화
        _audioSource = GetComponent<AudioSource>();
        _gameManager = GameManager.Instance;
    
        
        // delegate 할당 
        _gameManager.onPlay += PlayGameStateSound;

    }
    
    // 게임 스테이트에 맞는 사운드 
    private void PlayGameStateSound(GameState gameState)
    {
        switch (_gameManager.gameState)
        {
            // 게임 시작 시 
            case GameState.Start:
                _audioSource.PlayOneShot(gameStartAudio);
                StartCoroutine(WaitUntilSoundEnd());
                break;

        }

    }
    
    // 소리가 끝날 때까지 기다리는 코루틴
    private IEnumerator WaitUntilSoundEnd()
    {
        while (true)
        {
            yield return null;
            if (_audioSource.isPlaying == false)
            {
                // 게임 플레이 
                _gameManager.GamePlaying();
                break;
            }
        }
    }
    
    // 바둑알 놓기 
    public void PlayBadukSound()
    {
        _audioSource.PlayOneShot(badukAudio);
    }

}
