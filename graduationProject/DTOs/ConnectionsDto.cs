namespace graduationProject.DTOs
{
    public class ConnectionsDto
    {
        public int Count { get; set; }
        public IEnumerable<ConnectionDto> Connections { get; set;}
    }
}
