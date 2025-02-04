using System.Numerics;
using mygga;
using Raylib_cs;

Raylib.InitWindow(800,800,"Surfare");
Raylib.SetTargetFPS(60);

Player player = new Player(new (400,400),10, 2);


List<Bullet> friendlyBullets = new List<Bullet>();
List<Enemy> enemies = new List<Enemy>();
List<Exp> expPoints = new List<Exp>();

enemies.Add(new Enemy(new(400,300),50,5,new(40,40),expPoints));
enemies.Add(new Enemy(new(300,300),50,5,new(40,40),expPoints));
enemies.Add(new Enemy(new(500,300),50,5,new(40,40),expPoints));

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
        player.Attack((float)angle,friendlyBullets);
    }
    
    for (int i = 0; i < friendlyBullets.Count; i++)
    {
        if (friendlyBullets[i].alive)
        {
            friendlyBullets[i].Update();
            for (int j = 0; j < enemies.Count; j++)
            {
                Enemy enemy = enemies[j];
                if (enemies[j].alive)
                {
                    int damageTaken = friendlyBullets[i].CheckCollide(enemy.hitbox.Position,enemy.size.X);
                    enemy.takeDamage(damageTaken);
                }
            }
        }
    }
    for (int i = 0; i < enemies.Count; i++)
    {
        enemies[i].Update(player);
    }
    for (int i = 0; i < expPoints.Count; i++)
    {
        expPoints[i].Update(player);
    }

    Raylib.EndDrawing();
}
