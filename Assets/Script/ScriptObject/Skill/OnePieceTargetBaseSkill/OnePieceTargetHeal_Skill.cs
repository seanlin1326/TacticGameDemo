using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Skill/OnePieceTargetBaseSkill/NormalHeal")]
public class Heal_Skill : OnePieceTargetBaseSkill
{
    public int healAmount = 1;
    public override void BeingSkillEffect(Piece _target)
    {
        //Debug.Log("Heal_Skill");
        _target.GetHeal(healAmount);
      
    }

    public override void Execute()
    {
       // Debug.Log("Heal");
        GameManager.instance.ShowFriendlyTargetSkillRange(skillRange);
    }
}
