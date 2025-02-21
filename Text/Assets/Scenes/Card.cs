using UnityEngine;

public class Card : MonoBehaviour
{
    public Sprite frontSprite; // 카드의 앞면 이미지
    public Sprite backSprite;  // 카드의 뒷면 이미지

    private SpriteRenderer spriteRenderer;  // SpriteRenderer 컴포넌트
    private bool isFlipped = false;  // 카드가 뒤집혔는지 여부
    private GameManager gameController;  // GameController를 참조

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = backSprite;  // 카드 뒷면으로 초기화

        gameController = GameObject.Find("GameManager").GetComponent<GameManager>(); // GameController를 찾음
    }

    void OnMouseDown()
    {
        if (!isFlipped && gameController.CanFlipCard())
        {
            FlipCard();
            gameController.CardFlipped(this);  // 카드가 뒤집혔을 때 GameController에 알림
        }
    }

    public void FlipCard()
    {
        isFlipped = !isFlipped;
        spriteRenderer.sprite = isFlipped ? frontSprite : backSprite;  // 카드 앞면/뒷면 변경
    }

    public void DestroyCard()
    {
        Destroy(gameObject);  // 카드 삭제
    }

    public Sprite GetFrontSprite()
    {
        return frontSprite;  // 카드의 앞면 스프라이트 반환
    }
}
