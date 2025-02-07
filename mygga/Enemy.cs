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
    public bool shooter = false;

    public bool alive = true;
    int difficulty = 0;
    string shape;

    double angle;
    Color color;

    //enemy template
    public Enemy(Vector2 p, int h, float s, Vector2 ss, List<Exp> exp, Color colo, string type)
    {
        //position = p;
        maxHp = h;
        speed = new Vector2(s, s);
        hp = h;
        size = ss;
        //hitbox = new Rectangle(position, size);
        expList = exp;
        color = colo;
        shape = type.ToLower();
    }

    //actual enemy
    public Enemy(Enemy other, Vector2 location)
    {
        this.position = location;
        this.hp = other.hp;
        this.speed = other.speed;
        this.size = other.size;
        this.expList = other.expList;
        this.color = other.color;
        hitbox = new Rectangle(position, size);
        this.shape = other.shape;
    }

    public void Update(Player player, List<Enemy> fellows)
    {

        if (alive)
        {
            angle = Math.Atan2(hitbox.Y - player.position.Y, hitbox.X - player.position.X) * (180 / Math.PI);
            Draw();
            //Raylib.DrawRectanglePro(this.hitbox, this.size / 2, (float)angle, color);
            Move();
            CheckDistans(fellows);
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
        hitbox.Position -= vel;
    }
    public void takeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        expList.Add(new Exp(difficulty, hitbox.Position, expList));
        alive = false;
        hitbox.Position = new(-99999, -99999); //stupid, but who cares
    }

    public void CheckDistans(List<Enemy> enemies)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (Vector2.Distance(enemies[i].hitbox.Position, this.hitbox.Position) < this.size.X)
            {
                //Raylib.DrawCircle(400,400,20,Color.DarkGreen);
                float x = 0;
                float y = 0;
                float pushForce = 1;
                if (this.hitbox.X > enemies[i].hitbox.X) x = pushForce;
                if (this.hitbox.X < enemies[i].hitbox.X) x = -pushForce;

                if (this.hitbox.Y > enemies[i].hitbox.Y) y = pushForce;
                if (this.hitbox.Y < enemies[i].hitbox.Y) y = -pushForce;

                this.hitbox.X += x;
                this.hitbox.Y += y;
            }
        }
    }

    public void Draw()
    {
        if (shape == "square")
        {
            Raylib.DrawRectanglePro(this.hitbox, this.size / 2, (float)angle, color);
        }
        else if (shape == "circle")
        {
            Raylib.DrawCircleV(this.hitbox.Position, this.size.X / 2, color);
        }
        else if (shape == "triangle")
        {
            float radians = ((float)angle+180) * (MathF.PI / 180f);

            //not that great, but kinda works
            Vector2 point1 = new Vector2(//green
            MathF.Cos(radians) * size.X/2,
            MathF.Sin(radians) * size.X/2);
            point1 += this.hitbox.Position;

            radians+=120 * (MathF.PI / 180f);
            Vector2 point2 = new Vector2(//pink
            MathF.Cos(radians) * size.X/2,
            MathF.Sin(radians) * size.X/2);
            point2 += this.hitbox.Position;

            radians+=120 * (MathF.PI / 180f);
            Vector2 point3 = new Vector2(//white
            MathF.Cos(radians) * size.X/2,
            MathF.Sin(radians) * size.X/2);
            point3 += this.hitbox.Position;

            
            Raylib.DrawTriangle(point1, point2, point3, color);
            Raylib.DrawCircleV(this.hitbox.Position, 5, Color.Gold);
            Raylib.DrawLineV(point1,point2,Color.Red);
            Raylib.DrawLineV(point1,point3,Color.Red);
            Raylib.DrawLineV(point3,point2,Color.Red);
            Raylib.DrawCircleV(point1, 5, Color.Green);
            Raylib.DrawCircleV(point2, 5, Color.Pink);
            Raylib.DrawCircleV(point3, 5, Color.White);
        }
    }
}

