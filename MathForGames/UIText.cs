using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace MathForGames
{
    class UI_Text : Actor
    {
        public string Text;
        public int Width;
        public int Height;
        public int FontSize;
        public Font Font;


        public UI_Text(float x, float y, string name, Color color, int width, int height, int fontSize, string text = "") : base('\0', x, y, color, name)
        {
            Text = text;
            Width = width;
            Height = height;
            Font = Raylib.LoadFont("resources/fonts/alagard.png");
            FontSize = fontSize;
        }
        public UI_Text(float x, float y, string name, Color color) : base('\0', x, y, color)
        {
            Text = "";
            Width = 200;
            Height = 200;
            Font = Raylib.LoadFont("resources/fonts/alagard.png");
            FontSize = 20;
        }


    public override void Draw()
        {
            Rectangle textbox = new Rectangle(Position.X, Position.Y, Width, Height);
            Raylib.DrawTextRec(Font, Text, textbox, FontSize, 1, true, Icon.color);
        }
    }
}
