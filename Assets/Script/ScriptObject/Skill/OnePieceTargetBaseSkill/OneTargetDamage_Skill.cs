using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Skill/OnePieceTargetBaseSkill/OneTargetDamageSkill")]
public class OneTargetDamage_Skill : OnePieceTargetBaseSkill
{
    
    public int skillDamage = 1;
    public override void BeingSkillEffect(Piece _target)
    {
        _target.GetHurt(skillDamage);
    }

    public override void Execute()
    {
        //SkillManager.instance.DoubleSword();
        Debug.Log("Ice");
        GameManager.instance.ShowEnemyTargetSkillRange(skillRange);
    }
}
