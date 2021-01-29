using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackSkill
{
    void UseSkillQuick(Transform player, LayerMask mask);
    void UseSkillCharged(Transform player, LayerMask mask);
    float ChargeTime();
    void ShowVisualFeedback(Transform player, float chargeTime);

}
