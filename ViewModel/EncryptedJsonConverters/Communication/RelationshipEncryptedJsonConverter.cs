using Common.CustomAttribute;
using Domain;

namespace ViewModel.EncryptedJsonConverters.Communication;

public class RelationshipEncryptedJsonConverter : IntEncryptedJsonConverter
{
    public override Guid EntityGuid => Constant.RelationshipEntityGuid;
}