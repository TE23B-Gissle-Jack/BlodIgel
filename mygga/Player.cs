using System;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
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
    public int levelReq = 10;
    public int exp = 0;

    public Player(Vector2 p, int h, float s)
    {
        position = p;
        maxHp = h;
        speed = s;
        hp = maxHp;
        size = new Vector2(50, 50);
        hitbox = new Rectangle(position, size);
    }

    public void Update(List<Bullet> friendlyBullets)
    {
        Movement();

        Vector2 mousePos = Raylib.GetMousePosition();
        double angle = Math.Atan2(mousePos.Y - hitbox.Y, mousePos.X - hitbox.X) * (180 / Math.PI);
        Raylib.DrawRectanglePro(hitbox, size / 2, (float)angle, Color.DarkPurple);

        if (exp >= levelReq)
        {
            LevelUp();
        }
        //hitbox = new Rectangle(position, size);
        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            Attack((float)angle, friendlyBullets);
        }
    }

    public void Movement()
    {
        if (Raylib.IsKeyDown(KeyboardKey.W))
        {
            hitbox.Y -= speed;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.S))
        {

            hitbox.Y += speed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.A))
        {

            hitbox.X -= speed;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.D))
        {

            hitbox.X += speed;
        }
    }

    public void Attack(float angle, List<Bullet> bullets)
    {
        float radians = angle * (MathF.PI / 180f);
        bool gun=false , shotgun=true;

        if(shotgun)
        {
            Shotgun(100,9);
        }


        void Gun(int dmg)
        {
            
            int piercing;
            MakeBullet(bullets, radians,dmg);
        }
        void Shotgun(int dmg, int amt)
        {
            float diffAngle = 5*(MathF.PI / 180f);
            int piercing;
            if(amt%2==0) radians-=diffAngle*amt/2;
            else radians-=diffAngle*(int)(amt/2);
            for (int i = 0; i < amt; i++)
            {   
                radians+=diffAngle;
                MakeBullet(bullets, radians, dmg);
            }
        }
    }
    public void LevelUp()
    {
        //Console.Beep();
        exp -= levelReq;
        level++;
        levelReq = (int)double.Round(levelReq * 1.5);
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
            bullets.Add(new Bullet(true, this.hitbox.Position, vel, dmg));
        }
        else
        {
            bullets[bulletTokill].position = this.hitbox.Position;
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

