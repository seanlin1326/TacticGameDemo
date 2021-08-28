using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Skill/OnePieceTargetBaseSkill/AttackOrHealthBuff")]
public class OneTargetBuffPiece_Skill : OnePieceTargetBaseSkill
{
   public int attackBuffNum = 0;
   public int HealthBuffNum = 0;
    public override void BeingSkillEffect(Piece _target)
    {
        _target.attackPower += attackBuffNum;
        _target.MaxHealth += HealthBuffNum;
        _target.health += HealthBuffNum;
        _target.pieceUI.SetPieceUI();
    }

    public override void Execute()
    {
        GameManager.instance.ShowFriendlyTargetSkillRange(skillRange);
    }
}
