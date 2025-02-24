using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public List<Card> cards = new List<Card>();  // 게임의 카드 목록
    private List<Card> flippedCards = new List<Card>();  // 뒤집어진 카드들

    private bool canFlip = true;  // 카드가 뒤집힐 수 있는지 여부
    private float flipDelay = 1f;  // 뒤집기 딜레이 시간
    private float flipTimer = 0f;  // 시간 추적용 타이머

    public TextMeshProUGUI victoryText;
    public int count;


    public GameObject cardPrefab;

    public Transform cardContainer;

    public Sprite[] cardSprites;
    void Start()
    {
        ShuffleCards();  // 카드 섞기
        victoryText.gameObject.SetActive(false);

        count = cards.Count;

        CreateCards();
    }

    void Update()
    {
        if (flippedCards.Count == 2)
        {
            flipTimer += Time.deltaTime;  // 타이머 증가

            if (flipTimer >= flipDelay)  // 1초가 경과하면 비교 시작
            {
                flipTimer = 0f;  // 타이머 리셋
                CheckForMatch();  // 카드 비교
            }
        }

        Debug.Log(count);

        if (count == 0)
        {
            ShowVictoryMessage();  // 카드가 모두 사라지면 승리 메시지 표시
        }

    }
    private void ShowVictoryMessage()
    {
        victoryText.gameObject.SetActive(true);  // Victory 텍스트 활성화
        victoryText.text = "VICTORY";  // "VICTORY" 텍스트 설정
    }

    // 카드가 뒤집혔을 때 처리
    public void CardFlipped(Card card)
    {
        flippedCards.Add(card);

        if (flippedCards.Count == 2)
        {
            canFlip = false;  // 두 카드가 뒤집혔으면 더 이상 카드를 뒤집을 수 없도록 설정
        }
    }



    private void CheckForMatch()
    {
        if (flippedCards[0].GetFrontSprite() == flippedCards[1].GetFrontSprite())
        {

            flippedCards[0].DestroyCard();
            flippedCards[1].DestroyCard();
            count -= 2;
        }
        else
        {
            flippedCards[0].FlipCard();
            flippedCards[1].FlipCard();
        }

        flippedCards.Clear();
        canFlip = true;
    }



    // 카드가 뒤집힐 수 있는지 여부를 반환
    public bool CanFlipCard()
    {
        return canFlip;
    }

    // 카드 목록을 랜덤으로 섞기
    private void ShuffleCards()
    {
        // 카드가 2개씩 나오도록 하여 1부터 5까지 숫자가 두 번씩 나오게 설정
        List<int> cardNumbers = new List<int>();

        for (int i = 1; i <= 5; i++)
        {
            cardNumbers.Add(i);
            cardNumbers.Add(i);
        }

        // 카드 순서를 랜덤으로 섞음
        for (int i = 0; i < cardNumbers.Count; i++)
        {
            int randomIndex = Random.Range(0, cardNumbers.Count);
            int temp = cardNumbers[i];
            cardNumbers[i] = cardNumbers[randomIndex];
            cardNumbers[randomIndex] = temp;
        }

        // cards 리스트에 카드 데이터 추가
        cards.Clear();
        foreach (int num in cardNumbers)
        {
            Card card = new Card();
            card.frontSprite = cardSprites[num - 1];  // 1부터 5까지에 맞는 스프라이트 설정
            cards.Add(card);
        }
    }
    // 카드 생성 함수
    // 카드 프리팹을 화면에 생성하는 함수
    // 카드 생성 함수
    private void CreateCards()
    {
        // 격자 크기 설정 (예: 4x5 격자)
        int gridColumns = 5;  // 열의 수

        float xOffset = 2f;  // X축 간격
        float yOffset = 3f;  // Y축 간격

        // 카드가 그리드에 맞춰 생성되도록 설정
        for (int i = 0; i < cards.Count; i++)
        {
            // 카드 프리팹을 생성하고 위치를 설정
            GameObject cardObj = Instantiate(cardPrefab, cardContainer);

            Card cardPrefabBack = cardPrefab.GetComponent<Card>();

            Card cardComponent = cardObj.GetComponent<Card>();
            cardComponent.frontSprite = cards[i].frontSprite;
            cardComponent.backSprite = cardPrefabBack.backSprite;// 뒷면 이미지는 첫 번째 카드 뒷면으로 설정

            // 그리드 위치 계산
            int row = i / gridColumns;  // 현재 카드가 몇 번째 행에 위치할지 계산
            int col = i % gridColumns;  // 현재 카드가 몇 번째 열에 위치할지 계산

            // 카드의 위치 설정 (격자 위치 계산)
            cardObj.transform.position = new Vector3(
                col * xOffset,   // 열 간격을 기준으로 X좌표 계산
                -row * yOffset,  // 행 간격을 기준으로 Y좌표 계산
                0f               // Z축은 0으로 설정 (2D이므로)
            );
        }
    }


}
