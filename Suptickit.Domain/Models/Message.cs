﻿namespace SupTickit.Domain
{
    public class Message:BaseEntity
    {

        public string Body { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public int UserId { get; set; }

    }
}
