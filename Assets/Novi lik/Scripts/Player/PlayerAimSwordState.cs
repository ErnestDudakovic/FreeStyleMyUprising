using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
    public PlayerAimSwordState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.skill.sword.DotsActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", .2f);
    }

    public override void Update()
    {
        base.Update();

        player.SetZeroVelocity();

        // Check if the cooldown timer is active before handling right-click input
        if (player.skill.sword.cooldownTimer <= 0f)
        {
            if (Input.GetKeyUp(KeyCode.Mouse1))
                stateMachine.ChangeState(player.idleState);
        }

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (player.transform.position.x > mousePosition.x && player.facingDir == 1)
            player.Flip();
        else if(player.transform.position.x < mousePosition.x && player.facingDir == -1)
            player.Flip();
    }
}
