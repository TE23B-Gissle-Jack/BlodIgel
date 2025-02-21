using System;
using System.Numerics;

namespace mygga;

public class Wepon(int type, int dmg, float area, int amt, Player player)
{
    public List<Acid> pools;

    int coolDown = 0;
    int coolGuy  = 100;
    int bulletTokill = 0;

    public void Attack()
    {
        if (type == 1)
        {
            Gun();
        }
        if (type == 2)
        {
            Santa();
        }
    }

    void Santa()
    {
        if (coolDown == 0)
        {
           pools.Add(new Acid(dmg,area,pools));
        }
    }

    void Gun()
    {
        float radians = (float)player.angle * (MathF.PI / 180f);

        float diffAngle = 5 * (MathF.PI / 180f);
        int piercing;
        if (amt % 2 == 0) radians -= diffAngle * amt / 2;
        else radians -= diffAngle * (int)(amt / 2);
        for (int i = 0; i < amt; i++)
        {
            radians += diffAngle;
            MakeBullet(player.friendlyBullets, radians, dmg);
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
