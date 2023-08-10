using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadukButton : MonoBehaviour
{
    public int color { get; set; } // 바둑알의 색깔
    
    // 위치 
    public int x { get; set; }
    public int y { get; set; }

    public GameObject triangleImg;
    
    private Button       _button;
    private Transform    _fixedGroup;
    private GameManager  _gameManager;
    private SoundManager _soundManager;
    
    void Start()
    {
        // 게임 매니저, 사운드 매니저 초기화
        _gameManager = GameManager.Instance;
        _soundManager = SoundManager.Instance;
        
        // 바둑알 버튼 이벤트 리스너 할당
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SetBaduk);
        
        // 놓여진 바둑 그룹
        _fixedGroup = GameObject.Find("FixedGroup").transform;
        
        // 놓임 표시 이미지 비활성화 
        triangleImg.SetActive(false);
    }
    
    void SetBaduk()
    {
        // 버튼 비활성화 
        _button.enabled = false;
        transform.SetParent(_fixedGroup);
        
        // 바둑알 놓기
        _gameManager.AddBaduk(y, x, color, this);
        
        // 놓임 표시 이미지 활성화 
        triangleImg.SetActive(true);
        
        // 사운드 재생
        _soundManager.PlayBadukSound();
        
        // 플레이어 변경 
        _gameManager.ChangePlayer();
    }
    
}
