using Common.CustomAttribute;
using Domain;

namespace ViewModel.EncryptedJsonConverters.Financial;

public class GoodDiscountEncryptedJsonConverter : IntEncryptedJsonConverter
{
    public override Guid EntityGuid => Constant.GoodDiscountEntityGuid;
}