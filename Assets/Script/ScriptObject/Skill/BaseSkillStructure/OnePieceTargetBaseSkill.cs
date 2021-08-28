using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnePieceTargetBaseSkill : Skill
{
   
    public int skillRange = 1;
    public abstract void BeingSkillEffect(Piece _target);




}
