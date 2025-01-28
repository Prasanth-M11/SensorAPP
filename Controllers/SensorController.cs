using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Configuration;
using SensorAPP.Models;

namespace SensorAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private static List<SensorData> sensorDataList = new List<SensorData>();

        // POST: api/sensor/store
        [HttpPost("store")]
        public IActionResult StoreSensorData([FromBody] SensorData sensor)
        {
            if (sensor == null)
                return BadRequest("Sensor data is null");

            //sensor.Timestamp = DateTime.UtcNow; // Add timestamp
            sensorDataList.Add(sensor); // Add to in-memory list

            bool success = SaveSensorDataToDatabase(sensor);
            if (success)
            {
                return Ok("Data stored successfully");
            }
            else
            {
                return StatusCode(500, "Failed to save data to the database");
            }
        }

        // GET: api/sensor/get
        [HttpGet("get")]
        public IActionResult GetSensorData()
        {
            return Ok(sensorDataList); // Return the in-memory list
        }

        private bool SaveSensorDataToDatabase(SensorData sensor)
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SensorDBConnectionString"].ConnectionString;
                Console.WriteLine($"Connection String: {connectionString}");

                using (var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("INSERT INTO SensorData (Guid, SensorType, Value,Timestamp) VALUES (@Guid, @SensorType, @Value, @Timestamp)", connection);
                    command.Parameters.AddWithValue("@Guid", sensor.Guid);
                    command.Parameters.AddWithValue("@SensorType", sensor.SensorType);
                    command.Parameters.AddWithValue("@Value", sensor.Value);
                    command.Parameters.AddWithValue("@Timestamp", sensor.Timestamp);


                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
          
            
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw new Exception($"Database error: {ex.Message}"); // Propagate error details
            }
        }
    }
}


