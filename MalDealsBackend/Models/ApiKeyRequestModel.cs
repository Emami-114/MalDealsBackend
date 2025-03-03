using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MalDealsBackend.Models
{
    public class ApiKeyRequestModel
    {
        public required string DeviceId { get; set; }
        public required string Timestamp { get; set; }
        public required string Signature { get; set; }
    }
}