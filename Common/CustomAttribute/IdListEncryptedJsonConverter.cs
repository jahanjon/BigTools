using Common.Utilities;

namespace Common.CustomAttribute;

public abstract class IdListEncryptedJsonConverter : ArrayJsonConverter<int>
{
    protected abstract Guid EntityGuid { get; }

    public override int Read(string value)
    {
        return int.Parse(SecurityHelper.DecryptString(EntityGuid, value));
    }

    public override string Write(int value)
    {
        return SecurityHelper.EncryptString(EntityGuid, value.ToString());
    }
}