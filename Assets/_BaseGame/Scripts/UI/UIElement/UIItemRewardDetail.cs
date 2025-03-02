using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIItemRewardDetail : MonoBehaviour
{
    [SerializeField] private Image m_ImgBG;
    [SerializeField] private Sprite[] sprBGs;
    [SerializeField] private Image m_ImgIcon;
    [SerializeField] private TextMeshProUGUI m_TxtAmount;
    [SerializeField] private GameResource m_ItemReward;


    public void Setup(GameResource itemReward)
    {
        m_ItemReward = itemReward;
        //m_ImgIcon.sprite = SpriteGlobalConfig.Instance.GetSpriteByItemType(m_ItemReward.ResourceType);
        m_TxtAmount.SetText($"<style=h6>{m_ItemReward.Amount.ToStringUI()}");
        //m_ImgIcon.sprite = SpriteGlobalConfig.Instance.GetSpriteByItemType(m_ItemReward.ResourceType, m_ItemReward.Amount);
    }

    public void SetShowParticle(bool isShow)
    {
        //gOParticle.SetActive(isShow);
    }
}
