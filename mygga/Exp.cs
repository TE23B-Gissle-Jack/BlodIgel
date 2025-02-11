using System;
using System.Numerics;
using Raylib_cs;

namespace mygga;

public class Exp(int dropLevel,Vector2 position,List<Exp> expList)
{
    Color[] color = [Color.SkyBlue,Color.Lime,Color.Orange];
    int[] dropAmt = [10,20,30];

    public void Update(Player player)
    {
        Raylib.DrawCircleV(position,5,color[dropLevel]);
        if(Raylib.CheckCollisionCircles(position,5,player.hitbox.Position,player.size.X))
        {
         PickUp(player);
        }
    }
    public void PickUp(Player player)
    {
        expList.Remove(this);
        player.exp += dropAmt[dropLevel];
    }
}
