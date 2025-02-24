using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public List<Card> cards = new List<Card>();  // ������ ī�� ���
    private List<Card> flippedCards = new List<Card>();  // �������� ī���

    private bool canFlip = true;  // ī�尡 ������ �� �ִ��� ����
    private float flipDelay = 1f;  // ������ ������ �ð�
    private float flipTimer = 0f;  // �ð� ������ Ÿ�̸�

    public TextMeshProUGUI victoryText;
    public int count;


    public GameObject cardPrefab;

    public Transform cardContainer;

    public Sprite[] cardSprites;
    void Start()
    {
        ShuffleCards();  // ī�� ����
        victoryText.gameObject.SetActive(false);

        count = cards.Count;

        CreateCards();
    }

    void Update()
    {
        if (flippedCards.Count == 2)
        {
            flipTimer += Time.deltaTime;  // Ÿ�̸� ����

            if (flipTimer >= flipDelay)  // 1�ʰ� ����ϸ� �� ����
            {
                flipTimer = 0f;  // Ÿ�̸� ����
                CheckForMatch();  // ī�� ��
            }
        }

        Debug.Log(count);

        if (count == 0)
        {
            ShowVictoryMessage();  // ī�尡 ��� ������� �¸� �޽��� ǥ��
        }

    }
    private void ShowVictoryMessage()
    {
        victoryText.gameObject.SetActive(true);  // Victory �ؽ�Ʈ Ȱ��ȭ
        victoryText.text = "VICTORY";  // "VICTORY" �ؽ�Ʈ ����
    }

    // ī�尡 �������� �� ó��
    public void CardFlipped(Card card)
    {
        flippedCards.Add(card);

        if (flippedCards.Count == 2)
        {
            canFlip = false;  // �� ī�尡 ���������� �� �̻� ī�带 ������ �� ������ ����
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



    // ī�尡 ������ �� �ִ��� ���θ� ��ȯ
    public bool CanFlipCard()
    {
        return canFlip;
    }

    // ī�� ����� �������� ����
    private void ShuffleCards()
    {
        // ī�尡 2���� �������� �Ͽ� 1���� 5���� ���ڰ� �� ���� ������ ����
        List<int> cardNumbers = new List<int>();

        for (int i = 1; i <= 5; i++)
        {
            cardNumbers.Add(i);
            cardNumbers.Add(i);
        }

        // ī�� ������ �������� ����
        for (int i = 0; i < cardNumbers.Count; i++)
        {
            int randomIndex = Random.Range(0, cardNumbers.Count);
            int temp = cardNumbers[i];
            cardNumbers[i] = cardNumbers[randomIndex];
            cardNumbers[randomIndex] = temp;
        }

        // cards ����Ʈ�� ī�� ������ �߰�
        cards.Clear();
        foreach (int num in cardNumbers)
        {
            Card card = new Card();
            card.frontSprite = cardSprites[num - 1];  // 1���� 5������ �´� ��������Ʈ ����
            cards.Add(card);
        }
    }
    // ī�� ���� �Լ�
    // ī�� �������� ȭ�鿡 �����ϴ� �Լ�
    // ī�� ���� �Լ�
    private void CreateCards()
    {
        // ���� ũ�� ���� (��: 4x5 ����)
        int gridColumns = 5;  // ���� ��

        float xOffset = 2f;  // X�� ����
        float yOffset = 3f;  // Y�� ����

        // ī�尡 �׸��忡 ���� �����ǵ��� ����
        for (int i = 0; i < cards.Count; i++)
        {
            // ī�� �������� �����ϰ� ��ġ�� ����
            GameObject cardObj = Instantiate(cardPrefab, cardContainer);

            Card cardPrefabBack = cardPrefab.GetComponent<Card>();

            Card cardComponent = cardObj.GetComponent<Card>();
            cardComponent.frontSprite = cards[i].frontSprite;
            cardComponent.backSprite = cardPrefabBack.backSprite;// �޸� �̹����� ù ��° ī�� �޸����� ����

            // �׸��� ��ġ ���
            int row = i / gridColumns;  // ���� ī�尡 �� ��° �࿡ ��ġ���� ���
            int col = i % gridColumns;  // ���� ī�尡 �� ��° ���� ��ġ���� ���

            // ī���� ��ġ ���� (���� ��ġ ���)
            cardObj.transform.position = new Vector3(
                col * xOffset,   // �� ������ �������� X��ǥ ���
                -row * yOffset,  // �� ������ �������� Y��ǥ ���
                0f               // Z���� 0���� ���� (2D�̹Ƿ�)
            );
        }
    }


}
