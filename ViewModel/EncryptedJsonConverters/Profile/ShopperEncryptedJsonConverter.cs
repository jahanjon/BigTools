using Common.CustomAttribute;
using Domain;

namespace ViewModel.EncryptedJsonConverters.Profile;

public class ShopperEncryptedJsonConverter : IntEncryptedJsonConverter
{
    public override Guid EntityGuid => Constant.ShopperEntityGuid;
}