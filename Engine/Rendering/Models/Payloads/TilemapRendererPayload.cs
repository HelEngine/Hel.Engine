using Hel.Tiled.Models.Tilemap;

namespace Hel.Engine.Rendering.Models.Payloads
{
    public class TilemapRendererPayload : IRendererPayload
    {
        public TilemapRendererPayload(Tilemap tilemap)
        {
            Tilemap = tilemap;
        }

        public Tilemap Tilemap { get; }
    }
}