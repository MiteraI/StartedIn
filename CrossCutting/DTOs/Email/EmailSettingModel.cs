namespace CrossCutting.DTOs.Email
{
    public class EmailSettingModel
    {
        public static EmailSettingModel Instance { get; set; }

        public string FromEmailAddress { get; set; }
        public string FromDisplayName { get; set; }
        public Smtp Smtp { get; set; }
    }
}
