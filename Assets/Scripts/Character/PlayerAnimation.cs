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
        animator.Play("sleep");             // 回合开始为 sleep 动画
        animator.SetBool("isSleep", true);
    }

    /// <summary>
    /// 玩家回合开始动画
    /// </summary>
    public void PlayerTurnBeginAnimation()
    {
        animator.SetBool("isSleep", false);     // 结束 sleep
        animator.SetBool("isParry", false);     // 结束 防御
    }

    /// <summary>
    /// 玩家回合结束动画
    /// </summary>
    public void PlayerTurnEndAnimation()
    {
        if(player.CurrentDefense > 0)
        {
            animator.SetBool("isParry", true);      // 开始 防御
        }
        else
        {
            animator.SetBool("isSleep", true);      // 开始 sleep
            animator.SetBool("isParry", false);     // 结束 防御
        }
    }

    /// <summary>
    /// 玩家弃牌时调用
    /// </summary>
    /// <param name="cardObj">卡牌对象</param>
    public void OnPlayerDiscardEvent(object cardObj)
    {
        Card card = cardObj as Card;

        switch (card.cardData.cardType)
        {
            case CardType.Attack:
                animator.SetTrigger("attack");      // 攻击动画
                break;
            case CardType.Defense:
            case CardType.Abilities:
                animator.SetTrigger("skill");       // 技能动画
                break;
            default:
                break;
        }
    }
}
