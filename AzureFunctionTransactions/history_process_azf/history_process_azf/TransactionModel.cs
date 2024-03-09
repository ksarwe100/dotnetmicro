using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace history_process_azf
{
    public class TransactionModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public string Amount { get; set; }
        public string Type { get; set; }
        public string CreationDate { get; set; }
        public int AccountId { get; set; }
    }
}

