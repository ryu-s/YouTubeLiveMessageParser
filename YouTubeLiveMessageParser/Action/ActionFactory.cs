using Newtonsoft.Json;

namespace ryu_s.YouTubeLive.Message.Action
{
    public class ActionFactory
    {
        public static IAction Parse(string json)
        {
            dynamic? obj;
            try
            {
                obj = JsonConvert.DeserializeObject(json);
            }
            catch (JsonReaderException) { obj = null; }
            if (obj == null) return new ParseError(json);
            return Parse(obj);
        }
        public static IAction Parse(dynamic json)
        {
            if (json.ContainsKey("addChatItemAction"))
            {
                if (json.addChatItemAction.item.ContainsKey("liveChatTextMessageRenderer"))
                {
                    return TextMessage.Parse(json.addChatItemAction);
                }
                else if (json.addChatItemAction.item.ContainsKey("liveChatViewerEngagementMessageRenderer"))
                {
                    return new IgnoredMessage((string)json.addChatItemAction.ToString(Formatting.None));
                }
                else if (json.addChatItemAction.item.ContainsKey("liveChatPaidMessageRenderer"))
                {
                    return SuperChat.Parse(json.addChatItemAction);
                }
                else if (json.addChatItemAction.item.ContainsKey("liveChatMembershipItemRenderer"))
                {
                    return MemberShip.Parse(json.addChatItemAction);
                }
                else if (json.addChatItemAction.item.ContainsKey("liveChatPaidStickerRenderer"))
                {
                    return PaidSticker.Parse(json.addChatItemAction);
                }
                else if (json.addChatItemAction.item.ContainsKey("liveChatPlaceholderItemRenderer"))
                {
                    return Placeholder.Parse(json.addChatItemAction);
                }
                else
                {

                }
            }
            else if (json.ContainsKey("addBannerToLiveChatCommand"))
            {
                return IgnoredMessage.Parse(json.addBannerToLiveChatCommand);
            }
            else if (json.ContainsKey("markChatItemAsDeletedAction"))
            {
                return DeletedMessage.Parse(json.markChatItemAsDeletedAction);
            }
            else if (json.ContainsKey("addLiveChatTickerItemAction"))
            {
                if (json.addLiveChatTickerItemAction.item.ContainsKey("liveChatTickerSponsorItemRenderer"))
                {
                    return TickerSponser.Parse(json.addLiveChatTickerItemAction);
                }
                else if (json.addLiveChatTickerItemAction.item.ContainsKey("liveChatTickerPaidMessageItemRenderer"))
                {
                    return TickerPaidMessage.Parse(json.addLiveChatTickerItemAction);
                }
                else if (json.addLiveChatTickerItemAction.item.ContainsKey("liveChatTickerPaidStickerItemRenderer"))
                {
                    return TickerPaidSticker.Parse(json.addLiveChatTickerItemAction);
                }
                else
                {

                }

            }
            else if (json.ContainsKey("showLiveChatTooltipCommand"))
            {
                return IgnoredMessage.Parse(json.showLiveChatTooltipCommand);
            }
            else if (json.ContainsKey("markChatItemsByAuthorAsDeletedAction"))
            {
                return DeletedByAuthor.Parse(json.markChatItemsByAuthorAsDeletedAction);
            }
            else
            {

            }
            return new ParseError(json.ToString());
        }
    }
    class DeletedMessage : IAction
    {
        public static DeletedMessage Parse(dynamic json)
        {
            return new DeletedMessage();
        }
    }
}
