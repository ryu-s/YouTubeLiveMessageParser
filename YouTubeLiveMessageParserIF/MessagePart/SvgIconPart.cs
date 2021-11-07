using System;

namespace ryu_s.YouTubeLive.Message
{
    public class SvgIconPart : IBadge
    {
        public string Data { get; }
        public int Width { get; }
        public int Height { get; }
        public string Alt { get; }
        public SvgIconPart(string data, int width, int height, string alt)
        {
            Data = data;
            Width = width;
            Height = height;
            Alt = alt;
        }
        public override bool Equals(object? obj)
        {
            if (!(obj is SvgIconPart icon))
            {
                return false;
            }
            return Data == icon.Data && Width == icon.Width && Height == icon.Height && Alt == icon.Alt;
        }
        public override int GetHashCode()
        {
            return Data.GetHashCode() ^ Width.GetHashCode() ^ Height.GetHashCode() ^ Alt.GetHashCode();
        }
    }
}
