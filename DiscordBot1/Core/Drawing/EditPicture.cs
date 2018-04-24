using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DiscordBot1.Core.Drawing
{
    public class EditPicture
    {
        //public DrawImage(string Pokemon, string filePath)
        //{
        //    System.Drawing.Image bgImg = System.Drawing.Image.FromFile("C:\\Users\\David\\Documents\\Visual Studio 2017\\Projects\\DiscordBot1\\DiscordBot1\\bin\\Debug\\Resources\\Pictures\\BattleImage\\Stage.jpg");
        //    System.Drawing.Image Pokemon1 = System.Drawing.Image.FromFile($"C:\\Users\\David\\Documents\\Visual Studio 2017\\Projects\\DiscordBot1\\DiscordBot1\\bin\\Debug\\Resources\\Pictures\\Pokemon\\{Pokemon.ToLower().Trim()}.jpg");
        //    Graphics grImage = Graphics.FromImage(bgImg);
        //    grImage.DrawImage(Pokemon1, bgImg.Width / 2, 10);
        //    bgImg.Save("C:\\Users\\David\\Documents\\Visual Studio 2017\\Projects\\DiscordBot1\\DiscordBot1\\bin\\Debug\\Resources\\Pictures\\Completed\\Test.jpg");

        //    filePath = "C:\\Users\\David\\Documents\\Visual Studio 2017\\Projects\\DiscordBot1\\DiscordBot1\\bin\\Debug\\Resources\\Pictures\\Completed\\Test.jpg";

        //    //await Context.Channel.SendFileAsync("C:\\Users\\David\\Documents\\Visual Studio 2017\\Projects\\DiscordBot1\\DiscordBot1\\bin\\Debug\\Resources\\Pictures\\Completed\\Test.jpg");

        //    return filePath;

        //}

        //private void DrawImageOne(Bitmap img_bm, Bitmap result_bm, int x, int y) //img_bm is the added picture
        //{
        //    const byte ALPHA = 128;

        //    Color clr;

        //    for(int py = 0; py < img_bm.Height; py++)
        //    {
        //        for(int px = 0; px < img_bm.Width; px++)
        //        {
        //            clr = img_bm.GetPixel(px, py);
        //            img_bm.SetPixel(px, py, Color.FromArgb(ALPHA, clr.R, clr.G, clr.B));
        //        }
        //    }

        //    img_bm.MakeTransparent(img_bm.GetPixel(0, 0));

        //    using (Graphics gr = Graphics.FromImage(result_bm))
        //    {
        //        gr.DrawImage(img_bm, x, y);
        //    }
        //}
    }
}
