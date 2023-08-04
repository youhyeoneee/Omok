using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [Header("바둑판")] 
    [SerializeField] private Transform whiteBadukGroup;
    [SerializeField] private Transform blackBadukGroup;
    [SerializeField] private GameObject whiteBadukButton;
    [SerializeField] private GameObject blackbadukButton;

    [Header("메뉴")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private TMP_Text  resultText;

    private float xPos;
    private float yPos;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        SetBadukButton();
        
        gameManager = GameManager.Instance;
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
        whiteBadukGroup.gameObject.SetActive(false);
        blackBadukGroup.gameObject.SetActive(true);
    }
    

    void ChangeBadukButton(bool player)
    {
        if (player)
        {
            whiteBadukGroup.gameObject.SetActive(true);
            blackBadukGroup.gameObject.SetActive(false);
        }
        else
        {
            whiteBadukGroup.gameObject.SetActive(false);
            blackBadukGroup.gameObject.SetActive(true);
        }
    }

    void ActivateMenu(bool isPlay)
    {
        if (isPlay)
        {
            menuPanel.gameObject.SetActive(false);
        }
        else
        {
            if (gameManager.winner == Constants.Black) resultText.text = "BLACK IS WIN";
            else if (gameManager.winner == Constants.White) resultText.text =  "WHITE IS WIN";
            menuPanel.gameObject.SetActive(true);
        }
    }
}
