using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	[Header("血量")]
    public int hpMax = 20;
    public int hp = 20;
    public int score = 100;
    [Header("速度")]
    public float walkSpeed = 0.1f;
    public float runSpeed = 0.15f;
	[Header("Boss招")]
    public float IntervalTime = 4;
	public int waves = 3;
	public GameObject bullet;
	[Header("行走时间")]
    public float walkTime = 2f;

	private Rigidbody2D body;
    private GameObject player;
	private BossState bossState = BossState.Idle;
	private Vector2 direction;
	private int theWave = 0;
    private float cdTime = 0;
	private float randomTime;


	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (GlobalSceneGame.Running)
        {
			cdTime+=Time.fixedDeltaTime;
			randomTime += Time.fixedDeltaTime;
			if(bossState != BossState.Dead){
				if(cdTime>=IntervalTime&&theWave<waves){
					bossState = BossState.Attack;
					Fire();
					cdTime -= 0.1f;
					theWave ++;
				}
				if(cdTime>=IntervalTime&&theWave>=waves){
					bossState = BossState.Attack;
					cdTime = 0;
					theWave = 0;
				}
				if(cdTime<IntervalTime){
            		bossState = (BossState)Random.Range((int)BossState.Idle, (int)BossState.Attack);
					if(bossState == BossState.Idle){
						Idle();
					}else{
						Walk();
					}
				}
			}else{
				Die();
			}
		}
	}
	
	private void Fire(){
		GameObject theBullet;
		var angle = Utils.GetAngleFromVector2(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized);
   		var outset = GetFirePosition(angle);
		float perangle = 360 / (36 - 1);
		for (int i = 0; i < 36; i++)
		{
			theBullet = Instantiate(bullet, outset, Quaternion.identity);
			theBullet.transform.Rotate(new Vector3(0, 0, angle - (36 - 1) / 2 * perangle + i * perangle));
			theBullet.GetComponent<Bullet>().SetBulletAttributes(25, angle - (36 - 1) / 2 * perangle + i * perangle, 1);
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
	private void Idle(){
		//none
	}
	private void Walk(){
		if (randomTime > walkTime)
        {
            direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            randomTime = 0;
        }
        Vector2 temp = (Vector2)transform.position + direction * walkSpeed;
        body.MovePosition(temp);
	}
	private void Die()
    {
        Destroy(gameObject);
        player.GetComponent<Player>().ChangeScore(score);
    }
    public void ChangeHp(int delta)
    {
        var tempHp = hp + delta;
        if (tempHp <= 0)
        {
            hp = 0;
			bossState = BossState.Dead;
        }
        else if (delta > 0)
            hp = tempHp > hpMax ? hpMax : tempHp;
        else
            hp = tempHp;
    }

}

public enum BossState
{
    Idle, Walk, Attack, Dead
}
