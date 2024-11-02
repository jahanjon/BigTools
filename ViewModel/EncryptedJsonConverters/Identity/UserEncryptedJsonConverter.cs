using Common.CustomAttribute;
using Domain;

namespace ViewModel.EncryptedJsonConverters.Identity;

public class UserEncryptedJsonConverter : IntEncryptedJsonConverter
{
    public override Guid EntityGuid => Constant.UserEntityGuid;
}