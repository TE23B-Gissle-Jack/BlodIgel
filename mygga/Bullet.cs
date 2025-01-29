using System;
//using System.Drawing;
using System.Numerics;
using Raylib_cs;

namespace mygga;

public class Bullet(bool friendly, Vector2 position, Vector2 velocity)
{
    public void Update()
    {
        Raylib.DrawCircleV(position,10,Color.Yellow);
        position+=velocity;
    }
}
