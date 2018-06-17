using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public float bulletSpeed = 0.2f;
    public float fireInterval = 0.5f;
    public int damage = 1;
    public GameObject bullet;
    public AttackMode attackMode = 0;

    private int locked = 0;
    private float lockedFireInterval = 0.5f;

    public enum AttackMode { Single, Multiple, SingleShotgun, MultipleShotgun }

    public void Fire(Vector3 outset, float angle)
    {
        switch (attackMode)
        {
            case AttackMode.Single:
                SingleShot(outset, angle);
                break;
            case AttackMode.Multiple:
                MultipleShot(outset, angle, 2);
                break;
            case AttackMode.SingleShotgun:
                SingleShotgun(outset, angle, 3, 30);
                break;
            case AttackMode.MultipleShotgun:
                MultipleShotgun(outset, angle, 3, 30, 2);
                break;
            default:
                break;
        }
    }

    private void SingleShot(Vector3 outset, float angle)
    {
        GameObject theBullet = Instantiate(bullet, outset, Quaternion.identity);
        theBullet.transform.Rotate(new Vector3(0, 0, angle));
        theBullet.GetComponent<Bullet>().SetBulletAttributes(bulletSpeed, angle, damage);
    }

    private void MultipleShot(Vector3 outset, float angle, int bullets)
    {
        SingleShot(outset, angle);
        if (locked % bullets == 0)
        {
            fireInterval = 0.1f;
        }
        if (locked % bullets == bullets - 1)
        {
            fireInterval = lockedFireInterval;
        }
        locked = locked % bullets + 1;
    }

    private void SingleShotgun(Vector3 outset, float angle, int bullets, float attackAngle)
    {
        GameObject theBullet;
        float perangle = attackAngle / (bullets - 1);
        for (int i = 0; i < bullets; i++)
        {
            theBullet = Instantiate(bullet, outset, Quaternion.identity);
            theBullet.transform.Rotate(new Vector3(0, 0, angle - (bullets - 1) / 2 * perangle + i * perangle));
            theBullet.GetComponent<Bullet>().SetBulletAttributes(bulletSpeed, angle - (bullets - 1) / 2 * perangle + i * perangle, damage);
        }
    }

    private void MultipleShotgun(Vector3 outset, float angle, int bulletsPerWave, float attackAngle, int waves)
    {
        SingleShotgun(outset, angle, bulletsPerWave, attackAngle);
        if (locked % waves == 0)
        {
            fireInterval = 0.1f;
        }
        if (locked % waves == waves - 1)
        {
            fireInterval = lockedFireInterval;
        }
        locked = locked % waves + 1;
    }
}