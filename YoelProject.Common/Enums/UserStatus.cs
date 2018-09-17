
namespace YoelProject.Common.Enums
{
    public enum UserStatus
    {
        JustCreated = 0, //First Time Login
        Active = 1,
        LockedFromActive = 2,
        LockedFromJustCreated = 3,
        Inactive = 4,
    }
}