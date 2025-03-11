using System;
//using System.Drawing;
using System.Numerics;
using Raylib_cs;

namespace mygga;

public class Bullet(bool friendly, Vector2 position, Vector2 velocity, int damage)
{
    public Vector2 position = position;
    public Vector2 velocity = velocity; //dumb naming
    
    public int piercing = 0;
    public bool alive = true;
    public int dmg = damage;

    int size = 6;
    
    int collisions = 0;

    public void Update()
    {   
        if (alive)
        {
            Raylib.DrawCircleV(position,size,Color.Yellow);
        }
    }
    public void Move()
    {
        position+=velocity;
    }
    public int CheckCollide(Vector2 targetPosition, float tragetSize)
    {
        //Raylib.DrawCircleV(targetPosition,tragetSize/2,Color.Blue);
        if (Raylib.CheckCollisionCircles(position,size/2,targetPosition,tragetSize/2))
        {
            collisions++;
            if (collisions>piercing)
            {
                alive = false;
            }
            return dmg;
        }
        return 0;
    }
}
