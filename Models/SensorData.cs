namespace SensorAPP.Models
{
    public class SensorData
    {
        public Guid Guid { get; set; }
        public required string SensorType { get; set; }
        public float Value { get; set; }
        public DateTime Timestamp { get; set; }
        
    }
}
