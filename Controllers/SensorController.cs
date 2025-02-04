using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SensorAPP.Data;
using SensorAPP.Models;
using System;
using System.Linq;

namespace SensorAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public SensorController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/sensor/store
        [HttpPost("store")]
        public IActionResult StoreSensorData([FromBody] SensorDataRequest sensorRequest)
        {
            if (sensorRequest == null)
                return BadRequest("Sensor data is null");

            var sensor = new SensorData
            {
                Guid = Guid.NewGuid(), // Auto-generate GUID
                SensorType = sensorRequest.SensorType,
                Value = sensorRequest.Value,
                Timestamp = DateTime.UtcNow // Add timestamp automatically
            };

            _dbContext.SensorData.Add(sensor);
            _dbContext.SaveChanges(); // Save to DB

            return Ok("Data stored successfully");
        }

        // GET: api/sensor/get
        [HttpGet("get")]
        public IActionResult GetSensorData()
        {
            var sensorDataList = _dbContext.SensorData
                .OrderByDescending(s => s.Timestamp) // Order by latest timestamp
                .ToList();

            if (!sensorDataList.Any())
                return NotFound("No sensor data found.");

            return Ok(sensorDataList);
        }

        // GET: api/sensor/latest
        [HttpGet("latest")]
        public IActionResult GetLatestSensorData()
        {
            var latestSensor = _dbContext.SensorData
                .OrderByDescending(s => s.Timestamp) // Get latest entry
                .FirstOrDefault();

            if (latestSensor == null)
                return NotFound("No sensor data found.");

            return Ok(latestSensor);
        }
    }
}
