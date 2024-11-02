using Common.CustomAttribute;
using Domain;

namespace ViewModel.EncryptedJsonConverters.Profile;

public class ShopperFriendEncryptedJsonConverter : IntEncryptedJsonConverter
{
    public override Guid EntityGuid => Constant.ShopperFriendEntityGuid;
}