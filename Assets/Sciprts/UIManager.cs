using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [Header("바둑판")] 
    [SerializeField] private Transform whiteBadukGroup;
    [SerializeField] private Transform blackBadukGroup;
    [SerializeField] private GameObject whiteBadukButton;
    [SerializeField] private GameObject blackbadukButton;
    [SerializeField] private int N = 18; // N * N 
    private float offset = 26.0f;
    private float xPos;
    private float yPos;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        SetBadukButton();
        
        gameManager = GameManager.Instance;
        gameManager.playerChanged += ChangeBadukButton;

    }
    
    void SetBadukButton()
    {
        yPos = offset * (N/2);

        for (int i = 0; i <= N; i++)
        {
            xPos = offset * - (N/2);
            for (int j = 0; j <= N; j++)
            {
                Vector3 pos = new Vector3(xPos, yPos, 0);
                GameObject newBadukWhite = Instantiate(whiteBadukButton, whiteBadukGroup);
                GameObject newBadukBlack = Instantiate(blackbadukButton, blackBadukGroup);
                newBadukWhite.transform.localPosition = pos;
                newBadukBlack.transform.localPosition = pos;
                xPos += offset;
            }            
            yPos -= offset;
        }
        
        // 흑이 선
        whiteBadukGroup.gameObject.SetActive(false);
        blackBadukGroup.gameObject.SetActive(true);
    }
    
    void ChangeBadukButton(bool player)
    {
        if (player)
        {
            Debug.Log("백");
            whiteBadukGroup.gameObject.SetActive(true);
            blackBadukGroup.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("흑");
            whiteBadukGroup.gameObject.SetActive(false);
            blackBadukGroup.gameObject.SetActive(true);
        }
    }
}
