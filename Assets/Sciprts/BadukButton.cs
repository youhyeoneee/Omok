using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadukButton : MonoBehaviour
{
    
    // 위치 
    private int color; // 바둑알의 색깔
    private int x;
    private int y;

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

        
    /// <summary>
    /// by 유현.
    /// [바둑알의 위치와 색 할당하는 메서드]
    /// 바둑알의 위치를 y열 x축로, 색깔을 color로 설정한다. 
    /// </summary>
    /// <param name="color">Contants.Black, Constants.White 중 한가지를 int로 입력하세요. </param>
    public void SetPosColor(int y, int x, int color)
    {
        this.y = y;
        this.x = x;
        this.color = color;
    }
    
    
    /// <summary>
    /// by 유현.
    /// [바둑알 놓기 메서드]
    /// 바둑알이 놓이면 다시 클릭되지 않고,이미지가 비활성화 되지 않도록 하며 놓임 표시를 활성화한다. 
    /// SoundManager 클래스의 바둑알 놓는 사운드 재생 메서드(PlayBadukSound)를 호출하고 
    /// GameManager 클래스의 플레이어 변경 메서드(ChangePlayer)를 호출한다.
    /// </summary>
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
