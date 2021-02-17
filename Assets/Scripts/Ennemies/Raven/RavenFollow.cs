﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavenFollow : StateMachineBehaviour
{
    private Raven raven;
    private float thrustMult = 4.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        raven = animator.GetComponent<Raven>();
        raven.MoveTo = raven.player.transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        raven.MoveTo = raven.player.transform;

        if (raven.IsColliding())
        {
            raven.SetCollidingTimer(raven.GetCollidingTimer() - Time.deltaTime);
        }

        if (raven.GetCollidingTimer() <= 0f)
        {
            raven.ResetCollidingTimer();
            raven.SetColliding(false);
        }

        if (raven.rb2D.velocity.magnitude > raven.GetMagnitudeMax())
        {
            raven.rb2D.velocity *= 1 - ((raven.rb2D.velocity.magnitude / raven.GetMagnitudeMax()) - 1f);
        }

        if (!raven.IsColliding())
        {
            Vector3 direction = raven.MoveTo.position - raven.transform.position;
            raven.rb2D.AddForce(direction.normalized * raven.thrust * thrustMult, ForceMode2D.Force);
        }

        raven.FlipSprite();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
