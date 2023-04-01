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

    public void SetWalkAnimation(bool Val)
    {        
        _AgentAnimator.SetBool("IsWalk", Val);
    }

    public void AnimatePlayer(float Velocity)
    {
        SetWalkAnimation(Velocity > 0);
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

    public void PlayDeadAnimation(bool IsLeftRight)
    {
        if(IsLeftRight == true)
        {
            _AgentAnimator.SetTrigger("IsRightDead");
        }
        else
        {
            _AgentAnimator.SetTrigger("IsLeftDead");
        }        
    }    
}
