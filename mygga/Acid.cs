using System;
using System.Numerics;
using Raylib_cs;

namespace mygga;

public class Acid(int damage, float s,List<Acid> acidPools)
{
    float size = s;
    Vector2 position = new Vector2(Random.Shared.Next(800),Random.Shared.Next(800));
    Color color = new Color(100,255,100,100);
    int lifeTime = 300;
    int ogLifeTime = 300;
    public void Update(List<Enemy> enemies)
    {
        if (lifeTime>=0)
        {
            Raylib.DrawCircleV(position,size*lifeTime/ogLifeTime,color);
            lifeTime--;
        }
        else acidPools.Remove(this);
    }
}
