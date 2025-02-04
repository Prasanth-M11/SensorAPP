using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SensorAPP.Models
{
    public class SensorData
    {
        [Key] // Defines primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-generate GUID in DB
        public Guid Guid { get; set; }

        [Required]
        public string SensorType { get; set; }

        [Required]
        public float Value { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-set timestamp when inserted
        public DateTime Timestamp { get; set; } = DateTime.UtcNow; // Default to current UTC time
    }

    public class SensorDataRequest // Separate request model for Swagger
    {
        [Required]
        public string SensorType { get; set; }

        [Required]
        public float Value { get; set; }
    }
}
