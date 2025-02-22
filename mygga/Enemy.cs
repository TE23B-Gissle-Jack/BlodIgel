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
    public float size;
    List<Exp> expList;
    public bool shooter = false;

    public bool alive = true;
    int difficulty = 0;
    string shape;

    double angle;
    Color color;
    Player player;

    Color currentColor;
    int timeInWrongColor = 0;
    int colorChangeTime = 10;

    //enemy template
    public Enemy(Player hero, Vector2 p, int h, float s, float ss, List<Exp> exp, Color colo, string type)
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
        player = hero;
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
        hitbox = new Rectangle(position, new(size, size));
        this.shape = other.shape;
        this.player = other.player;
    }

    public void Update(List<Enemy> fellows)
    {

        if (alive)
        {
            angle = Math.Atan2(hitbox.Y - player.hitbox.Y, hitbox.X - player.hitbox.X) * (180 / Math.PI);
            Draw();
            //Raylib.DrawRectanglePro(this.hitbox, this.size / 2, (float)angle, color);
            Move();
            CheckDistans(fellows);

            if (currentColor.R != color.R || currentColor.G != color.G ||
                currentColor.B != color.B || currentColor.A != color.A)
            {
                if (timeInWrongColor == colorChangeTime)
                {
                    currentColor = color;
                    timeInWrongColor = 0;
                }
                timeInWrongColor++;
            }
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
        currentColor = new(100,30,30);//make red
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        expList.Add(new Exp(difficulty, hitbox.Position, expList));
        alive = false;
        hitbox.Position = new(float.MinValue, float.MinValue); //stupid, but who cares
    }

    public void CheckDistans(List<Enemy> enemies)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            Enemy target = enemies[i];
            float distance = Vector2.Distance(target.hitbox.Position, this.hitbox.Position);
            bool fuckYou = target != this;
            if (distance < this.size / 2 + target.size / 2 && target != null && target != this)//while but to jumpy
            {
                //Raylib.DrawCircle(400,400,20,Color.DarkGreen);
                float x = 0;
                float y = 0;
                float pushForce = 1;
                if (this.hitbox.X > target.hitbox.X) x = pushForce;
                if (this.hitbox.X < target.hitbox.X) x = -pushForce;

                if (this.hitbox.Y > target.hitbox.Y) y = pushForce;
                if (this.hitbox.Y < target.hitbox.Y) y = -pushForce;

                this.hitbox.X += x;
                this.hitbox.Y += y;

                //distance = Vector2.Distance(target.hitbox.Position, this.hitbox.Position);
            }
        }
    }

    public void Draw()
    {
        if (shape == "square")
        {
            Raylib.DrawRectanglePro(this.hitbox, new Vector2(this.size, this.size) / 2, (float)angle, currentColor);
        }
        else if (shape == "circle")
        {
            Raylib.DrawCircleV(this.hitbox.Position, this.size / 2, currentColor);
        }
        else if (shape == "ring")
        {
            Raylib.DrawRing(this.hitbox.Position, this.size / 4, this.size / 2, 0, 360, 100, currentColor);
        }
        else if (shape == "triangle")
        {
            float radians = ((float)angle + 180) * (MathF.PI / 180f);
            //float dist = size / 2 + size/MathF.Sin(30);
            //not that great, but kinda works
            Vector2 point1 = new Vector2(//green
            MathF.Cos(radians) * size,
            MathF.Sin(radians) * size);
            point1 += this.hitbox.Position;

            radians += 120 * (MathF.PI / 180f);
            Vector2 point2 = new Vector2(//pink
            MathF.Cos(radians) * size,          //size/2 there is a gap betwen enemies//size there is some overlaping
            MathF.Sin(radians) * size);                                               //im not changing my collision
            point2 += this.hitbox.Position;

            radians += 120 * (MathF.PI / 180f);
            Vector2 point3 = new Vector2(//white
            MathF.Cos(radians) * size,
            MathF.Sin(radians) * size);
            point3 += this.hitbox.Position;


            Raylib.DrawTriangle(point3, point2, point1, currentColor);// stupid counterclockwise

            if (false)
            {
                Raylib.DrawCircleV(this.hitbox.Position, 5, Color.Gold);
                Raylib.DrawLineV(point1, point2, Color.Red);
                Raylib.DrawLineV(point1, point3, Color.Red);
                Raylib.DrawLineV(point3, point2, Color.Red);
                Raylib.DrawCircleV(point1, 5, Color.Green);
                Raylib.DrawCircleV(point2, 5, Color.Pink);
                Raylib.DrawCircleV(point3, 5, Color.White);
            }
        }
        else throw null; // make it crash; I dont want invisble enemies
    }
}

