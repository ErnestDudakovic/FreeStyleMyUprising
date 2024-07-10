using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Enemy
{
    #region States

    public SkeletonIdleState idleState { get; private set; }
    public SkeletonMoveState moveState { get; private set; }
    public SkeletonBattleState battleState { get; private set; }
    public SkeletonAttackState attackState { get; private set; }

    public SkeletonStunnedState stunnedState { get; private set; }
    public SkeletonDeadState deadState { get; private set; }
    #endregion

    // Reference to the coin prefab
    public GameObject coinPrefab;

    protected override void Awake()
    {
        base.Awake();
        
        idleState = new SkeletonIdleState(this, stateMachine, "Idle", this);
        moveState = new SkeletonMoveState(this, stateMachine, "Move", this);
        battleState = new SkeletonBattleState(this, stateMachine, "Move", this);
        attackState = new SkeletonAttackState(this, stateMachine, "Attack", this);
        stunnedState = new SkeletonStunnedState(this, stateMachine, "Stunned", this);
        deadState = new SkeletonDeadState(this, stateMachine, "Idle", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.U))
        {
            stateMachine.ChangeState(stunnedState);
        }
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }

        return false;
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);

        // Determine how many coins to drop based on enemy tag
       if (coinPrefab != null && Random.value <= 0.15f)

        {
            if (CompareTag("Enemy_Skeleton"))
            {
                DropCoins(1, 0);
            }
            else if (CompareTag("Enemy_Skeleton_Red"))
            {
                DropCoins(5, 0.5f);
            }
            else if (CompareTag("Enemy_Skeleton_Blue"))
            {
                DropCoins(3, 0.5f);
            }
        }
        else
        {
            Debug.LogWarning("Coin prefab is not assigned in the inspector.");
        }
    }

    private void DropCoins(int numberOfCoins, float spread)
    {
        for (int i = 0; i < numberOfCoins; i++)
        {
            Vector3 coinPosition = transform.position + new Vector3(i * spread, 0, 0);
            Instantiate(coinPrefab, coinPosition, Quaternion.identity);
        }
    }
}
