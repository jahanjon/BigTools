using Common.CustomAttribute;
using Domain;

namespace ViewModel.EncryptedJsonConverters.Product;

public class GoodIdListEncryptedJsonConverter : IdListEncryptedJsonConverter
{
    protected override Guid EntityGuid => Constant.GoodEntityGuid;
}