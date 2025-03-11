using System;
using Raylib_cs;
using System.Numerics;

namespace mygga;

public class UpgradeCard
{
    Rectangle hitbox;
    Wepon wepon;
    List<int> what;
    List<string> upgradeNames = ["Attack with", "Effect Area with", "Amount of projectiles"];
    List<List<int>> upgradeAmount = [[20, 10, 30], [10, 20, 5], [1, 2, 3]];

    public UpgradeCard(List<Wepon> weapons, Vector2 position)
    {
        hitbox = new Rectangle(position, new(150, 200));
        wepon = weapons[Random.Shared.Next(weapons.Count)];
        what = [wepon.damage,wepon.area,wepon.amount];

        for (int i = 0; i < upgradeNames.Count; i++)
        {
            if (!wepon.upgradeble[i])
            {
                what.RemoveAt(i);
                upgradeAmount.RemoveAt(i);
                upgradeNames.RemoveAt(i);
            }
        }
        while (upgradeNames.Count > 1)
        {
            int toRemove = Random.Shared.Next(upgradeNames.Count);
            upgradeAmount.RemoveAt(toRemove);
            upgradeNames.RemoveAt(toRemove);
            what.RemoveAt(toRemove);
        }
        while (upgradeAmount[0].Count > 1)
        {
            int toRemove = Random.Shared.Next(upgradeAmount[0].Count);
            upgradeAmount[0].RemoveAt(toRemove);
        }
    }

    public bool Update()
    {  
        if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), hitbox))
        {

            int padding = 0;
            Vector2 position = new Vector2(hitbox.X-10,hitbox.Y-10);
            Rectangle highlight = new Rectangle(position,new(170, 220));

            Raylib.DrawRectangleRec(highlight, Color.Violet);
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                what[0]+=upgradeAmount[0][0];
                return true;
            }
        }
        Raylib.DrawRectangleRec(hitbox, Color.DarkGray);
        Text(wepon.name,Color.Gold,20,25);
        Text("Increase " + upgradeNames[0],Color.Orange,60,15);
        Text(""+upgradeAmount[0][0],Color.Red,120,40);
        return false;
    }

    void Text(string text, Color color, int fromTop, int size)
    {
        int leangth = Raylib.MeasureText(text,size);
        if (leangth > hitbox.Width)
        {
            int lineLeangt = 0;
            List<string> words = text.Split(" ").ToList();
            words.Add("");
            //string lastWord = words[0];
            List<string> lineOneWords = new();
            
            int word = 0;
            while (lineLeangt < hitbox.Width)
            {
                lineOneWords.Add(words[word]);
                lineLeangt += Raylib.MeasureText(" "+words[word],size);
                word++;
            }
            lineOneWords.RemoveAt(lineOneWords.Count-1);
            for (int i = 0; i < word-1; i++)
            {
                words.RemoveAt(0);
            }
            string lineOne = string.Join(" ", lineOneWords);
            string lineTwo = string.Join(" ", words);
            Text(lineOne,color,fromTop,size);
            Text(lineTwo,color,fromTop+20,size);
        }
        else
        {
        int offset = (int)((hitbox.Width-leangth)/2);
        int x = (int)hitbox.X+offset;
        Raylib.DrawText(text, x, (int)hitbox.Y + fromTop, size, color);
        }
    }
}
