using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;

namespace YGCGanpati.Models
{
    [NotMapped()]
    public class GraphData
    {
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime TDate { get; set; }
        public decimal Collections { get; set; }
        public decimal Expenses { get; set; }
        public int Balance { get; set; }
    }

    [Table("Events")]
    public class Event
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int EventID { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Event Date")]
        public DateTime EventDate { get; set; }

        [Required]
        [Display(Name = "Event Name")]
        public string EventName { get; set; }

        [Display(Name = "Event Name")]
        public string EventTime { get; set; }
    }


    [Table("Collections")]
    public class Collection
    {

        public Collection()
        {
            this.CollectionDate = YGCGanpati.Models.Common.GetCurrentDate();
            this.CreatedDate = this.CollectionDate;
            this.ModifiedDate = this.CollectionDate;
            this.Amount = 500;
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CollectionID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Flat/Shop No")]
        public string FlatNo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime CollectionDate { get; set; }

        [Required]
        [Range(1, 1000000)]
        public decimal Amount { get; set; }        
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        [NotMapped()]
        public string Name
        {
            get
            {
                return (this.FirstName + " " + this.LastName);
            }
        }

        [NotMapped()]
        public string Amt
        {
            get
            {
                return NumbersToWords(this.Amount);
            }
        }

        public virtual ApplicationUser UserProfile { get; set; }

        private static string NumbersToWords(decimal inputNumber)
        {
            int inputNo = (int)inputNumber;

            if (inputNo == 0)
                return "Zero";

            int[] numbers = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (inputNo < 0)
            {
                sb.Append("Minus ");
                inputNo = -inputNo;
            }

            string[] words0 = {"" ,"One ", "Two ", "Three ", "Four ",
            "Five " ,"Six ", "Seven ", "Eight ", "Nine "};
            string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
            "Fifteen ","Sixteen ","Seventeen ","Eighteen ", "Nineteen "};
            string[] words2 = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
            "Seventy ","Eighty ", "Ninety "};
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };

            numbers[0] = inputNo % 1000; // units
            numbers[1] = inputNo / 1000;
            numbers[2] = inputNo / 100000;
            numbers[1] = numbers[1] - 100 * numbers[2]; // thousands
            numbers[3] = inputNo / 10000000; // crores
            numbers[2] = numbers[2] - 100 * numbers[3]; // lakhs

            for (int i = 3; i > 0; i--)
            {
                if (numbers[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (numbers[i] == 0) continue;
                u = numbers[i] % 10; // ones
                t = numbers[i] / 10;
                h = numbers[i] / 100; // hundreds
                t = t - 10 * h; // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i == 0) sb.Append("and ");
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
        }


    }


    [Table("Expenses")]
    public class Expense
    {
        public Expense()
        {
            this.ExpenseDate = YGCGanpati.Models.Common.GetCurrentDate();
            this.CreatedDate = this.ExpenseDate;
            this.ModifiedDate = this.ExpenseDate;
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ExpenseID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime ExpenseDate { get; set; }
        [Required]
        [Range(1, 9900000)]
        [Display(Name = "Amount")]
        public decimal ExpenseAmount { get; set; }
        [Required]
        [Display(Name = "Expense")]
        public string ExpenseName { get; set; }
        [Required]
        public string Details { get; set; }        
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual ApplicationUser UserProfile { get; set; }
    }

    [Table("Sponsers")]
    public class Sponser
    {
        public Sponser()
        {
            this.SponserDate = YGCGanpati.Models.Common.GetCurrentDate();
            this.CreatedDate = YGCGanpati.Models.Common.GetCurrentDate();
            this.ModifiedDate = this.CreatedDate;
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int SponserID { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Flat/Shop No")]
        public string FlatNo { get; set; }

        [Required]        
        [Display(Name = "Date")]
        public DateTime SponserDate { get; set; }
        [Required]
        [Range(0, 9900000)]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }
        [Required]
        [Display(Name = "Sponsership")]
        public string Details { get; set; }

        [NotMapped()]
        [Display(Name = "Sponser")]
        public string Name
        {
            get
            {
                return (this.FirstName + " " + this.LastName);
            }
        }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual ApplicationUser UserProfile { get; set; }        
    }
}