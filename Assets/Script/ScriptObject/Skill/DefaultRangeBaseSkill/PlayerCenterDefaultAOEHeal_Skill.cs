using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Skill/DefaultRangeBaseSkill/AOEHeal")]
public class PlayerCenterDefaultAOEHeal_Skill : DefaultRangeBaseSkill
{
    public int castRange = 1;
    public int healAmount = 1;
    public override void BeingSkillEffect()
    {
        foreach (var _piece in GameManager.instance.PiecesInSkillArea)
        {
            _piece.GetHeal(healAmount);
        }
        GameManager.instance.CloseAllRange();
        AudioManager.instance.audioMagic1.Play();
    }

    public override void Execute()
    {
        GameManager.instance.ShowFriendlyInAOERange(castRange);
    }
}
