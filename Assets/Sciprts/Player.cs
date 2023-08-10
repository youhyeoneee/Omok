using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public bool isReady = false;
    [SerializeField] private string playerName;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private Button nameSaveBtn;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text nameText2;
    [SerializeField] private Image profileImage;
    [SerializeField] private Image timeImage;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private Image resultImg;
    [SerializeField] private Sprite winImg;


    private bool isTurn = false;
    private float time = 10.0f;
    private GameManager gameManager;
    void Start()
    {
        nameSaveBtn.onClick.AddListener(SetName);
        resultImg.gameObject.SetActive(false);

        gameManager = GameManager.Instance;
    }

    public void Update()
    {
        switch (gameManager.gameState)
        {
            case GameState.Playing:
                if (isTurn)
                {
                    time -= Time.deltaTime;
                    timeText.text = $"00 : 00 : {Mathf.Ceil(time).ToString("00")}";

                    // 시간 초과
                    if (time <= 0)
                    {
                        GameManager.Instance.ChangePlayer();
                    }
                }
                else time = 10.0f;

                break;
            case GameState.End:
                resultImg.gameObject.SetActive(true);
                break;
        }
    }

    public void Activate()
    {
        profileImage.GetComponent<Outline>().enabled = true;
        timeImage.color = Color.black;
        isTurn = true;
    }
    
    public void DeActivate()
    {
        profileImage.GetComponent<Outline>().enabled = false;
        timeImage.color = Color.gray;
        isTurn = false;
    }

    public void SetResultImg()
    {
        resultImg.sprite = winImg;
    }

    private void SetName()
    {
        // 이름 설정 
        playerName = nameInput.text;
        nameText.text = playerName;
        nameText2.text = playerName;
        
        // UI 비활성화
        nameInput.gameObject.SetActive(false);
        nameSaveBtn.gameObject.SetActive(false);
        
        // 준비
        isReady = true;
    }

}
