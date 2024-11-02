using Common.CustomAttribute;
using Domain;

namespace ViewModel.EncryptedJsonConverters.Place;

public class CityEncryptedJsonConverter : IntEncryptedJsonConverter
{
    public override Guid EntityGuid => Constant.CityEntityGuid;
}