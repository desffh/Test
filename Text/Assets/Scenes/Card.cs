using UnityEngine;

public class Card : MonoBehaviour
{
    public Sprite frontSprite; // ī���� �ո� �̹���
    public Sprite backSprite;  // ī���� �޸� �̹���

    private SpriteRenderer spriteRenderer;  // SpriteRenderer ������Ʈ
    private bool isFlipped = false;  // ī�尡 ���������� ����
    private GameManager gameController;  // GameController�� ����

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = backSprite;  // ī�� �޸����� �ʱ�ȭ

        gameController = GameObject.Find("GameManager").GetComponent<GameManager>(); // GameController�� ã��
    }

    void OnMouseDown()
    {
        if (!isFlipped && gameController.CanFlipCard())
        {
            FlipCard();
            gameController.CardFlipped(this);  // ī�尡 �������� �� GameController�� �˸�
        }
    }

    public void FlipCard()
    {
        isFlipped = !isFlipped;
        spriteRenderer.sprite = isFlipped ? frontSprite : backSprite;  // ī�� �ո�/�޸� ����
    }

    public void DestroyCard()
    {
        Destroy(gameObject);  // ī�� ����
    }

    public Sprite GetFrontSprite()
    {
        return frontSprite;  // ī���� �ո� ��������Ʈ ��ȯ
    }
}
