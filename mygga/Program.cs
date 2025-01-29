using System.Numerics;
using mygga;
using Raylib_cs;

Raylib.InitWindow(800,800,"Surfare");
Raylib.SetTargetFPS(60);

Player player = new Player(new (400,400),10, 2);


List<Bullet> bullets = new List<Bullet>();
List<Enemy> enemies = new List<Enemy>();

enemies.Add(new Enemy(new(100,100),10,5,new(40,40)));

while (!Raylib.WindowShouldClose())
{
    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.Black);
    player.Movement();
    //Raylib.DrawRectangleRec(player.hitbox,Color.DarkPurple);
    Vector2 mousePos = Raylib.GetMousePosition();
    double angle = Math.Atan2(mousePos.Y - player.position.Y, mousePos.X - player.position.X) * (180 / Math.PI);

    Raylib.DrawRectanglePro(player.hitbox,player.size/2,(float)angle,Color.DarkPurple);
    player.hitbox = new Rectangle(player.position,player.size);
    if(Raylib.IsMouseButtonPressed(MouseButton.Left))
    {
        bullets.Add(player.Attack((float)angle));
    }
    
    for (int i = 0; i < bullets.Count; i++)
    {
        bullets[i].Update();
    }
    for (int i = 0; i < enemies.Count; i++)
    {
        enemies[i].Update(player);
    }

    Raylib.EndDrawing();
}
