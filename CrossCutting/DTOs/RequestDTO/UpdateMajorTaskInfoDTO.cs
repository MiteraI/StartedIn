namespace CrossCutting.DTOs.RequestDTO
{
    public class UpdateMajorTaskInfoDTO
    {
        public string TaskTitle { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> MinorTaskIds { get; set; }
    }
}
