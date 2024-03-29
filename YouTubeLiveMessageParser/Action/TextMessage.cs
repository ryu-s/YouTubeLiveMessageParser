﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ryu_s.YouTubeLive.Message;
using System.Linq;

namespace ryu_s.YouTubeLive.Message.Action
{
    public class TextMessage : IAction
    {
        public IReadOnlyList<IMessagePart> MessageItems { get; }
        public string Id { get; }
        public string? AuthorName { get; }
        public long TimestampUsec { get; }
        public Thumbnail2 AuthorPhoto { get; }
        public string AuthorExternalChannelId { get; }
        public List<IAuthorBadge> AuthorBadges { get; }
        public TextMessage(List<IMessagePart> messageItems, string id, string? authorName, long timestampUsec, Thumbnail2 authorPhoto, string channelId, List<IAuthorBadge> authorBadges)
        {
            MessageItems = messageItems;
            Id = id;
            AuthorName = authorName;
            TimestampUsec = timestampUsec;
            AuthorPhoto = authorPhoto;
            AuthorExternalChannelId = channelId;
            AuthorBadges = authorBadges;
        }
        public static TextMessage Parse(string json)
        {
            dynamic? d = JsonConvert.DeserializeObject(json);
            if (d == null)
            {
                throw new ArgumentException();
            }
            return Parse(d);
        }
        public static TextMessage Parse(dynamic json)
        {
            var messageItems = ActionTools.RunsToString(json.item.liveChatTextMessageRenderer.message);

            var timestampUsec = long.Parse((string)json.item.liveChatTextMessageRenderer.timestampUsec);
            var id = (string)json.item.liveChatTextMessageRenderer.id;

            string? authorName;
            if (json.item.liveChatTextMessageRenderer.ContainsKey("authorName"))
            {
                authorName = ActionTools.SimpleTextToString(json.item.liveChatTextMessageRenderer.authorName);
            }
            else
            {
                authorName = null;
            }

            var authorPhoto = Thumbnail2.Parse(json.item.liveChatTextMessageRenderer.authorPhoto.thumbnails[0]);

            var authorBadges = new List<IAuthorBadge>();
            if (json.item.liveChatTextMessageRenderer.ContainsKey("authorBadges"))
            {
                foreach (var badge in json.item.liveChatTextMessageRenderer.authorBadges)
                {
                    var c = AuthorBadgeFactory.Parse(badge);
                    authorBadges.Add(c);
                }
            }
            var authorExternalChannelId = (string)json.item.liveChatTextMessageRenderer.authorExternalChannelId;

            return new TextMessage(messageItems, id, authorName, timestampUsec, authorPhoto, authorExternalChannelId, authorBadges);
        }
    }
    public interface IAuthorBadge { }
    public class AuthorBadgeCustomThumbWithSize : IAuthorBadge
    {
        public List<Thumbnail2> Thumbnails { get; }
        public string Tooltip { get; }
        public AuthorBadgeCustomThumbWithSize(List<Thumbnail2> thumbnails, string tooltip)
        {
            Thumbnails = thumbnails;
            Tooltip = tooltip;
        }
        public override bool Equals(object obj)
        {
            if (!(obj is AuthorBadgeCustomThumbWithSize b))
            {
                return false;
            }
            return Enumerable.SequenceEqual(Thumbnails, b.Thumbnails) && Tooltip == b.Tooltip;
        }
        public override int GetHashCode()
        {
            return Thumbnails.GetHashCode() ^ Tooltip.GetHashCode();
        }
        public override string ToString()
        {
            return $"AuthorBadgeCustomThumbWithSize tooltip={Tooltip}";
        }
    }
    public class AuthorBadgeCustomThumb : IAuthorBadge
    {
        public List<Thumbnail1> Thumbnails { get; }
        public string Tooltip { get; }
        public AuthorBadgeCustomThumb(List<Thumbnail1> thumbnails, string tooltip)
        {
            Thumbnails = thumbnails;
            Tooltip = tooltip;
        }
        public override bool Equals(object obj)
        {
            if (!(obj is AuthorBadgeCustomThumb b))
            {
                return false;
            }
            return Enumerable.SequenceEqual(Thumbnails, b.Thumbnails) && Tooltip == b.Tooltip;
        }
        public override int GetHashCode()
        {
            return Thumbnails.GetHashCode() ^ Tooltip.GetHashCode();
        }
        public override string ToString()
        {
            return $"AuthorBadgeCustomThumb tooltip={Tooltip}";
        }
    }
    public class AuthorBadgeIcon : IAuthorBadge
    {
        public string IconType { get; }
        public string Tooltip { get; }
        public AuthorBadgeIcon(string iconType, string tooltip)
        {
            IconType = iconType;
            Tooltip = tooltip;
        }
        public override bool Equals(object obj)
        {
            if (!(obj is AuthorBadgeIcon b))
            {
                return false;
            }
            return IconType == b.IconType && Tooltip == b.Tooltip;
        }
        public override int GetHashCode()
        {
            return IconType.GetHashCode() ^ Tooltip.GetHashCode();
        }
        public override string ToString()
        {
            return $"AuthorBadgeIcon type={IconType} tooltip={Tooltip}";
        }
    }
    public class AuthorBadgeFactory
    {
        public static IAuthorBadge Parse(string json)
        {
            dynamic? d = JsonConvert.DeserializeObject(json);
            if (d == null)
            {
                throw new ArgumentException();
            }
            return Parse(d);
        }
        public static IAuthorBadge Parse(dynamic json)
        {
            if (json.liveChatAuthorBadgeRenderer.ContainsKey("icon"))
            {
                var iconType = (string)json.liveChatAuthorBadgeRenderer.icon.iconType;
                var tooltip = (string)json.liveChatAuthorBadgeRenderer.tooltip;
                return new AuthorBadgeIcon(iconType, tooltip);
            }
            else if (json.liveChatAuthorBadgeRenderer.ContainsKey("customThumbnail"))
            {
                var customThum = json.liveChatAuthorBadgeRenderer.customThumbnail;
                if (customThum.thumbnails.Count > 0 && customThum.thumbnails[0].ContainsKey("width"))
                {
                    var thumbnails = new List<Thumbnail2>();
                    foreach (var thumb in json.liveChatAuthorBadgeRenderer.customThumbnail.thumbnails)
                    {
                        var url = (string)thumb.url;
                        var width = (int)thumb.width;
                        var height = (int)thumb.height;
                        thumbnails.Add(new Thumbnail2(url, width, height));
                    }
                    var tooltip = (string)json.liveChatAuthorBadgeRenderer.tooltip;
                    return new AuthorBadgeCustomThumbWithSize(thumbnails, tooltip);
                }
                else
                {
                    //最近はwidthやheightが付記されている？
                    var thumbnails = new List<Thumbnail1>();
                    foreach (var thumb in json.liveChatAuthorBadgeRenderer.customThumbnail.thumbnails)
                    {
                        var url = (string)thumb.url;
                        thumbnails.Add(new Thumbnail1(url));
                    }
                    var tooltip = (string)json.liveChatAuthorBadgeRenderer.tooltip;
                    return new AuthorBadgeCustomThumb(thumbnails, tooltip);
                }
            }
            throw new NotImplementedException();
        }
    }
    public class Thumbnail1
    {
        public Thumbnail1(string url)
        {
            Url = url;
        }
        public static Thumbnail1 Parse(dynamic json)
        {
            var url = (string)json.url;
            return new Thumbnail1(url);
        }
        public string Url { get; }
        public override bool Equals(object? obj)
        {
            if (!(obj is Thumbnail1 b))
            {
                return false;
            }
            return Url == b.Url;
        }
        public override int GetHashCode()
        {
            return Url.GetHashCode();
        }
        public override string ToString()
        {
            return $"Thumbnail1 url={Url}";
        }
    }
    public class Thumbnail2
    {
        public Thumbnail2(string url, int width, int height)
        {
            Url = url;
            Width = width;
            Height = height;
        }
        public static Thumbnail2 Parse(dynamic json)
        {
            var url = (string)json.url;
            var width = (int)json.width;
            var height = (int)json.height;
            return new Thumbnail2(url, width, height);
        }

        public string Url { get; }
        public int Width { get; }
        public int Height { get; }
    }
}
