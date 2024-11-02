using Common.CustomAttribute;
using Domain;

namespace ViewModel.EncryptedJsonConverters.Product;

public class GoodCodeEncryptedJsonConverter : IntEncryptedJsonConverter
{
    public override Guid EntityGuid => Constant.GoodCodeEntityGuid;
}