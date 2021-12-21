using UnityEngine;

namespace Plugins.Jey_s_Tools.Scripts.ExtensionMethods
{
    public static class TextureExtensions
    {
        public static Texture2D CreateReadableTexture2D(this Texture2D texture2d)
        {
            var renderTexture = RenderTexture.GetTemporary(
                texture2d.width,
                texture2d.height,
                0,
                RenderTextureFormat.Default,
                RenderTextureReadWrite.Linear);

            Graphics.Blit(texture2d, renderTexture);

            var previous = RenderTexture.active;

            RenderTexture.active = renderTexture;

            var readableTexture2D = new Texture2D(texture2d.width, texture2d.height);

            readableTexture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);

            readableTexture2D.Apply();

            RenderTexture.active = previous;

            RenderTexture.ReleaseTemporary(renderTexture);

            return readableTexture2D;
        }
    }
}