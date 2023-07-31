using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
   
    
    public delegate void PlayerChanged(bool player); 
    public PlayerChanged playerChanged;

    // GameManager 에서 사용 하는 데이터
    public bool player = false;

    private void Start()
    {
    }

    public void ChangePlayer()
    {
        player = !player;
        playerChanged.Invoke(player);
    }
}

