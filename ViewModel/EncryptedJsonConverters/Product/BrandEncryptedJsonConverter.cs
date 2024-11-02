using Common.CustomAttribute;
using Domain;

namespace ViewModel.EncryptedJsonConverters.Product;

public class BrandEncryptedJsonConverter : IntEncryptedJsonConverter
{
    public override Guid EntityGuid => Constant.BrandEntityGuid;
}