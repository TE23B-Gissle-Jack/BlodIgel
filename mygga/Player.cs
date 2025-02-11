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

        // Create velocity vector
        Vector2 vel = new Vector2(
        MathF.Cos(radians) * 10,  // X velocity
        MathF.Sin(radians) * 10   // Y velocity
    );
        if (bullets.Count <= 100)
        {
            bullets.Add(new Bullet(true, this.hitbox.Position, vel));
        }
        else
        {
            bullets[bulletTokill].position = this.hitbox.Position;
            bullets[bulletTokill].velocity = vel;
            bullets[bulletTokill].alive = true;
            if (bulletTokill < 99)
            {
                bulletTokill++;
            }
            else
            {
                bulletTokill = 0;
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
}

