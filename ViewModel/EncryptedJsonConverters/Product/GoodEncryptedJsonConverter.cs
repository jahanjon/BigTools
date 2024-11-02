using Common.CustomAttribute;
using Domain;

namespace ViewModel.EncryptedJsonConverters.Product;

public class GoodEncryptedJsonConverter : IntEncryptedJsonConverter
{
    public override Guid EntityGuid => Constant.GoodEntityGuid;
}