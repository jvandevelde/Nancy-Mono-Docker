using System;
using System.ComponentModel.DataAnnotations;

namespace NancyMonoDemo.Entities
{
    public class PageRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime RequestTime { get; set; }
        public string RequestPath { get; set; }
        public string EnvironmentReturned { get; set; }
        public string EsConnectionReturned { get; set; }

        public override string ToString()
        {
            return $"[{RequestTime}] Request: {RequestPath}, Env: {EnvironmentReturned}, Es: {EsConnectionReturned}";
        }
    }
}