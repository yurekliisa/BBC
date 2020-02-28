namespace BBC.Services.Identity.Dto
{
    public class UserPermission
    {
        public UserPermission()
        {

        }

        public UserPermission(string type, string value)
        {
            PermissionType = type;
            PermissionValue = value;
        }
        public string PermissionType { get; set; }
        public string PermissionValue { get; set; }
    }
}
