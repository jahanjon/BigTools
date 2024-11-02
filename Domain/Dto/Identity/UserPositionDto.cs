using Domain.Dto.Common;

namespace Domain.Dto.Identity;

public class UserPositionDto
{
    public bool IsAdmin { get; set; }
    public List<IdTitleDto> Suppliers { get; set; }
    public List<IdTitleDto> Shoppers { get; set; }
}