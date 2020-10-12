using Hel.Engine.Rendering.Models.Enums;
using Hel.Engine.Rendering.Models.Payloads;
using Microsoft.Xna.Framework.Graphics;

namespace Hel.Engine.Rendering
{
    public interface IRenderer<in T> where T: IRendererPayload
    {
        public RendererApi RendererApi { get; }
        public void Draw(T payload, SpriteBatch spriteBatch);
    }
}