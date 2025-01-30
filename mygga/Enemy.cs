using System;
//using System.Drawing;
using System.Numerics;
using Raylib_cs;

namespace mygga;

public class Enemy
{
    //(int maxHp, float speed, Vector2 position, Vector2 size)
    int hp;
    public Rectangle hitbox;
    public int maxHp; 
    public float speed; 
    public Vector2 position; 
    public Vector2 size;

    public bool alive = true;

    public Enemy(Vector2 p, int h, float s,Vector2 ss)
    {
        position = p;
        maxHp = h;
        speed = s;
        hp = maxHp;
        size = ss;
        hitbox = new Rectangle(position, size);
    }

    public void Update(Player player)
    {

        if (alive)
        {
            double angle = Math.Atan2(position.Y - player.position.Y, position.X - player.position.X) * (180 / Math.PI);
            Raylib.DrawRectanglePro(this.hitbox,this.size/2,(float)angle,Color.Red);
        }
    }
    public void takeDamage(int dmg)
    {
        hp-=dmg;
        if (hp<=0)
        {
            alive = false;
        }
    }
}
