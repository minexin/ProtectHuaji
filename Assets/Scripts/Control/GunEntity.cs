using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEntity : MonoBehaviour
{

    public JoystickAdapter joystickAdapter;
    public Player player;
    public float offset;
    public GuiGame gui;

    public Gun[] guns;
    public int[] ammo;
    public int[] ammoMax;
    public int[] ammoCost;

    private int currentGun = 0;
    public float cdTime = 0;
    
    // Use this for initialization
    void Start()
    {
        cdTime = guns[currentGun].fireInterval - 0.2f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GlobalSceneGame.Running)
        {
            cdTime += Time.fixedDeltaTime;
            if (cdTime >= guns[currentGun].fireInterval)
            {
                if (joystickAdapter.isControlled)
                {
                    Fire();
                    cdTime = 0;
                }
            }
        }
    }

    private bool UseBullet()
    {
        if (ammo[currentGun] - ammoCost[currentGun] >= 0)
        {
            ammo[currentGun] -= ammoCost[currentGun];
            return true;
        }
        return false;
    }

    private void Fire()
    {
        if (UseBullet())
        {
            var angle = Utils.GetAngleFromVector2(joystickAdapter.vector);
            var outset = GetFirePosition(angle);
            guns[currentGun].Fire(outset, angle);
            if(currentGun != 0)
            {
                gui.SetGunAmmo(currentGun, ammo[currentGun]);
            }
        }
    }

    private Vector3 GetFirePosition(float angle)
    {
        Vector3 playerPosition = transform.parent.transform.position;
        Vector3 fireOffset = (Vector3)Utils.NormalizedVectorFromAngle(angle);
        fireOffset.x *= offset;
        fireOffset.y *= offset;
        return fireOffset + playerPosition;
    }

    public void SwitchGun(int index)
    {
        gui.NotUseGun(currentGun);
        currentGun = index;
        gui.UseGun(currentGun);
    }
}
