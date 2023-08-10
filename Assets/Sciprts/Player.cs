using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public string playerName { get; set; } //플레이어 이름 
    public bool   isReady    { get; set; } // 플레이어 준비 상태 
    public bool   isTurn     { get; set; }// 현재 자기 턴인지 

    // 플레이어 UI 
    [Header("Name")]
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private Button         nameSaveBtn;
    [SerializeField] private TMP_Text       nameText;
    [SerializeField] private TMP_Text       nameText2;
    
    [Header("Timer")]
    [SerializeField] private Image     timeImage;
    [SerializeField] private TMP_Text  timeText;
    private float _time; // 현재 플레이어 타이머에 표시되는 시간
    private float _oneTime = 10.0f; // 한 턴에 할당된 시간 

    [Header("Images")]
    [SerializeField] private Image  profileImage;
    [SerializeField] private Image  resultImg;
    [SerializeField] private Sprite winImg;
    
    private GameManager _gameManager; 
    
    void Start()
    {
        // 게임 매니저, 시간, 플레이어 준비 상태 초기화 
        isReady = false;
        _gameManager = GameManager.Instance;

        // 이름 저장 버튼 이벤트리스너 할당 
        nameSaveBtn.onClick.AddListener(SetName);
        
        // 결과 이미지 비활성화 
        resultImg.gameObject.SetActive(false);
    }

    public void Update()
    {
        switch (_gameManager.gameState)
        {
            case GameState.Start:
                _time = _oneTime;
                break;
            
            case GameState.Playing:
                // 자기 턴이라면 
                if (isTurn)
                {
                    // 시간 감소
                    _time -= Time.deltaTime;
                    timeText.text = $"00 : 00 : {Mathf.Ceil(_time).ToString("00")}";

                    // 시간 초과 시
                    if (_time <= 0)
                    {
                        // 플레이어 바꾸기 
                        _gameManager.ChangePlayer();
                    }
                }
                // 자기 턴이 아니라면 시간 초기화
                else _time = _oneTime;
                break;

            case GameState.End:
                // 결과 이미지 활성화 
                resultImg.gameObject.SetActive(true);
                break;
        }
    }

    // 플레이어 활성화 
    public void Activate()
    {
        isTurn = true;
        
        // 플레이어 이미지 테두리 활성화  
        profileImage.GetComponent<Outline>().enabled = true;
        
        // 타이머 활성화 
        timeImage.color = Color.black;
    }
    
    // 플레이어 비활성화 
    public void DeActivate()
    {
        isTurn = false;

        // 플레이어 이미지 테두리 활성화  
        profileImage.GetComponent<Outline>().enabled = false;
        
        // 타이머 비활성화 
        timeImage.color = Color.gray;
    }

    // 결과 이미지 => 승리 이미지로
    public void SetResultWinImg()
    {
        resultImg.sprite = winImg;
    }
    
    // 이름 설정
    private void SetName()
    {
        // 이름 설정 
        playerName = nameInput.text;
        nameText.text = playerName;
        nameText2.text = playerName;
        
        // UI 비활성화
        nameInput.gameObject.SetActive(false);
        nameSaveBtn.gameObject.SetActive(false);
        
        // 준비 완료 
        isReady = true;
    }
    
}
