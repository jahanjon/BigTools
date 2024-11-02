using Common.CustomAttribute;
using Domain;

namespace ViewModel.EncryptedJsonConverters.Product;

public class PackageTypeEncryptedJsonConverter : IntEncryptedJsonConverter
{
    public override Guid EntityGuid => Constant.PackageTypeEntityGuid;
}