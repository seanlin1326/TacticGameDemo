using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SkillTest/TestActualSkillB")]
public class TestActualSkillB : TestBaseSkill
{
    public override void Execute()
    {
        TestSkillManager.instance.QQ();
    }
}
