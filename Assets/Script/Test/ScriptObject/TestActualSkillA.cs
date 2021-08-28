using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SkillTest/TestActualSkillA")]
public class TestActualSkillA : TestBaseSkill
{
    public override void Execute()
    {
        TestSkillManager.instance.HaHa();
    }
}
