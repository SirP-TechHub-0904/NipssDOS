﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NipssDOS.Data.Model
{
    public class TicketRequirement
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string QuantityRequired { get; set; }
        public string QuantityIssued { get; set; }
        public decimal Cost { get; set; }
        public string SIVno { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }
        public TicketItemStatus TicketItemStatus { get; set; }

        public long TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}
