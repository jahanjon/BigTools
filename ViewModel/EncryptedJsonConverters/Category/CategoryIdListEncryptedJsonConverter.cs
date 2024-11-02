using Common.CustomAttribute;
using Domain;

namespace ViewModel.EncryptedJsonConverters.Category;

public class CategoryIdListEncryptedJsonConverter : IdListEncryptedJsonConverter
{
    protected override Guid EntityGuid => Constant.CategoryEntityGuid;
}