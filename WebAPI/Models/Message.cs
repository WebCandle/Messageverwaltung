using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Message
    {
        #region Properties
        [Key]
        public int MessageId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        #endregion


        #region Constructors
        public Message()
        {
            MessageId = 0;
            Text = string.Empty;
            Date = DateTime.Now;
        }
        public Message(string text, DateTime date)
        {
            Text = text;
            Date = date;
        }
        #endregion
    }
}
