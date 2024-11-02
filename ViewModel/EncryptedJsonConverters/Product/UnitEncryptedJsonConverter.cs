using Common.CustomAttribute;
using Domain;

namespace ViewModel.EncryptedJsonConverters.Product;

public class UnitEncryptedJsonConverter : IntEncryptedJsonConverter
{
    public override Guid EntityGuid => Constant.UnitEntityGuid;
}