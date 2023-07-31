using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadukButton : MonoBehaviour
{
    private Button _button;
    private Transform fixedGroup;

    [SerializeField] private bool isWhite = true;
    
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(DeactivateButton);

        string color;
        if (isWhite)
            color = "white";
        else
            color = "black";
        
        fixedGroup = GameObject.Find(color + "BadukFixedGroup").transform;

    }
    
    void DeactivateButton()
    {
        // 버튼 비활성화 
        _button.enabled = false;
        
        transform.SetParent(fixedGroup);
        // 플레이어 변경 
        GameManager.Instance.ChangePlayer();
    }
    
    void Update()
    {
        
    }
}
