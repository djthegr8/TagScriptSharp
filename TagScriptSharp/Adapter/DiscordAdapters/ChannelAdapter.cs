using System.Collections.Generic;

namespace TagScriptSharp.Adapter.DiscordAdapters
{
    /// <summary>
    /// The ``{channel}`` block with no parameters returns the channel's full name
    ///but passing the attributes listed below to the block payload
    ///    will return that attribute instead.
    ///
    ///    ** Usage:** ``{channel([attribute])``
    ///
    ///        ** Payload:** None
    ///
    /// ** Parameter:** attribute, None
    ///
    ///   ** Attributes:**
    ///
    /// id
    ///  The channel's ID.
    ///    name
    ///The channel's name.
    /// created_at
    ///    The channel's creation date.
    ///    timestamp
    ///  The channel's creation date as a UTC timestamp.
    ///     nsfw
    ///     Whether the channel is nsfw.
    ///   mention
    ///   A formatted text that pings the channel.
    ///  topic
    /// The channel's topic.
    /// </summary>
    public class ChannelAdapter : AttributeAdapter
    {
        public new void UpdateAttributes()
        {
            Dictionary<string, object> additionalAtrs = new();
            if (_base.IsTextChannel == true)
            {
                additionalAtrs = new()
                {
                    ["nsfw"] = _base.nsfw,
                    ["mention"] = _base.mention,
                    ["topic"] = _base.topic
                };
            }
            Attributes = Attributes.Update(additionalAtrs);
        }
    }
}