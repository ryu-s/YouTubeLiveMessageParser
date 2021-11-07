namespace ryu_s.YouTubeLive.Message
{
    public class RemoteIconPart : IBadge
    {
        public string Url { get; }
        public int Width { get; }
        public int Height { get; }
        public string Alt { get; }
        public RemoteIconPart(string url, int width, int height, string alt)
        {
            Url = url;
            Width = width;
            Height = height;
            Alt = alt;
        }
        public override bool Equals(object? obj)
        {
            if (!(obj is RemoteIconPart icon))
            {
                return false;
            }
            return Url == icon.Url && Width == icon.Width && Height == icon.Height && Alt == icon.Alt;
        }
        public override int GetHashCode()
        {
            return Url.GetHashCode() ^ Width.GetHashCode() ^ Height.GetHashCode() ^ Alt.GetHashCode();
        }
    }
}
