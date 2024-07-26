namespace CrossCutting.DTOs.RequestDTO
{
    public class UpdateMajorTaskInfoDTO
    {
        public string TaskTitle { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> AddMinorTaskIds { get; set; }
        public string? RemoveMinorTaskId { get; set; }
    }
}
