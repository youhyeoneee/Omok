using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadukButton : MonoBehaviour
{
    private Button _button;
    private Transform fixedGroup;
    public int color;
    public int x;
    public int y;

    private GameManager _gameManager;
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SetBaduk);
        fixedGroup = GameObject.Find("FixedGroup").transform;
        _gameManager = GameManager.Instance;

    }
    
    void SetBaduk()
    {
        // 버튼 비활성화 
        _button.enabled = false;
        transform.SetParent(fixedGroup);
        
        // 오목 놓기
        _gameManager.AddBaduk(y, x, color);
        
        // 플레이어 변경 
        _gameManager.ChangePlayer();
    }
    
}
