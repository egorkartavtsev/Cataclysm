using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Engine.Helpers
{
    internal static class TextureFunctions
    {
        public static Texture2D AddWatermark(Texture2D background, Texture2D watermark)
        {

            int startX = 0;
            int startY = background.height - watermark.height;

            for (int x = startX; x < background.width; x++)
            {

                for (int y = startY; y < background.height; y++)
                {
                    Color bgColor = background.GetPixel(x, y);
                    Color wmColor = watermark.GetPixel(x - startX, y - startY);

                    var t = Mathf.Max(wmColor.a / 1.0f - 0.3f, 0f);
                    Color final_color = Color.Lerp(bgColor, wmColor, t);

                    background.SetPixel(x, y, final_color);
                }
            }

            background.Apply();
            return background;
        }

        public static Texture2D Resize(Texture2D texture2D, int targetX, int targetY)
        {
            RenderTexture rt = new RenderTexture(targetX, targetY, 24);
            RenderTexture.active = rt;
            Graphics.Blit(texture2D, rt);
            Texture2D result = new Texture2D(targetX, targetY);
            result.ReadPixels(new Rect(0, 0, targetX, targetY), 0, 0);
            result.Apply();
            return result;
        }
    }
}
