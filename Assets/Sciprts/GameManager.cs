using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 상수들 
static class Constants
{
    public const int Black = 1;
    public const int White = 2;
    public const int N = 18; // N * N 
    public const float offset = 26.0f;
}

// 게임 상태
public enum GameState
{
    Ready,
    Start,
    Playing,
    End
}
public class GameManager : MonoBehaviour
{

    #region singleton
    //  Singleton Instance 선언
    private static GameManager instance = null;

    // Singleton Instance에 접근하기 위한 프로퍼티
    public static GameManager Instance
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
    
    // 플레이어 변경 delegate
    public delegate void PlayerChanged(int color);
    public PlayerChanged playerChanged;
    
    // 게임중인지 delegate 
    public delegate void OnPlay(GameState gameState);
    public OnPlay onPlay;

    
    public GameState   gameState { get; set; } // 현재 게임 상태
    public int         winner    { get; set; }  // 승리 플레이어
    public int         su        { get; set; }  // 현재 몇수인지 
    public BadukButton lastBaduk { get; set; }  // 마지막으로 놓은 바둑알 


    // 플레이어 
    public Player whitePlayer; // 백 
    public Player blackPlayer; // 흑 
    
    private int      _player; // 현재 플레이어 색깔
    private int[ , ] _map    = new int[Constants.N+4, Constants.N+4]; // 바둑판 배열 
    // 방향 벡터 : 오른쪽, 아래, 오아, 오위 대각선
    private int[]    _dy     = new int[] { 0, 1, 1, -1 };
    private int[]    _dx     = new int[] { 1, 0, 1, 1 }; 

    private void Start()
    {
        // 초기화 : 게임 상태, 플레이어, 바둑판 배열 
        gameState = GameState.Ready;
        _player = Constants.Black;
        su = 0;
        
        for (int i = 0; i < Constants.N+4; i++)
        {
            for (int j = 0; j < Constants.N+4; j++)
                _map[i, j] = 0;
        }
    }
    
    /// <summary>
    /// by 유현.
    /// [플레이어 변경 메서드]
    /// 현재 플레이어가 흑이라면 백, 백이라면 흑으로 변경한다.
    /// playerChanged 델리게이트를 호출한다.
    /// </summary>
    public void ChangePlayer()
    {
        if (_player == Constants.Black) _player = Constants.White;
        else if (_player == Constants.White) _player = Constants.Black;
        
        playerChanged.Invoke(_player);
    }

    /// <summary>
    /// by 유현.
    /// [바둑알 추가 메서드]
    /// 바둑판 2차원 배열 y열 x축에 color색의 바둑알을 놓고,
    /// 이전에 놓은 바둑알 놓임 표시를 비활성화하기 위해
    /// 마지막으로 놓은 바둑알(badukButton)을 저장한다.
    /// </summary>
    public void AddBaduk(int y, int x, int color, BadukButton badukButton)
    {
        // 이전 바둑알 놓임 표시 비활성화
        if (lastBaduk != null)
        {
            lastBaduk.triangleImg.SetActive(false);
        }
        lastBaduk = badukButton;
        
        // 수 증가 
        su++;

        // 바둑판 배열에 할당
        _map[y, x] = color;

        CheckGameEnd();
    }

    /// <summary>
    /// by유현.
    /// [게임 종료 체크 메서드]
    /// N*N 바둑판 배열을 돌면서
    /// 바둑알이 있다면 4방향을 오목인지 검사하고, 오목이라면 승리 메서드(Win)를 호출한다.
    /// </summary>
    private void CheckGameEnd()
    {
        for (int i = 0; i <= Constants.N; i++)
        {
            for (int j = 0; j <= Constants.N; j++)
            {
                // 바둑알이 놓여 있다면
                if (_map[i, j] != 0)
                {
                    // 4방향을 검사 
                    for (int dir = 0; dir < 4; dir++)
                    {
                        if (IsOmok(_map[i, j], i, j, dir))
                        {
                            // 6목인지 반대 방향의 한 알 체크 
                            int by = i - _dy[dir];
                            int bx = j - _dx[dir];

                            // 범위 안
                            if (by >= 0 || by <= Constants.N || bx >= 0 || bx <= Constants.N )
                            {
                                if (_map[by, bx] != _map[i, j]) 
                                {
                                    Win(_map[i, j]);
                                    return;
                                }
                            }
                            // 범위 밖 
                            else 
                            {
                                Win(_map[i, j]);
                                return;
                            }
                        }
                            
                    }
                }
            }
        }
    }

    /// <summary>
    /// by유현.
    /// [승리 메서드]
    /// 승자 플레이어를 지정하고 게임 오버 메서드(GameOver)를 호출한다.
    /// </summary>
    private void Win(int color)
    {
        winner = color;
        GameOver();
    }
    
    /// <summary>
    /// by유현.
    /// [오목 체크 메서드]
    /// y열 x축의 바둑알에서 dir방향으로 color 색깔이 끊기지 않고 이어지는 개수가 5개인지 bool 변수를 반환한다.
    /// </summary>
    private bool IsOmok(int color, int y, int x, int dir)
    {
        int cnt;
        for(cnt = 0; color == _map[y, x]; cnt++)
        {
            y += _dy[dir]; 
            x += _dx[dir];
            
            if (y < 0 || y > Constants.N || x < 0 || x > Constants.N)
                break;
        }
        
        return (cnt == 5);
    }
    
    /// <summary>
    /// by.유현
    /// [플레이어 준비 체크 메서드]
    /// 흑, 백 플레이어가 모두 준비 되었는지를 bool 변수를 반환한다.
    /// </summary>
    public bool IsPlayersReady()
    {
        return (whitePlayer.isReady && blackPlayer.isReady);
    }
    
    /// <summary>
    /// by 유현.
    /// [게임 시작 메서드]
    /// 플레이어가 모두 준비되었다면
    /// 게임 상태를 Start로 변경하고 onPlay 델리게이트를 호출한다.
    /// </summary>
    public void GameStart()
    {
        if (IsPlayersReady())
        {
            gameState = GameState.Start;
            onPlay.Invoke(gameState);
        }
    }
    
    /// <summary>
    /// by 유현.
    /// [게임 진행중 메서드]
    /// 게임 상태를 Playing으로 변경하고 onPlay 델리게이트를 호출한다.
    /// 흑 플레이어를 선으로 설정한다.
    /// </summary>
    public void GamePlaying()
    {
        gameState = GameState.Playing;
        onPlay.Invoke(gameState);

        whitePlayer.isTurn = false;
        blackPlayer.isTurn = true;
    }

    /// <summary>
    /// by 유현.
    /// [게임 종료 메서드]
    /// 게임 상태를 End로 변경하고 onPlay 델리게이트를 호출한다.
    /// </summary>
    private void GameOver()
    {
        gameState = GameState.End;
        onPlay.Invoke(gameState);
    }
}

