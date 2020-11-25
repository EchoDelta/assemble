namespace Assemble.Desktop.Components
{
    public class DirectionalTexture
    {
        public Texture NorthEastTexture { get; }
        public Texture SouthEastTexture { get; }
        public Texture SouthWestTexture { get; }
        public Texture NorthWestTexture { get; }

        public float Alpha { get; set; } = 1.0f;



        public DirectionalTexture(Texture northEastTexture, Texture southEastTexture, Texture southWestTexture, Texture northWestTexture)
        {
            NorthEastTexture = northEastTexture;
            SouthEastTexture = southEastTexture;
            SouthWestTexture = southWestTexture;
            NorthWestTexture = northWestTexture;
        }
    }
}