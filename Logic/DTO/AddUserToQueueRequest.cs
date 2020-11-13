using System;
using System.ComponentModel.DataAnnotations;

namespace Logic.DTO
{
    public class AddUserToQueueRequest
    {

[Required]
public int BusinessId {get; set;}

        [Required]
        public DateTime QueueStart { get; set; }
        
        public string UserMail { get; set; }

    }
}