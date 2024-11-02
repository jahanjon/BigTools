using Common.CustomAttribute;
using Domain;

namespace ViewModel.EncryptedJsonConverters.Place;

public class ProvinceEncryptedJsonConverter : IntEncryptedJsonConverter
{
    public override Guid EntityGuid => Constant.ProvinceEntityGuid;
}