using System;
using System.Numerics;
using Microsoft.VisualBasic;

namespace mygga;

public class Wepon
{
    public List<Acid> pools;
    Player player;

    int coolDown = 0;
    int coolGuy = 100;
    int bulletTokill = 0;


    public string name;
    int weaponType;
    public int damage;
    public int area;
    public int amount;

    public bool[] upgradeble = new bool[3];

    public Wepon(int type, int dmg, int aoe, int amt, Player p1)
    {
        weaponType = type;
        damage = dmg;
        area = aoe;
        amount = amt;
        player = p1;
        pools = new List<Acid>();
        if (type == 0)                  //could do bool[][]// i think
        {
            upgradeble = [true, false, true];
            name = "Gun";
        }
        else if(type == 1)
        {
           upgradeble = [true, true, true]; 
           name = "Acid Pools";
        }
    }

    public void Attack()
    {
        if (weaponType == 0)
        {
            Gun();
        }
        if (weaponType == 1)
        {
            Santa();
        }
    }

    void Santa()
    {
        if (coolDown == 0)
        {
            pools.Add(new Acid(damage, area, pools));
        }
    }

    void Gun()
    {
        float radians = (float)player.angle * (MathF.PI / 180f);

        float diffAngle = 5 * (MathF.PI / 180f);
        int piercing;
        if (amount % 2 == 0) radians -= diffAngle * amount / 2;
        else radians -= diffAngle * (int)(amount / 2);
        for (int i = 0; i < amount; i++)
        {
            radians += diffAngle;
            MakeBullet(player.friendlyBullets, radians, damage);
        }
    }

    public void MakeBullet(List<Bullet> bullets, float radians, int dmg)
    {
        int maxBullets = 1000;
        // Create velocity vector
        Vector2 vel = new Vector2(
        MathF.Cos(radians) * 10,  // X velocity
        MathF.Sin(radians) * 10   // Y velocity
        );
        if (bullets.Count <= maxBullets)
        {
            bullets.Add(new Bullet(true, player.hitbox.Position, vel, dmg));
        }
        else
        {
            bullets[bulletTokill].position = player.hitbox.Position;
            bullets[bulletTokill].velocity = vel;
            bullets[bulletTokill].alive = true;
            bullets[bulletTokill].dmg = dmg;
            if (bulletTokill < maxBullets - 1)
            {
                bulletTokill++;
            }
            else
            {
                bulletTokill = 0;
            }
        }
    }
}
