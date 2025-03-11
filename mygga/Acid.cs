using System;
using System.Numerics;
using Raylib_cs;

namespace mygga;

public class Acid(int damage, float s, List<Acid> acidPools)
{
    float ogSize = s;
    float size = 0;
    Vector2 position = new Vector2(Random.Shared.Next(800), Random.Shared.Next(800));
    Color color = new Color(100, 255, 100, 100);
    int lifeTime = 300;
    int ogLifeTime = 300;
    int dmgCooldown = 0;
    public void Update(List<Enemy> enemies)
    { 
        if (lifeTime >= 0)
        {
            size = ogSize * lifeTime / ogLifeTime;
            lifeTime--;
            if (dmgCooldown==0)
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (Raylib.CheckCollisionCircles(position,size,enemies[i].hitbox.Position,enemies[i].size/2))
                    {
                        enemies[i].takeDamage(damage);
                        Console.WriteLine("ahahahah");
                    }
                }
                dmgCooldown = 50;
            }
            dmgCooldown--;
        }
        else acidPools.Remove(this);
    }
    public void Draw()
    {
        Raylib.DrawCircleV(position, size, color);
    }
}
