using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int hpMax = 10;
    public int mpMax = 100;
    public int hp = 10;
    public int mp = 100;
    public int score = 0;
    public float speed = 0.1f;

    public int goldCoins = 0;
    public JoystickAdapter joystickAdapter;

    public GuiGame gui;
    private Rigidbody2D body;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        speed = speed * Time.fixedDeltaTime;
    }

    void FixedUpdate()
    {
        if (GlobalSceneGame.Running && joystickAdapter.isControlled) Move();
    }

    private void Move()
    {
        var dest = (Vector2)transform.position + joystickAdapter.vector * speed;
        body.MovePosition(dest);
    }

    public void ChangeHpMax(int delta)
    {
        if (delta > 0)
            hpMax += delta;
        else
        {
            hpMax += delta;
            if (hp > hpMax)
                hp = hpMax;
        }
        gui.SetTextHp(hp, hpMax);
    }

    public void ChangeHp(int delta)
    {
        var tempHp = hp + delta;
        if (tempHp <= 0)
        {
            hp = 0;
            Die();
        }
        else if (delta > 0)
            hp = tempHp > hpMax ? hpMax : tempHp;
        else
            hp = tempHp;

        gui.SetTextHp(hp, hpMax);
    }

    public void ChangeScore(int delta)
    {
        score = score + delta;
        gui.SetTextScore(score);
        GlobalSceneGame.score = score;
    }
    private void Die()
    {
        Utils.Pause();
        gui.ShowGameOver();
        GameObject.Find("Button Pause").gameObject.SetActive(false);
    }
}
