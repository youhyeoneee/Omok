using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Constants
{
    public const int Black = 1;
    public const int White = 2;
    public const int N = 18; // N * N 
    public const float offset = 26.0f;
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
   
    
    public delegate void PlayerChanged(int color);
    public PlayerChanged playerChanged;
    
    public delegate void OnPlay(bool isPlay);
    public OnPlay onPlay;
    
    public bool isPlay = false;
    public int winner;
    

    // GameManager 에서 사용 하는 데이터
    private int player = Constants.Black;
    
    public int[ , ] map = new int[Constants.N+4, Constants.N+4];
    private int[] dy = new int[] { -1, 0, 1, -1 };
    private int[] dx = new int[] { 0, 1, 1, 1 };

    private void Start()
    {
        player = Constants.Black;
        
        // 초기화
        for (int i = 0; i < Constants.N+4; i++)
        {
            for (int j = 0; j < Constants.N+4; j++)
                map[i, j] = 0;
        }
        
        isPlay = true;
        onPlay.Invoke(isPlay);

    }
    
    public void ChangePlayer()
    {
        if (player == Constants.Black) player = Constants.White;
        else if (player == Constants.White) player = Constants.Black;
        
        playerChanged.Invoke(player);
    }

    public void AddBaduk(int y, int x, int color)
    {
        map[y, x] = color;
        CheckGameEnd();
    }

    private void CheckGameEnd()
    {
        for (int i = 0; i <= Constants.N; i++)
        {
            for (int j = 0; j <= Constants.N; j++)
            {
                if (map[i, j] != 0)
                {
                    for (int dir = 0; dir < 4; dir++)
                    {
                        if (IsOmok(map[i, j], i, j, dir))
                        {
                            winner = map[i, j];
                            Debug.Log($"오목 {map[i, j]} 승리");
                            GameOver();
                            return;
                        }
                            
                    }
                }
            }
        }
    }
    private bool IsOmok(int color, int y, int x, int dir)
    {
        int cnt;
        for(cnt = 0; color == map[y, x]; cnt++)
        {
            y += dy[dir]; 
            x += dx[dir];
            
            if (y < 0 || y > Constants.N || x < 0 || x > Constants.N)
                break;
        }
        
        return (cnt == 5);
    }
    
    private void GameOver()
    {
        isPlay = false;
        onPlay.Invoke(isPlay);
    }
}

