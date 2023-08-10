using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadukButton : MonoBehaviour
{
    private Button button;
    private Transform fixedGroup;
    public int color;
    public int x;
    public int y;

    private GameManager  gameManager;
    private SoundManager soundManager;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetBaduk);
        fixedGroup = GameObject.Find("FixedGroup").transform;
        gameManager = GameManager.Instance;
        soundManager = SoundManager.Instance;
    }
    
    void SetBaduk()
    {
        // 버튼 비활성화 
        button.enabled = false;
        transform.SetParent(fixedGroup);
        
        // 오목 놓기
        gameManager.AddBaduk(y, x, color);
        
        // 사운드 재생
        soundManager.PlayBadukSound();
        
        // 플레이어 변경 
        gameManager.ChangePlayer();
    }
    
}
