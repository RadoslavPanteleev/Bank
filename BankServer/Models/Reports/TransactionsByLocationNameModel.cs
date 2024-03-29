﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Models.Reports
{
    public class TransactionsByLocationNameModel
    {
        [DisplayName("№")]
        public int Id { get; set; }

        [DisplayName("Date")]
        public DateTime Date { get; set; }

        [DisplayName("Amount")]
        public double Amount { get; set; }

        [DisplayName("Location name")]
        public string? LocationName { get; set; }

        [DisplayName("Location address")]
        public string? LocationAddress { get; set; }

        [DisplayName("Account number")]
        public Guid? AccountNumber { get; set; }

        [DisplayName("Account name")]
        public string? AccountName { get; set; }
    }
}
