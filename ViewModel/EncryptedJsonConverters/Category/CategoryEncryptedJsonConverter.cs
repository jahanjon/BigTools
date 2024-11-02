using Common.CustomAttribute;
using Domain;

namespace ViewModel.EncryptedJsonConverters.Category;

public class CategoryEncryptedJsonConverter : IntEncryptedJsonConverter
{
    public override Guid EntityGuid => Constant.CategoryEntityGuid;
}