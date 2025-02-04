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
    public Vector2 speed; 
    public Vector2 position; 
    public Vector2 size;
    List<Exp> expList;

    public bool alive = true;
    int difficulty = 0;

    double angle;

    public Enemy(Vector2 p, int h, float s,Vector2 ss, List<Exp> exp)
    {
        position = p;
        maxHp = h;
        speed = new Vector2(s,s);
        hp = h;
        size = ss;
        hitbox = new Rectangle(position, size);
        expList = exp;
    }

    public void Update(Player player)
    {

        if (alive)
        {
            angle = Math.Atan2(hitbox.Y - player.position.Y, hitbox.X - player.position.X) * (180 / Math.PI);
            Raylib.DrawRectanglePro(this.hitbox,this.size/2,(float)angle,Color.Red);
            Move();
        }
    }
    public void Move()
    {
        float radians = (float)angle * (MathF.PI / 180f);
        // Create velocity vector
        Vector2 vel = new Vector2(
        MathF.Cos(radians) * 1,  // X velocity
        MathF.Sin(radians) * 1   // Y velocity
        );
        hitbox.Position-=vel;
    }
    public void takeDamage(int dmg)
    {
        hp-=dmg;
        if (hp<=0)
        {
            Die();
        }
    }

    private void Die()
    {
        expList.Add(new Exp(difficulty,hitbox.Position,expList));
        alive = false;
    }

    public void CheckDistans(List<Enemy> enemies)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (Vector2.Distance(enemies[i].hitbox.Position,this.hitbox.Position)<size.X)
            {
                bool right;
                bool up;
                if(this.hitbox.X > enemies[i].hitbox.X);
            }
        }
    }
}
