using System.Collections.Generic;
using System.Linq;
namespace TagScriptSharp.Adapter.DiscordAdapters
{
    /// <summary>
    /// The ``{author}`` block with no parameters returns the tag invoker's full username
    ///and discriminator, but passing the attributes listed below to the block payload
    ///will return that attribute instead.
    ///
    ///**Aliases:** ``user``
    ///
    ///**Usage:** ``{
    ///author([attribute])``
    ///
    /// **Payload:**None
    ///
    ///    * *Parameter:**attribute, None
    ///
    ///    ** Attributes:**
    ///
    ///  id
    ///  The author's Discord ID.
    ///  name
    ///  The author's username.
    ///  nick
    ///  The author's nickname, if they have one, else their username.
    ///   avatar
    ///   A link to the author's avatar, which can be used in embeds.
    ///   discriminator
    ///   The author's discriminator.
    ///  created_at
    ///  The author's account creation date.
    ///      timestamp
    ///   The author's account creation date as a UTC timestamp.
    ///      joined_at
    ///   The date the author joined the server.
    ///   mention
    ///   A formatted text that pings the author.
    ///   bot
    ///    Whether or not the author is a bot.*/
    /// </summary>
    public class MemberAdapter : AttributeAdapter
    {
        public new void UpdateAttributes()
        {
            Dictionary<string, object> additionalAttr = new()
            {
                {"nick", _base.DisplayName},
                {"avatar", _base.AvatarUrl},
                {"discriminator", _base.Discriminator},
                {"joined_at", _base.GetAttr("joined_at") == null ? _base.CreatedAt : _base.GetAttr("joined_at")},
                {"mention", _base.Mention},
                {"bot", _base.Bot}
            };
            Attributes = Attributes.Update(additionalAttr);
        }
    }
}