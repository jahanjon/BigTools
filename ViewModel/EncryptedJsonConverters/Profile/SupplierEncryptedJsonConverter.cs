using Common.CustomAttribute;
using Domain;

namespace ViewModel.EncryptedJsonConverters.Profile;

public class SupplierEncryptedJsonConverter : IntEncryptedJsonConverter
{
    public override Guid EntityGuid => Constant.SupplierEntityGuid;
}