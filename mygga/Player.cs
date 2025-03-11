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
    public List<Bullet> friendlyBullets;

    List<UpgradeCard> upgradeCards = new List<UpgradeCard>();

    public bool isLeveling = false;
    int level = 0;
    public int levelReq = 10;
    public int exp = 0;

    public List<Wepon> wepons = new List<Wepon>();

    public Player(Vector2 p, int h, float s,List<Bullet> bullets)
    {
        position = p;
        maxHp = h;
        speed = s;
        hp = maxHp;
        size = new Vector2(50, 50);
        hitbox = new Rectangle(position, size);
        friendlyBullets = bullets;
    }


    public double angle;
    public void Update()
    {
        Movement();

        Vector2 mousePos = Raylib.GetMousePosition();
        angle = Math.Atan2(mousePos.Y - hitbox.Y, mousePos.X - hitbox.X) * (180 / Math.PI);

        if (exp >= levelReq)
        {
            LevelUp();
        }
        //hitbox = new Rectangle(position, size);
        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            Attack();
        }
    }

    public void Draw()
    {
        Raylib.DrawRectanglePro(hitbox, size / 2, (float)angle, Color.DarkPurple);

        //draw upgrade cards
        if (isLeveling)
        {
            for (int i = 0; i < upgradeCards.Count; i++)
            {
                isLeveling = !upgradeCards[i].Update();
            }
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

    public void Attack()
    {
        for (int i = 0; i < wepons.Count; i++)
        {
            wepons[i].Attack();
        }
    }
    public void LevelUp()
    {
        //Console.Beep();
        exp -= levelReq;
        level++;
        isLeveling = true;
        levelReq = (int)double.Round(levelReq * 1.5);
        for (int i = 0; i < 3; i++)
        {
            Vector2 position = new((Raylib.GetScreenWidth()/3)*(i)+30,200);
            upgradeCards.Add(new UpgradeCard(wepons,position));
        }
        
    }
    
}

