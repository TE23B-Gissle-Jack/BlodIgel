using System;
using System.Numerics;
using Raylib_cs;

namespace mygga;

public class Player
{
    public Vector2 position;
    public int hp;
    public int maxHp;
    public float speed;
    public Rectangle hitbox;
    public Vector2 size;

    int bulletTokill = 0;

    int level = 0;
    public int exp = 0;

    public Player(Vector2 p, int h, float s)
    {
        position = p;
        maxHp = h;
        speed = s;
        hp = maxHp;
        size = new Vector2(50,50);
        hitbox = new Rectangle(position, size);
    }

    public void Movement()
    {
        if(Raylib.IsKeyDown(KeyboardKey.W))
        {
            position.Y -= speed;
        }
        else if(Raylib.IsKeyDown(KeyboardKey.S))
        {
            
            position.Y += speed;
        }
        if(Raylib.IsKeyDown(KeyboardKey.A))
        {
            
            position.X -=speed;
        }
        else if(Raylib.IsKeyDown(KeyboardKey.D))
        {
            
            position.X += speed;
        }
    }
    public void Attack(float angle, List<Bullet> bullets)
    {
        float radians = angle * (MathF.PI / 180f);

        // Create velocity vector
        Vector2 vel = new Vector2(
        MathF.Cos(radians) * 10,  // X velocity
        MathF.Sin(radians) * 10   // Y velocity
    );
        if (bullets.Count<=100)
        {
            bullets.Add(new Bullet(true,this.position,vel));
        }
        else
        {
            bullets[bulletTokill].position = this.position;
            bullets[bulletTokill].velocity = vel;
            bullets[bulletTokill].alive = true;
            if (bulletTokill<99)
            {
                bulletTokill++;
            }
            else
            {
                bulletTokill=0;
            }
        }
    }
}

