using System;
using System.Collections.Generic;

namespace TagScriptSharp.Adapter.DiscordAdapters
{
    /// <summary>
    /// The ``{server}`` block with no parameters returns the server's name
    ///    but passing the attributes listed below to the block payload
    ///    will return that attribute instead.
    ///
    ///    ** Aliases:** ``guild``
    ///
    ///    ** Usage:** ``{server([attribute])``
    ///
    ///        ** Payload:** None
    ///
    ///        ** Parameter:** attribute, None
    ///
    ///        ** Attributes:**
    ///
    ///    id
    ///        The server's ID.
    ///        name
    ///        The server's name.
    ///        icon
    ///        A link to the server's icon, which can be used in embeds.
    ///        created_at
    ///        The server's creation date.
    ///        timestamp
    ///    The server's creation date as a UTC timestamp.
    ///        member_count
    ///        The server's member count.
    ///        bots
    ///    The number of bots in the server.
    ///        humans
    ///    The number of humans in the server.
    ///        description
    ///    The server's description if one is set, or "No description".
    ///    random
    ///        A random member from the server.
    /// </summary>
    public class GuildAdapter : AttributeAdapter
    {
        public new void UpdateAttributes()
        {
            var guild = _base;
            int bots, humans;
            bots = humans = 0;
            foreach (var m in guild.Members)
            {
                if (m.IsBot()) bots += 1;
                else humans += 1;
            }

            Dictionary<string, object> additionalAttributes = new()
            {
                {"icon", guild.icon_url},
                {"member_count", guild.member_count},
                {"bots", bots},
                {"humans", humans},
                {"description", string.IsNullOrEmpty(guild.Description) ? "None" : guild.Description},
                {"random", guild.Members[new Random().Next()]}
            };
            Attributes.Update(additionalAttributes);
        }
    }
}