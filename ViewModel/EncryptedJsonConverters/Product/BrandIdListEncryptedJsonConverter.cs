using Common.CustomAttribute;
using Domain;

namespace ViewModel.EncryptedJsonConverters.Product;

public class BrandIdListEncryptedJsonConverter : IdListEncryptedJsonConverter
{
    protected override Guid EntityGuid => Constant.BrandEntityGuid;
}