using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("바둑판")] 
    [SerializeField] private Transform  whiteBadukGroup; // 백 바둑알들 부모 객체
    [SerializeField] private Transform  blackBadukGroup; // 흑 바둑알들 부모 객체
    [SerializeField] private GameObject whiteBadukButton; // 백 바둑알 프리팹
    [SerializeField] private GameObject blackbadukButton; // 흑 바둑알 프리팹

    [Header("메뉴")]
    [SerializeField] private Button     gameStartButton; // 게임 시작 버튼
    [SerializeField] private GameObject menuPanel; // 메뉴 패널
    [SerializeField] private GameObject startImg; // 게임 시작 이미지 
    [SerializeField] private TMP_Text   resultText; // 결과 텍스트 
    [SerializeField] private TMP_Text   suText; // 수 텍스트 

    private GameManager  _gameManager;
    private Player       _whitePlayer;
    private Player       _blackPlayer;

    void Start()
    {
        // 게임매니저, 플레이어 초기화 
        _gameManager = GameManager.Instance;
        _whitePlayer = _gameManager.whitePlayer;
        _blackPlayer = _gameManager.blackPlayer;

        // delegate 할당 
        _gameManager.playerChanged += ChangeBadukButton;
        _gameManager.onPlay += ActivateMenu;
        
        // 게임 시작 버튼 이벤트리스너 초기화 
        gameStartButton.onClick.AddListener(_gameManager.GameStart);

        // 바둑판 버튼 세팅
        SetBadukButton();
    }
    
    /// <summary>
    /// 바둑판 버튼 세팅 : N*N 배열에 흑, 백 버튼 세팅
    /// </summary>
    void SetBadukButton()
    {
        float yPos = Constants.offset * (Constants.N/2);
        float xPos;
        
        for (int i = 0; i <=  Constants.N; i++)
        {
            xPos = Constants.offset * - (Constants.N/2);
            for (int j = 0; j <= Constants.N; j++)
            {
                Vector3 pos = new Vector3(xPos, yPos, 0);
                GameObject newBadukWhite = Instantiate(whiteBadukButton, whiteBadukGroup);
                newBadukWhite.GetComponent<BadukButton>().SetPosColor(i, j, Constants.White);
                newBadukWhite.transform.localPosition = pos;

                GameObject newBadukBlack = Instantiate(blackbadukButton, blackBadukGroup);
                newBadukBlack.GetComponent<BadukButton>().SetPosColor(i, j, Constants.Black);
                newBadukBlack.transform.localPosition = pos;
                
                xPos += Constants.offset;
            }            
            yPos -= Constants.offset;
        }
        
        // 흑이 선
        ChangeBadukButton(Constants.Black);
    }
    
    /// <summary>
    /// 바둑판 버튼 변경 : 현재 플레이어에 따라 알맞은 바둑판 버튼 활성화 
    /// </summary>
    void ChangeBadukButton(int color)
    {
        suText.text = $"{_gameManager.su}수";

        if (color == Constants.Black)
        {
            whiteBadukGroup.gameObject.SetActive(false);
            _whitePlayer.DeActivate();

            blackBadukGroup.gameObject.SetActive(true);
            _blackPlayer.Activate();
        }
        else if (color == Constants.White)
        {
            whiteBadukGroup.gameObject.SetActive(true);
            _whitePlayer.Activate();

            blackBadukGroup.gameObject.SetActive(false);
            _blackPlayer.DeActivate();
        }
    }

    /// <summary>
    /// 메뉴 활성화 : 이미지, 텍스트 등 게임 상태에 따라 메뉴 활성화 
    /// </summary>
    void ActivateMenu(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Start:
                startImg.SetActive(false);
                break;
            
            case GameState.Playing:
                menuPanel.gameObject.SetActive(false);
                break;
            
            case GameState.End:
                menuPanel.gameObject.SetActive(true);
                // 게임 결과 텍스트 
                string winnerName = "";
                if (_gameManager.winner == Constants.Black)
                {
                    _blackPlayer.SetResultWinImg();
                    winnerName = _blackPlayer.playerName;
                }
                else if (_gameManager.winner == Constants.White)
                {
                    _whitePlayer.SetResultWinImg();
                    winnerName = _whitePlayer.playerName;
                }
                resultText.text = $"'{winnerName}'님께서 승리 하셨습니다.";
                resultText.gameObject.SetActive(true);
                break;
        }
    }
}
