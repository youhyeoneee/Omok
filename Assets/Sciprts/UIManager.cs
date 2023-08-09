using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button gameStartButton;

    [Header("바둑판")] 
    [SerializeField] private Transform whiteBadukGroup;
    [SerializeField] private Transform blackBadukGroup;
    [SerializeField] private GameObject whiteBadukButton;
    [SerializeField] private GameObject blackbadukButton;

    [Header("메뉴")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject startImg;

    [SerializeField] private Player whitePlayer;
    [SerializeField] private Player blackPlayer;

    private float xPos;
    private float yPos;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        SetBadukButton();
        
        gameManager = GameManager.Instance;
        
        gameStartButton.onClick.AddListener(gameManager.GameStart);

        gameManager.playerChanged += ChangeBadukButton;
        gameManager.onPlay += ActivateMenu;
    }
    
    void SetBadukButton()
    {
        yPos = Constants.offset * (Constants.N/2);

        for (int i = 0; i <=  Constants.N; i++)
        {
            xPos = Constants.offset * - (Constants.N/2);
            for (int j = 0; j <= Constants.N; j++)
            {
                Vector3 pos = new Vector3(xPos, yPos, 0);
                GameObject newBadukWhite = Instantiate(whiteBadukButton, whiteBadukGroup);
                newBadukWhite.GetComponent<BadukButton>().y = i;
                newBadukWhite.GetComponent<BadukButton>().x = j;
                newBadukWhite.GetComponent<BadukButton>().color = Constants.White;
                newBadukWhite.transform.localPosition = pos;

                GameObject newBadukBlack = Instantiate(blackbadukButton, blackBadukGroup);
                newBadukBlack.GetComponent<BadukButton>().y = i;
                newBadukBlack.GetComponent<BadukButton>().x = j;
                newBadukBlack.GetComponent<BadukButton>().color = Constants.Black;
                newBadukBlack.transform.localPosition = pos;
                
                xPos += Constants.offset;
            }            
            yPos -= Constants.offset;
        }
        
        // 흑이 선
        ChangeBadukButton(Constants.Black);
    }
    
    void ChangeBadukButton(int color)
    {
        if (color == Constants.Black)
        {
            whiteBadukGroup.gameObject.SetActive(false);
            whitePlayer.DeActivate();

            blackBadukGroup.gameObject.SetActive(true);
            blackPlayer.Activate();
        }
        else if (color == Constants.White)
        {
            whiteBadukGroup.gameObject.SetActive(true);
            whitePlayer.Activate();

            blackBadukGroup.gameObject.SetActive(false);
            blackPlayer.DeActivate();
        }
    }

    void ActivateMenu(bool isPlay)
    {
        bool isReady = gameManager.IsPlayersReady();
        
        if (isPlay && isReady)
        {
            menuPanel.gameObject.SetActive(false);
            startImg.SetActive(false);
        }
        else
        {
            // 게임이 종료 되었을 경우
            if (isReady)
            {
                if (gameManager.winner == Constants.Black) blackPlayer.SetResultImg();
                else if (gameManager.winner == Constants.White) whitePlayer.SetResultImg();
                menuPanel.gameObject.SetActive(true);
            }
        }
    }
}
