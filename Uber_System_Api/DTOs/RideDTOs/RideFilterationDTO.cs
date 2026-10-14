namespace Uber_System_Api.DTOs.RideDTOs
{
    public class RideFilterationDTO
    {
        public int id { get; set; }
        public int MinFare { get; set; }
        public int MaxFare { get; set; }
        public string Status { get; set; }
    }
}
