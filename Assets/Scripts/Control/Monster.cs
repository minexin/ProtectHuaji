using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("血量")]
    public int hpMax = 5;
    public int hp = 5;
    public int score = 10;
    [Header("速度")]
    public float walkSpeed = 0.1f;
    public float runSpeed = 0.15f;
    [Header("距离")]
    public float inspectDistance = 20;
    public float standAttackDistance = 15;
    [Header("武器")]
    public Gun gun;
    public int nearDamage = 1;
    public float nearInteval = 1.5f;
    [Header("行走时间")]
    public float walkTime = 4f;

    private GameObject player;
    private MonsterGenerator generator;
    private Rigidbody2D body;

    private MonsterState monsterState = MonsterState.Idle;
    private Vector2 direction;
    private float randomTime;
    private float cdTime = 0;

    void Start()
    {
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        body = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").gameObject;
    }

    public void SetGenerator(MonsterGenerator theGenerator)
    {
        generator=theGenerator;
    }
    void FixedUpdate()
    {
        if (GlobalSceneGame.Running)
        {
            cdTime += Time.fixedDeltaTime;
            randomTime += Time.fixedDeltaTime;
            if (monsterState != MonsterState.Dead)
            {
                ControlState();
                switch (monsterState)
                {
                    case MonsterState.Idle:
                        Idle();
                        break;
                    case MonsterState.Walk:
                        Walk();
                        break;
                    case MonsterState.Run:
                        Run();
                        break;
                    case MonsterState.Attack:
                        Attack();
                        break;
                }
            }
            else
            {
                Die();
            }
        }
    }

    void ControlState()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance > inspectDistance)
        {
            monsterState = (MonsterState)Random.Range((int)MonsterState.Idle, (int)MonsterState.Run);
        }
        else
        {
            if (distance > standAttackDistance)
                monsterState = MonsterState.Run;
            else
                monsterState = MonsterState.Attack;
        }
    }


    private void Idle()
    {
        //"什么都不干，思考人生"
    }
    private void Walk()
    {
        if (randomTime > walkTime)
        {
            direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            randomTime = 0;
        }
        Vector2 temp = (Vector2)transform.position + direction * walkSpeed;
        body.MovePosition(temp);
    }
    private void Run()
    {
        direction = (player.transform.position - transform.position).normalized;
        Vector2 temp = (Vector2)transform.position + direction * runSpeed;
        body.MovePosition(temp);
    }
    private void Attack()
    {
        direction = (player.transform.position - transform.position).normalized;
        var angle = Utils.GetAngleFromVector2(direction);
        var outset = GetFirePosition(angle);
        if(gun!=null){
            if (cdTime >= gun.fireInterval)
            {
                gun.Fire(outset, angle);
                cdTime = 0;
            }
        }else{
            //近战
            if (cdTime >= nearInteval)
            {
                body.MovePosition((Vector2)player.transform.position-direction);
                player.GetComponent<Player>().ChangeHp(-nearDamage);
                cdTime = 0;
            }
        }
    }
    private Vector3 GetFirePosition(float angle)
    {
        var offset = 1.5f;
        Vector3 monsterPosition = transform.position;
        Vector3 fireOffset = (Vector3)Utils.NormalizedVectorFromAngle(angle);
        fireOffset.x *= offset;
        fireOffset.y *= offset;
        return fireOffset + monsterPosition;
    }
    private void Die()
    {
        generator.NoticeKilled();//向生成器传达死亡信息
        Destroy(gameObject);
        player.GetComponent<Player>().ChangeScore(score);
    }
    public void ChangeHp(int delta)
    {
        var tempHp = hp + delta;
        if (tempHp <= 0)
        {
            hp = 0;
            monsterState = MonsterState.Dead;
        }
        else if (delta > 0)
            hp = tempHp > hpMax ? hpMax : tempHp;
        else
            hp = tempHp;
    }
}

public enum MonsterState
{
    Idle, Walk, Run, Attack, Dead
}
