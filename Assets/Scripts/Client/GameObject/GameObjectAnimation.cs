using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GameObjectAnimation : MonoBehaviour
{
    protected Animator _AgentAnimator;

    private void Awake()
    {
        _AgentAnimator = GetComponent<Animator>();
    }

    public void SetWalkAnimation(bool val)
    {        
        _AgentAnimator.SetBool("IsWalk", val);
    }

    public void AnimatePlayer(float velocity)
    {
        SetWalkAnimation(velocity > 0);
    }

    public void PlayDeathAnimation()
    {
        _AgentAnimator.SetTrigger("IsDeath");
    }

    public void SetWalkSpeed(int WalkSpeed)
    {
        _AgentAnimator.SetFloat("WalkMultiplier", WalkSpeed);
    }

    public void PlayWeaponMeleeAttack()
    {
        _AgentAnimator.SetTrigger("IsWeaponMeleeAttack");
    }    

    public void PlayCommonDamage()
    {
        _AgentAnimator.SetTrigger("IsCommonDamage");
    }
}
