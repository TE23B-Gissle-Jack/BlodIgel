using System;
//using System.Drawing;
using System.Numerics;
using Raylib_cs;

namespace mygga;

public class Bullet(bool friendly, Vector2 position, Vector2 velocity)
{
    public Vector2 position = position;
    public Vector2 velocity = velocity; //dumb naming
    
    public int piercing = 0;
    public bool alive = true;
    
    int collisions = 0;

    public void Update()
    {   
        if (alive)
        {
            Raylib.DrawCircleV(position,10,Color.Yellow);
            position+=velocity;
        }
    }

    public int CheckCollide(Rectangle target)
    {
        if (Raylib.CheckCollisionCircleRec(position,5,target))
        {
            collisions++;
            if (collisions>piercing)
            {
                alive = false;
            }
            return 10;
        }
        return 0;
    }
}
