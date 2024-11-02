using Common.Utilities;

namespace Common.CustomAttribute;

public abstract class IntEncryptedJsonConverter : AppEncryptedJsonConverter<int?>
{
    public abstract Guid EntityGuid { get; }

    public override string Write(int? value)
    {
        return !value.HasValue ? string.Empty : SecurityHelper.EncryptString(EntityGuid, value.Value.ToString());
    }

    public override int? Read(string value)
    {
        return string.IsNullOrEmpty(value) ? null : int.Parse(SecurityHelper.DecryptString(EntityGuid, value));
    }
}