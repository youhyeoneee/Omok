using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private string playerName;

    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text nameText2;
    [SerializeField] private Image profileImage;
    [SerializeField] private Image timeImage;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private Image resultImg;
    [SerializeField] private Sprite winImg;


    private bool isTurn = false;
    private float time = 10.0f;
    private GameManager _gameManager;
    void Start()
    {
        nameText.text = playerName;
        nameText2.text = playerName;
        resultImg.gameObject.SetActive(false);

        _gameManager = GameManager.Instance;
    }

    public void Update()
    {
        if (_gameManager.isPlay)
        {
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
        }
        else
        {
            resultImg.gameObject.SetActive(true);
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

}
