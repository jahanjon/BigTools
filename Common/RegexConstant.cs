namespace Common;

public static class RegexConstant
{
    public static readonly string MobileRegex = @"((0?9)|(\+?989))\d{9}";

    public static readonly string LoginCodeRegex = @"\d{5}$";
}