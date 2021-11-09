﻿namespace ryu_s.YouTubeLive.Message.Action
{
    class TickerPaidSticker : IAction
    {
        public static TickerPaidSticker Parse(dynamic json)
        {
            var id = (string)json.item.liveChatTickerPaidStickerItemRenderer.id;
            return new TickerPaidSticker();
        }
    }
}