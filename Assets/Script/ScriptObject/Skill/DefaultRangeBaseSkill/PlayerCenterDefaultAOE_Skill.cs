using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Skill/DefaultRangeBaseSkill/PlayerCenterDefaultAOE")]
public class PlayerCenterDefaultAOE_Skill : DefaultRangeBaseSkill
{
   public int castRange = 1;
   public int skillDamage = 1;
    public override void BeingSkillEffect()
    {
      foreach (var _piece in GameManager.instance.PiecesInSkillArea)
        {
            _piece.GetHurt(skillDamage);
        }
        GameManager.instance.CloseAllRange();
        AudioManager.instance.audioMagic1.Play();
    }


    public override void Execute()
    {
        GameManager.instance.ShowEnemyInAOERange(castRange);
    }
}
