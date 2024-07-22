using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Player player;
    private Animator animator;

    private void Awake()
    {
        player = GetComponent<Player>();
        animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        animator.Play("sleep");             // �غϿ�ʼΪ sleep ����
        animator.SetBool("isSleep", true);
    }

    /// <summary>
    /// ��һغϿ�ʼ����
    /// </summary>
    public void PlayerTurnBeginAnimation()
    {
        animator.SetBool("isSleep", false);     // ���� sleep
        animator.SetBool("isParry", false);     // ���� ����
    }

    /// <summary>
    /// ��һغϽ�������
    /// </summary>
    public void PlayerTurnEndAnimation()
    {
        if(player.CurrentDefense > 0)
        {
            animator.SetBool("isParry", true);      // ��ʼ ����
        }
        else
        {
            animator.SetBool("isSleep", true);      // ��ʼ sleep
            animator.SetBool("isParry", false);     // ���� ����
        }
    }

    /// <summary>
    /// �������ʱ����
    /// </summary>
    /// <param name="cardObj">���ƶ���</param>
    public void OnPlayerDiscardEvent(object cardObj)
    {
        Card card = cardObj as Card;

        switch (card.cardData.cardType)
        {
            case CardType.Attack:
                animator.SetTrigger("attack");      // ��������
                break;
            case CardType.Defense:
            case CardType.Abilities:
                animator.SetTrigger("skill");       // ���ܶ���
                break;
            default:
                break;
        }
    }
}
