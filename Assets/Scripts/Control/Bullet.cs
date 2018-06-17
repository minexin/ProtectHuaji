using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int owner;
    private float speed;
    private Vector3 direction;
    private int damage;
    private int colliderLayer;
    private int damageLayer;

    // Use this for initialization
    void Start()
    {
        SetColliderLayer();
    }

    void FixedUpdate()
    {
        if (GlobalSceneGame.Running)
        {
            if (DetectWallCollider())
            {
                Destroy(gameObject);
            }
            else if (CauseDamage())
            {
                Destroy(gameObject);
            }
            transform.Translate(Vector3.right * speed);
        }
    }

    public void SetBulletAttributes(float speed, float angle, int damage)
    {
        this.speed = speed * Time.fixedDeltaTime;
        direction = (Vector3)Utils.NormalizedVectorFromAngle(angle);
        this.damage = damage;
    }

    private bool DetectWallCollider()
    {
        return Physics2D.Raycast(transform.position, direction, speed, colliderLayer);
    }

    private void SetColliderLayer()
    {
        colliderLayer = 1 << 8;
        if(owner==0){
            damageLayer = 1 << 9;
        }else{
            damageLayer = 1 << 10;
        }
    }

    private bool CauseDamage()
    {
        var hit = Physics2D.Raycast(transform.position, direction, speed, damageLayer);
        if (hit == false) {
            return false;
        }
        else{
            if(owner==0)
            {
                if(hit.collider.gameObject.GetComponent<Monster>()==null){
                    hit.collider.gameObject.GetComponent<Boss>().ChangeHp(-damage);
                }else{
                    hit.collider.gameObject.GetComponent<Monster>().ChangeHp(-damage);
                }
            }else{
                hit.collider.gameObject.GetComponent<Player>().ChangeHp(-damage);
            }
        }
        return true;
    }
}
